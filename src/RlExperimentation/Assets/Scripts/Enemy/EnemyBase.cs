using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public abstract class EnemyBase
    {
        private int currentHealth;

        public EnemyBase()
        {
            CurrentHealth = MaxHealth;
        }

        public abstract string Name { get; }

        public abstract int MaxHealth { get; }

        public int CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = Math.Min(value, MaxHealth);
        }

        public List<Effect> OnEnter;

        public List<Effect> OnExit;

        public List<Effect> OnPrepare;

        public List<Effect> Intent { get; }
    }
}
