using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class EnemyView : MonoBehaviour
    {
        [HideInInspector] public ActorBase actor;
        public Text Name;
        public Text Health;

        void Update()
        {
            if(actor != null)
            {
                Name.text = actor.Name;
                Health.text = $"HP: {Math.Max(actor.CurrentHealth, 0)}";
            }
        }
    }
}
