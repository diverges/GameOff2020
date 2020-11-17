using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Status.Source
{
    class Empower : StatusBase
    {
        public override string Name => "Empower";

        public override string Description => $"Deal +{this.Intensity} damage per attack.";

        /// <summary>
        /// Empower can be negative.
        /// </summary>
        public override bool IsExpired => this.Intensity == 0;

        /// <summary>
        /// Avoid decrementing Empower on turn start.
        /// </summary>
        /// <param name="source"></param>
        public override void OnActorTurnStart(ActorBase source)
        {}

        public override Effect OnEffectSource(Effect effect)
        {
            if(effect.Type == EffectType.Damage)
            {
                effect.Amount += Intensity;
            }
            return effect;
        }
    }
}
