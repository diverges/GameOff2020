﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CaravanMember : MonoBehaviour
    {
        private const string healthFormat = "HP: {0}";
        [HideInInspector] public ActorBase actor;
        public Text headerText;
        public Text health;

        public void Update()
        {
            if (this.actor == null)
            {
                return;
            }

            health.text = string.Format(healthFormat, actor.CurrentHealth);
        }

        public void SetCaravanMember(ActorBase actor)
        {
            this.actor = actor;
            headerText.text = actor.Name;
            health.text = string.Format(healthFormat, actor.CurrentHealth);
        }
    }
}
