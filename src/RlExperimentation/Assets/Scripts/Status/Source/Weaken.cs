using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Status.Source
{
    class Weaken : StatusBase
    {
        public override string Name => "Weak";

        public override string Description => "Deal half damage.";

        public override Effect OnEffectSource(Effect effect)
        {
            if(effect.Type == EffectType.Damage)
            {
                effect.Amount >>= 1;
            }
            return base.OnEffectSource(effect);
        }
    }
}
