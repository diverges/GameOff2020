using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Status.Target
{
    class Pierce : StatusBase
    {
        public override string Name => "Pierced";

        public override string Description => "Take double damage";

        public override Effect OnEffectTarget(Effect effect)
        {
            if(effect.Type == EffectType.Damage)
            {
                effect.Amount <<= 1;
            }
            return base.OnEffectTarget(effect);
        }
    }
}
