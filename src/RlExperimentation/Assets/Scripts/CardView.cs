using Assets.Scripts;
using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardView : MonoBehaviour
{
    [HideInInspector] public CardBase card;
    [HideInInspector] public HandManager controller;
    public Text headerText;
    public Text descriptionText;

    public void SetCardBase(CardBase card)
    {
        if (card == null)
        {
            return;
        }

        this.card = card;
        headerText.text = card.Name;
        descriptionText.text = string.Join("\r\n", card.Effects.Select(effect => effect.ToString()));
    }

    void OnMouseDown()
    {
        Debug.Log($"Player has played ${card.Name}");
        controller.onCardPlay.Invoke(card);
    }
}
