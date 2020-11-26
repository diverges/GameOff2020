using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [Serializable]
    public class CardPlayEvent : UnityEvent<CardBase> {}

    public class HandManager : MonoBehaviour
    {
        private const int MaxHandWidth = 1800;
        private const int MaxSeperatorWidth = 440;

        public Text deckDisplay;
        public Text discardDisplay;
        public Text actionsDisplay;
        public GameObject handSpawn;
        public GameObject cardPrefab;
        public CardPlayEvent onCardPlay;


        public int remainingActions = 0;
        public bool swapAvailable = false;
        private Deck deck;
        private List<CardBase> hand;

        public HandManager()
        {
            deck = new Deck(Enumerable.Empty<CardBase>().ToList());
            hand = new List<CardBase>();
        }

        public void SetDeck(List<CardBase> cards)
        {
            deck = new Deck(cards);
            hand = new List<CardBase>();
        }

        public void DrawCard(int count)
        {
            // Draw
            for (int i = 0; i < count; ++i)
            {
                var cardPosition = handSpawn.transform.position;
                cardPosition.x += (hand.Count * 425);
                var card = deck.Draw();
                if(card)
                {
                    var instance = Instantiate(cardPrefab, cardPosition, Quaternion.identity);
                    var view = instance.GetComponentInChildren<CardView>();
                    view.controller = this;
                    card.instance = instance;
                    view.SetCardBase(card);
                    hand.Add(card);
                    Debug.Log($"Player drew {card.Name}");
                }
            }

            // Reposition hand
            var index = 0;
            var seperatorWidth = Math.Min(MaxHandWidth / hand.Count(), MaxSeperatorWidth);
            foreach(var card in hand)
            {
                var transform = card.instance.GetComponent<Transform>();
                var newPosX = handSpawn.transform.position.x + index * seperatorWidth;
                transform.position = new Vector3(newPosX, transform.position.y, -index);
                index++;
            }
        }

        public void DiscardCard(CardBase card)
        {
            hand.Remove(card);
            deck.AddToDiscard(card);
            Destroy(card.instance);
        }

        public void DiscardPlayerHand()
        {
            Debug.Log($"Player discards hand of size {hand.Count}");
            var card = hand.FirstOrDefault();
            while (card != null)
            {
                DiscardCard(card);
                card = hand.FirstOrDefault();
            }
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        public void Update()
        {
            actionsDisplay.text = this.remainingActions.ToString();
            deckDisplay.text = $"Deck: {deck.drawPile.Count}";
            discardDisplay.text = $"Discard: {deck.discardPile.Count}";
        }
    }
}
