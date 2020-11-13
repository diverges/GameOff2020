using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.CaravanClass
{
    public class Pistoleer : ActorBase
    {
        public override string Name => "Pistoleer";

        public override int MaxHealth => 16;

        public override BoardState OnEnter(BoardState state, ActorBase previous)
        {
            if (state.enemy == null)
                return state;

            Debug.Log("Pistoleer dealt 2 damage");
            state.enemy.CurrentHealth -= 2;
            return state;
        }

        public override BoardState OnExit(BoardState state, ActorBase next)
        {
            if (state.enemy == null)
                return state;

            Debug.Log("Pistoleer dealt 1 damage");
            state.enemy.CurrentHealth -= 1;
            return state;
        }

        public override BoardState OnPrepare(BoardState state)
        {
            if (state.enemy == null)
                return state;

            Debug.Log("Pistoleer dealt 1 damage");
            state.enemy.CurrentHealth -= 1;
            return state;
        }
    }
}
