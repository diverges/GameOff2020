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

        public ActorBase()
        {
            currentHealth = MaxHealth;
        }

        public void OnEnable()
        {
            currentHealth = MaxHealth;
        }

        public string Name;

        public CaravanClass Class;

        public int MaxHealth;

        public List<CardBase> Backpack;

        public int CurrentHealth
        {
            get => currentHealth;
        }

        public List<Effect> OnEnter;

        public List<Effect> OnExit;

        public List<Effect> OnPrepare;

        public void Heal(int amount)
        {
            this.currentHealth = Math.Min(this.currentHealth + amount, MaxHealth);
        }

        public void Damage(int amount)
        {
            this.currentHealth = Math.Max(this.currentHealth - amount, 0);
        }
    }
}
