using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Raider : EnemyBase
    {
        public override string Name => "Raider";

        public override int MaxHealth => 13;

        public override string Intent {
            get {
                var baseDamage = (takeAimBuf) ? 4 : 0;

                if (intent >= 5)
                {
                    return $"<b>Take Aim!</b>\r\nDeal <b>{baseDamage+3}</b> damage. Next turn deal <b>+2</b> damage.";
                }
                return $"<b>Fire!</b>\r\nDeal <b>{baseDamage+7}</b> damage.";
            }
        }

        private bool takeAimBuf = false;
        private int intent = Random.Range(0, 10);

        public override BoardState OnEnter(BoardState state, ActorBase previous)
        {
            //var target = state.GetFirstAvailablePlayerTarget();
            //target.CurrentHealth -= 3;
            //Debug.Log($"Raider deals damage 3 damage to {target.Name}.");
            return state;
        }

        public override BoardState OnPrepare(BoardState state)
        {
            //var target = state.GetFirstAvailablePlayerTarget();
            //var baseDamage = (takeAimBuf) ? 4 : 0;
            //takeAimBuf = false;

            //if (intent >= 5 )
            //{
            //    // Take Aim
            //    Debug.Log("Raider is taking aim!");
            //    target.CurrentHealth -= (baseDamage + 3);
            //    takeAimBuf = true;
            //}
            //else
            //{
            //    // Open Fire!
            //    Debug.Log("Raider fired!");
            //    target.CurrentHealth -= (baseDamage + 7);
            //}
            intent = Random.Range(0, 10);
            return state;
        }
    }
}
