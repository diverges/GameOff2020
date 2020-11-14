using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CaravanMember : MonoBehaviour
    {
        private const string healthFormat = "HP: {0}/{1}";
        [HideInInspector] public ActorBase actor;
        public Text headerText;
        public Text health;

        public void Update()
        {
            if (this.actor == null)
            {
                return;
            }

            health.text = string.Format(healthFormat, actor.CurrentHealth, actor.MaxHealth);
        }

        public void SetCaravanMember(ActorBase actor)
        {
            this.actor = actor;
            if (this.actor == null)
            {
                return;
            }

            headerText.text = actor.Name;
            health.text = string.Format(healthFormat, actor.CurrentHealth, actor.MaxHealth);
        }
    }
}
