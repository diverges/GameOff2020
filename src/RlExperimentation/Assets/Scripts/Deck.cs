using Assets.Scripts.ScriptableObjects;
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
        int count = drawPile.Count;
        while (count > 1)
        {
            int swapIndex = UnityEngine.Random.Range(0, count);
            count--;
            var value = drawPile[swapIndex];
            drawPile[swapIndex] = drawPile[count];
            drawPile[count] = value;
        }
    }

    /// <summary>
    /// Draw a card from draw pile.
    /// </summary>
    public CardBase Draw()
    {
        if (!drawPile.Any())
        {
            Reset();
        }

        var first = drawPile.FirstOrDefault();
        if (first != null)
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
        Shuffle();
    }
}
