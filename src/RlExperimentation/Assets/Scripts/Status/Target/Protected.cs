using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Status.Target
{
    class Protected : StatusBase
    {
        public override string Name => "Protected";

        public override string Description => "Half damage taken";

        public override Effect OnEffectTarget(Effect effect)
        {
            if (effect.Type == EffectType.Damage)
            {
                effect.Amount >>= 1;
            }
            return base.OnEffectSource(effect);
        }
    }
}
