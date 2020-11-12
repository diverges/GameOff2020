﻿namespace Assets.Scripts
{
    public abstract class ActorBase
    {
        public ActorBase()
        {
            CurrentHealth = MaxHealth;
        }

        public abstract string Name { get; }

        public abstract int MaxHealth { get; }

        public int CurrentHealth;

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
