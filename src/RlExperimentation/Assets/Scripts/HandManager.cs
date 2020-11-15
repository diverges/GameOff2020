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
        public Text deckDisplay;
        public Text discardDisplay;
        public GameObject handSpawn;
        public GameObject cardPrefab;
        public CardPlayEvent onCardPlay;

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
            for (int i = 0; i < count; ++i)
            {
                var cardPosition = handSpawn.transform.position;
                cardPosition.x += (hand.Count * 360);
                var card = deck.Draw();
                if(card)
                {
                    var instance = Instantiate(cardPrefab, cardPosition, Quaternion.identity);
                    var view = instance.GetComponent<CardView>();
                    view.controller = this;
                    card.instance = instance;
                    view.SetCardBase(card);
                    hand.Add(card);
                    Debug.Log($"Player drew {card.Name}");
                }
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
            deckDisplay.text = $"Deck: {deck.drawPile.Count}";
            discardDisplay.text = $"Discard: {deck.discardPile.Count}";
        }
    }
}
