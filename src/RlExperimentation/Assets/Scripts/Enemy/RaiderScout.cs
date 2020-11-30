using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class RaiderScout : EnemyBase
    {
        private int intent = Random.Range(0, 4);

        public override string ActorName => "Raider Scout";

        public override void Think()
        {
            if (intent > 0)
            {
                IntentName = "Quick shot!";
                Intent = new List<Effect>() {
                    new Effect()
                    {
                        Type = EffectType.Damage,
                        Amount = 2,
                        Target = EffectTarget.CaravanActiveOrCaravan
                    },
                    new Effect()
                    {
                        Type = EffectType.StatusDurationBased,
                        Amount = 2,
                        Target = EffectTarget.CaravanActive,
                        StatusId = Status.StatusId.Weaken
                    },
                };
            }
            else
            {
                IntentName = "Tactical strike";
                Intent = new List<Effect>() {
                    new Effect()
                    {
                        Type = EffectType.Damage,
                        Amount = 2,
                        Target = EffectTarget.CaravanActiveOrCaravan
                    },
                    new Effect()
                    {
                        Type = EffectType.StatusIntensityBased,
                        Amount = 1,
                        Target = EffectTarget.EnemyActive,
                        StatusId = Status.StatusId.Empowered
                    },
                };
            }
            intent = Random.Range(0, 3);
        }
    }
}
