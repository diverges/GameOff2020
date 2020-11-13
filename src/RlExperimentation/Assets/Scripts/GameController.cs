using Assets.Scripts.CaravanClass;
using Assets.Scripts.Cards;
using Assets.Scripts.Core;
using Assets.Scripts.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [Serializable]
    public class PlayerHandChangeEvent : UnityEvent<List<CardBase>> { }

    public class GameController : MonoBehaviour
    {
        private const string PlayerHandTag = "PlayerHand";
        private const int MaxActionsPerTurn = 2;

        public Text combatText;
        public Text deckText;
        public Text discardText;

        public CaravanManager caravanManager;
        public HandManager handManager;
        public EnemyManager enemyManager;

        public EnemyView enemySpawn;
        public GameObject handSpawn;
        public GameObject cardPrefab;

        private BoardState state;
        private int remainingActions = 0;
        private bool swapAvailable = false;

        // Events
        public PlayerHandChangeEvent onPlayerHandChange;

        public void Awake()
        {
            var deck = new Deck(new List<CardBase>()
            {
                new Bandage(), new Bandage(), new Bandage(),
                new Bandage(), new Bandage(), new Pistol(),
                new Pistol(), new Pistol(), new Pistol(),
                new Pistol()
            });
            state = new BoardState()
            {
                turnState = TurnState.EnemyPrepare,
                enemyPack = new List<EnemyBase> { new Raider(), new Raider(), new Raider(), new Raider() },
                enemy = null,
                deck = deck,
                hand = new List<CardBase>()
            };
        }

        public void Start()
        {
            caravanManager.InitializeCaravan(new List<ActorBase> { new Medic(), new Pistoleer(), new Medic(), new Pistoleer() });

            state.deck.Shuffle();


            do
            {
                ProcessTurnPhase();
            } while (state.turnState != TurnState.PlayerAct && state.turnState != TurnState.End);
        }

        public void Update()
        {
            deckText.text = $"Deck: {state.deck.drawPile.Count}";
            discardText.text = $"Discard: {state.deck.discardPile.Count}";
            if(state.turnState != TurnState.End)
            {
                combatText.text = state.turnState.ToString();
                combatText.text += $"\r\nEnemies Remaining {state.enemyPack.Count()+1}";
                combatText.text += $"\r\nActions {remainingActions}";
                if(swapAvailable)
                {
                    combatText.text += $"\r\nCan Swap!";
                }
            }
            else
            {
                combatText.text = (caravanManager.HasRemainingMembers()) ? "Victory!" : "Defeat!";
            }
        }

        public void PlayCard(CardBase card)
        {
            if(state.turnState == TurnState.PlayerAct)
            {
                state = card.OnPlay(state);
                DiscardCard(card);
                remainingActions--;
            }
            Debug.Log($"Player has ${remainingActions} action left.");

            if(remainingActions == 0)
            {
                OnPlayerActFinish();
            }
        }

        public void SwapWithActive(CaravanMember target)
        {
            if(state.turnState != TurnState.PlayerAct || !swapAvailable)
            {
                return;
            }

            remainingActions--;
            swapAvailable = false;

            var (prev, cur) = caravanManager.SetActiveMember(target);
            prev.OnExit(state, cur);
            cur.OnEnter(state, prev);

            if (remainingActions == 0)
            {
                OnPlayerActFinish();
            }
        }

        private void OnPlayerActFinish()
        {
            state.turnState = TurnState.PlayerEnd;
            do
            {
                ProcessTurnPhase();
            } while (state.turnState != TurnState.PlayerAct && state.turnState != TurnState.End);
        }

        private void DrawAndPlaceCard(int count)
        {
            Debug.Log($"Player draws {count}");
            for(int i = 0; i < count; ++i)
            {
                var hand = state.hand;
                var cardPosition = handSpawn.transform.position;
                cardPosition.x += (hand.Count * 360);
                var card = state.deck.Draw();
                var instance = Instantiate(cardPrefab, cardPosition, Quaternion.identity);
                var view = instance.GetComponent<CardView>();
                instance.tag = PlayerHandTag;
                card.instance = instance;
                view.controller = this;
                view.SetCardBase(card);
                hand.Add(card);
                onPlayerHandChange.Invoke(state.hand);
                Debug.Log($"Player drew {card.Name} (id:{card.instanceId})");
            }
        }

        private void DiscardCard(CardBase card)
        {
            state.hand.Remove(card);
            Debug.Log(state.hand.Count);
            Destroy(card.instance);
            state.deck.AddToDiscard(card);
        }

        private void DiscardPlayerHand()
        {
            Debug.Log($"Player discards hand of size {state.hand.Count}");
            var card = state.hand.FirstOrDefault();
            while(card != null)
            {
                DiscardCard(card);
                card = state.hand.FirstOrDefault();
            }
        }

        private void ProcessTurnPhase()
        {
            switch (state.turnState)
            {
                case TurnState.EnemyPrepare:
                    if (state.enemy == null && state.enemyPack.Any())
                    {
                        state.enemy = state.enemyPack.First();
                        enemySpawn.actor = state.enemy;
                        state.enemyPack.RemoveAt(0);
                        state.enemy.OnEnter(state, null);
                        state.turnState = TurnState.PlayerDraw;
                    }
                    else
                    {
                        state.turnState = (state.enemy != null) ? TurnState.EnemyAct : TurnState.End;
                    }
                    break;
                case TurnState.EnemyAct:
                    state.enemy.OnPrepare(state);
                    state.turnState = TurnState.PlayerDraw;
                    caravanManager.CleanupCaravan();
                    break;
                case TurnState.PlayerDraw:
                    remainingActions = MaxActionsPerTurn;
                    swapAvailable = true;
                    DrawAndPlaceCard(4);
                    state.turnState = TurnState.PlayerPrepare;
                    break;
                case TurnState.PlayerPrepare:
                    caravanManager.OnPrepare(state);
                    state.turnState = (caravanManager.HasRemainingMembers()) ? TurnState.PlayerAct : TurnState.End;
                    break;
                case TurnState.PlayerAct:
                    break;
                case TurnState.PlayerEnd:
                    caravanManager.CleanupCaravan();
                    DiscardPlayerHand();
                    if(state.enemy.CurrentHealth <= 0)
                    {
                        state.enemy = null;
                    }
                    state.turnState = TurnState.EnemyPrepare;
                    break;
                case TurnState.End:
                    break;
            }
        }
    }
}