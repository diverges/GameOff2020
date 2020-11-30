using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class WeakScavenger : EnemyBase
    {
        public override string ActorName => "Weak Scavenger";

        public override void Think()
        {
            IntentName = "Strike";
            Intent = new List<Effect>() { new Effect()
            {
                Type = EffectType.Damage,
                Amount = 4,
                Target = EffectTarget.CaravanActiveOrCaravan
            }};
        }
    }
}
