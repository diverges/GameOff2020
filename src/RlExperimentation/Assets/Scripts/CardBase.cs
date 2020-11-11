
using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public abstract class CardBase
    {
        public CardBase()
        {
            instanceId = Guid.NewGuid();
        }

        public Guid instanceId { get; }

        public GameObject instance { get; set; }

        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract BoardState OnPlay(BoardState state);
    }
}
