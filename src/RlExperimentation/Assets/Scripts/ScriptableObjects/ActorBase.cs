using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "DefaultActor", menuName = "Member", order = 2)]
    public class ActorBase : ScriptableObject
    {
        private int currentHealth;

        public void OnEnable()
        {
            CurrentHealth = MaxHealth;
        }

        public string Name;

        public int MaxHealth;

        public int CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = Math.Min(value, MaxHealth);
        }

        public List<Effect> OnEnter;

        public List<Effect> OnExit;

        public List<Effect> OnPrepare;
    }
}
