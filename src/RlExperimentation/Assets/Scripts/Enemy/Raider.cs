using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Raider : ActorBase
    {
        public override string Name => "Raider";

        public override int MaxHealth => 13;

        private bool takeAimBuf = false;

        public override BoardState OnEnter(BoardState state, ActorBase previous)
        {
            var target = state.GetFirstAvailablePlayerTarget();
            target.CurrentHealth -= 3;
            Debug.Log($"Raider deals damage 3 damage to {target.Name}.");
            return state;
        }

        public override BoardState OnPrepare(BoardState state)
        {
            var target = state.GetFirstAvailablePlayerTarget();
            var baseDamage = (takeAimBuf) ? 4 : 0;
            takeAimBuf = false;

            if (Random.Range(0, 10) >= 5 )
            {
                // Take Aim
                Debug.Log("Raider is taking aim!");
                target.CurrentHealth -= (baseDamage + 3);
                takeAimBuf = true;
            }
            else
            {
                // Open Fire!
                Debug.Log("Raider fired!");
                target.CurrentHealth -= (baseDamage + 7);
            }
            return state;
        }
    }
}
