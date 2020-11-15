using Assets.Scripts.Core;
using Assets.Scripts.Enemy;
using Assets.Scripts.ScriptableObjects;
using System;
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

        private TurnState turnState;
        private int remainingActions = 0;
        private bool swapAvailable = false;
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

            do
            {
                ProcessTurnPhase();
            } while (turnState != TurnState.PlayerAct && turnState != TurnState.End);
        }

        public void Update()
        {
            if(turnState != TurnState.End)
            {
                turnText.text = turnState.ToString();
                turnText.text += $"\r\nEnemies Remaining {enemyManager.GetReminingEnemyCount()}";
                turnText.text += $"\r\nActions {remainingActions}";
                if(swapAvailable)
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
                ProcessEffectsAndLog(card.Effects);
                handManager.DiscardCard(card);
                remainingActions--;
            }
            Debug.Log($"Player has ${remainingActions} action left.");

            if (remainingActions == 0)
            {
                OnPlayerActFinish();
            }
        }

        public void SwapWithActive(CaravanMember target)
        {
            if(turnState != TurnState.PlayerAct || !swapAvailable)
            {
                return;
            }

            remainingActions--;
            swapAvailable = false;

            var (prev, cur) = caravanManager.SetActiveMember(target);
            if(prev != null)
            {
                ProcessEffectsAndLog(prev.OnExit);
            }
            ProcessEffectsAndLog(cur.OnEnter);

            if (remainingActions == 0)
            {
                OnPlayerActFinish();
            }
        }

        private void OnPlayerActFinish()
        {
            turnState = TurnState.PlayerEnd;
            do
            {
                ProcessTurnPhase();
            } while (turnState != TurnState.PlayerAct && turnState != TurnState.End);
        }

        private void ProcessTurnPhase()
        {
            switch (turnState)
            {
                case TurnState.EnemyPrepare:
                    if (!enemyManager.IsActiveEnemyAlive()
                        && enemyManager.TrySpawnNextEnemy(out EnemyBase result))
                    {
                        ProcessEffectsAndLog(result.Instance.OnEnter);
                        result.Think();
                        turnState = TurnState.PlayerDraw;
                    }
                    else
                    {
                        turnState = (enemyManager.IsActiveEnemyAlive())
                            ? TurnState.EnemyAct : TurnState.End;
                    }
                    break;
                case TurnState.EnemyAct:
                    ProcessEffectsAndLog(enemyManager.GetCurrentEnemy().Intent);
                    enemyManager.GetCurrentEnemy().Think();
                    caravanManager.CleanupCaravan();
                    turnState = TurnState.PlayerDraw;
                    break;
                case TurnState.PlayerDraw:
                    remainingActions = MaxActionsPerTurn;
                    swapAvailable = true;
                    handManager.DrawCard(4);
                    turnState = TurnState.PlayerPrepare;
                    break;
                case TurnState.PlayerPrepare:
                    if(caravanManager.activeCaravanActor)
                        ProcessEffectsAndLog(caravanManager.activeCaravanActor.OnPrepare);
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
        }

        private void ProcessEffectsAndLog(List<Effect> effects)
        {
            foreach(var result in effectProcessor.ProcessEffect(effects))
            {
                Debug.Log(result);
                combatTextStore.Add(result);
            }
            var count = combatTextStore.Count();
            if ( count > 15)
            {
                combatTextStore.RemoveRange(0, count-15);
            }
        }
    }
}