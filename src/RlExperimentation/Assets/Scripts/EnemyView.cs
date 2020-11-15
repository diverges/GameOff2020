using System;
using System.Linq;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class EnemyView : MonoBehaviour
    {
        [HideInInspector] public EnemyBase actor;
        public Text Name;
        public Text Health;
        public Text Intent;
        public Text IntentDescription;

        void Update()
        {
            if(actor != null)
            {
                Name.text = actor.Instance.Name;
                Health.text = $"HP: {Math.Max(actor.Instance.CurrentHealth, 0)}";
                Intent.text = actor.IntentName;
                IntentDescription.text = string.Join(
                    "\r\n",
                    actor.Intent.Select(effect => effect.ToString()));

            }
        }
    }
}
