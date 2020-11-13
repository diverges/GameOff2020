using System;

namespace Assets.Scripts.Core
{
    public abstract class ActorBase
    {
        private int currentHealth;

        public ActorBase()
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

        public virtual BoardState OnEnter(BoardState state, ActorBase previous)
        {
            return state;
        }

        public virtual BoardState OnExit(BoardState state, ActorBase next)
        {
            return state;
        }

        public virtual BoardState OnPrepare(BoardState state)
        {
            return state;
        }
    }
}
