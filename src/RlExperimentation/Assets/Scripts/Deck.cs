using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Deck
{
    public List<CardBase> drawPile;
    public List<CardBase> discardPile;

    public Deck(List<CardBase> cards)
    {
        discardPile = new List<CardBase>();
        drawPile = new List<CardBase>(cards);
    }

    public int DeckLength { get => drawPile.Count; }

    /// <summary>
    /// Shuffle draw pile
    /// </summary>
    public void Shuffle()
    {
        drawPile = drawPile.OrderBy(x => Guid.NewGuid()).ToList();
    }

    /// <summary>
    /// Draw a card from draw pile.
    /// </summary>
    public CardBase Draw()
    {
        if(!drawPile.Any())
        {
            Reset();
        }

        var first = drawPile.First();
        drawPile.RemoveAt(0);
        return first;
    }

    /// <summary>
    /// Add card to discard
    /// </summary>
    /// <param name="card"></param>
    public void AddToDiscard(CardBase card)
    {
        discardPile.Add(card);
    }

    /// <summary>
    /// Move discardPile to draw pile.
    /// </summary>
    public void Reset()
    {
        drawPile = new List<CardBase>(drawPile);
        drawPile.AddRange(discardPile);
        discardPile.Clear();
    }
}
