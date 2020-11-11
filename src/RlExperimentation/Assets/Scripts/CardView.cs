﻿using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardView : MonoBehaviour
{
    [HideInInspector] public CardBase card;
    [HideInInspector] public GameController controller;
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
        descriptionText.text = card.Description;
    }

    void OnMouseDown()
    {
        Debug.Log($"Player has played ${card.Name}");
        controller.PlayCard(card);
    }
}