using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [Serializable]
    //[CreateAssetMenu(fileName = "DefaultEffect", menuName = "Effect", order = 1)]
    public class Effect
    {
        /// <summary>
        /// Effect Type
        /// </summary>
        public EffectType Type;

        /// <summary>
        /// Effect amount.
        /// </summary>
        public int Amount;

        /// <summary>
        /// Effect Target
        /// </summary>
        public EffectTarget Target;

        /// <summary>
        /// Condition for this effect to trigger
        /// </summary>
        public List<EffectCondition> Condition;

        public override string ToString()
        {
            return "Hello";
        }
    }
}
