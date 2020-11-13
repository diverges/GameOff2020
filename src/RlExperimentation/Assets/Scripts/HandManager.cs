using Assets.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class HandManager : MonoBehaviour
    {
        public Deck deck;
        public List<CardBase> hand;

        public void OnHandChange(List<CardBase> newHand)
        {
        }
    }
}
