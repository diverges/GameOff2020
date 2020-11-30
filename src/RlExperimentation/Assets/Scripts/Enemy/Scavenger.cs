using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Scavenger : EnemyBase
    {
        private int intent = Random.Range(0, 3);
        public override string ActorName => "Scavenger";

        public override void Think()
        {
            if (intent == 0)
            {
                IntentName = "Impale";
                Intent = new List<Effect>() { new Effect()
                {
                    Type = EffectType.Damage,
                    Amount = 4,
                    Target = EffectTarget.CaravanActiveOrCaravan
                }};
            }
            else if(intent == 1)
            {
                IntentName = "Headbutt";
                Intent = new List<Effect>() { new Effect()
                {
                    Type = EffectType.Damage,
                    Amount = 3,
                    Target = EffectTarget.CaravanActiveOrCaravan
                },
                new Effect()
                {
                    Type = EffectType.StatusDurationBased,
                    Amount = 2,
                    Target = EffectTarget.CaravanActive,
                    StatusId = Status.StatusId.Weaken
                }};
            }
            else
            {
                IntentName = "Gash";
                Intent = new List<Effect>() { new Effect()
                {
                    Type = EffectType.Damage,
                    Amount = 2,
                    Target = EffectTarget.CaravanActiveOrCaravan
                },
                new Effect()
                {
                    Type = EffectType.StatusIntensityBased,
                    Amount = 1,
                    Target = EffectTarget.CaravanActiveOrCaravan,
                    StatusId = Status.StatusId.Bleed
                }};
            }
            intent = Random.Range(0, 3);
        }
    }
}
