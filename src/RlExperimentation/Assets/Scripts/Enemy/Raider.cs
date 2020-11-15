using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Raider : EnemyBase
    {
        private bool takeAimBuf = false;
        private int intent = Random.Range(0, 10);

        public override string ActorName => "Raider";

        public override void Think()
        {
            var bufDmg = (takeAimBuf) ? 3 : 0;
            if (intent >= 5)
            {
                IntentName = "TakeAim!";
                Intent = new List<Effect>() { new Effect()
                {
                    Type = EffectType.Damage,
                    Amount = 2 + bufDmg,
                    Target = EffectTarget.CaravanActiveOrCaravan
                }};
                takeAimBuf = true;
            }
            else
            {
                IntentName = "Open Fire!";
                Intent = new List<Effect>() { new Effect()
                {
                    Type = EffectType.Damage,
                    Amount = 3 + bufDmg,
                    Target = EffectTarget.CaravanActiveOrCaravan
                }};
                takeAimBuf = false;
            }
        }
    }
}
