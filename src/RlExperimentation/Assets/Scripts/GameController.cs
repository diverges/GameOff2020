using Assets.Scripts.Core;
using Assets.Scripts.Enemy;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private const int MaxActionsPerTurn = 2;

        private List<string> combatTextStore = new List<string>();
        public Text combatText;
        public Text turnText;

        public List<ActorBase> startingCaravan;

        public CaravanManager caravanManager;
        public HandManager handManager;
        public EnemyManager enemyManager;

        private bool encounterActive;
        private TurnState turnState;
        private EffectProcessor effectProcessor;

        public void Awake()
        {
            turnState = TurnState.EnemyPrepare;
            effectProcessor = new EffectProcessor(handManager, caravanManager, enemyManager);
        }

        public void Start()
        {
            caravanManager.InitializeCaravan(startingCaravan
                .Select(item => UnityEngine.Object.Instantiate(item))
                .ToList());
            handManager.SetDeck(startingCaravan
                .SelectMany(item =>item.Backpack.Select(card => Instantiate(card)))
                .ToList());
            handManager.Shuffle();
            enemyManager.SetEnemyPack(new List<EnemyBase>
            {
                new Raider(), new Raider(), new Raider(), new Raider()
            });
            encounterActive = true;

            StartCoroutine(ProcessTurn());
        }

        public void Update()
        {
            if(turnState != TurnState.End)
            {
                turnText.text = turnState.ToString();
                turnText.text += $"\r\nEnemies Remaining {enemyManager.GetReminingEnemyCount()}";
                turnText.text += $"\r\nActions {handManager.remainingActions}";
                if(handManager.swapAvailable)
                {
                    turnText.text += $"\r\nCan Swap!";
                }
            }
            else
            {
                turnText.text = (caravanManager.HasRemainingMembers()) ? "Victory!" : "Defeat!";
            }
            combatText.text = combatTextStore.Aggregate<string>((prev, cur) => $"{prev}\r\n{cur}");
        }

        public void PlayCard(CardBase card)
        {
            if (turnState == TurnState.PlayerAct)
            {
                if (caravanManager.activeCaravanActor != null)
                {
                    ProcessEffectsAndLog(card.Effects
                        .Select((effect) => caravanManager.activeCaravanActor.OnEffectSource(effect))
                        .ToList(), null);
                }
                else
                {
                    ProcessEffectsAndLog(card.Effects, null);
                }
                handManager.DiscardCard(card);
                handManager.remainingActions--;
            }
            Debug.Log($"Player has ${handManager.remainingActions} action left.");

            if (handManager.remainingActions == 0)
            {
                OnPlayerActFinish();
            }
        }

        public void SwapWithActive(CaravanMember target)
        {
            if(turnState != TurnState.PlayerAct || !handManager.swapAvailable)
            {
                return;
            }

            handManager.remainingActions--;
            handManager.swapAvailable = false;

            var (prev, cur) = caravanManager.SetActiveMember(target);
            if (prev != null)
            {
                prev.animations.EnterRight();
                ProcessEffectsAndLog(prev.OnExit, prev);
            }
            cur.animations.EnterLeft();
            ProcessEffectsAndLog(cur.OnEnter, cur);

            if (handManager.remainingActions == 0)
            {
                OnPlayerActFinish();
            }
        }

        private void OnPlayerActFinish()
        {
            turnState = TurnState.PlayerEnd;
            StartCoroutine(ProcessTurn());
        }

        private IEnumerator ProcessTurn()
        {
            do
            {
                yield return ProcessTurnPhase();
            } while (turnState != TurnState.PlayerAct && turnState != TurnState.End);
        }

        private IEnumerator ProcessTurnPhase()
        {
            switch (turnState)
            {
                case TurnState.EnemyPrepare:
                    enemyManager.OnEnemyTurnStart();
                    if (enemyManager.IsActiveEnemyAlive())
                    {
                        turnState = TurnState.EnemyAct;
                    }
                    else
                    {
                        turnState = (TryUpdateActiveEnemy())
                            ? TurnState.PlayerDraw : TurnState.End;
                    }
                    break;
                case TurnState.EnemyAct:
                    var enemy = enemyManager.GetCurrentEnemy();
                    ProcessEffectsAndLog(enemy.Intent.Select(
                        effect => enemy.Instance.OnEffectSource(effect)).ToList(),
                        enemy.Instance);
                    enemy.Think();
                    caravanManager.CleanupCaravan();
                    turnState = TurnState.PlayerDraw;
                    break;
                case TurnState.PlayerDraw:
                    handManager.remainingActions = MaxActionsPerTurn;
                    handManager.swapAvailable = true;
                    caravanManager.OnPlayerTurnStart();
                    handManager.DrawCard(4);
                    turnState = TurnState.PlayerPrepare;
                    break;
                case TurnState.PlayerPrepare:
                    if(caravanManager.activeCaravanActor)
                        ProcessEffectsAndLog(
                            caravanManager.activeCaravanActor.OnPrepare,
                            caravanManager.activeCaravanActor);
                    turnState = (caravanManager.HasRemainingMembers()) ? TurnState.PlayerAct : TurnState.End;
                    break;
                case TurnState.PlayerAct:
                    break;
                case TurnState.PlayerEnd:
                    caravanManager.CleanupCaravan();
                    handManager.DiscardPlayerHand();
                    turnState = TurnState.EnemyPrepare;
                    break;
                case TurnState.End:
                    break;
            }
            yield return new WaitForSeconds(1);
        }

        private void ProcessEffectsAndLog(List<Effect> effects, ActorBase source)
        {
            foreach(var result in effectProcessor.ProcessEffect(effects, source))
            {
                Debug.Log(result);
                combatTextStore.Add(result);
            }
            var count = combatTextStore.Count();
            if ( count > 15)
            {
                combatTextStore.RemoveRange(0, count-15);
            }

            if (!TryUpdateActiveEnemy() && enemyManager.IsActiveEnemyAlive())
            {
                encounterActive = false;
                Debug.Log("Encounter over");
            }
        }

        private bool TryUpdateActiveEnemy()
        {
            if(enemyManager.IsActiveEnemyAlive())
            {
                return true;
            }

            if (enemyManager.TrySpawnNextEnemy(out EnemyBase result))
            {
                ProcessEffectsAndLog(
                    result.Instance.OnEnter,
                    result.Instance);
                result.Think();
                return true;
            }

            return false;
        }
    }
}