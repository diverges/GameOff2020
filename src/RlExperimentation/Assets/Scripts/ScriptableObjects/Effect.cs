using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<EffectCondition> Condition = new List<EffectCondition>();

        public override string ToString()
        {
            string conditionText = string.Join(", ", this.Condition.Select(condition =>
            {
                switch (condition.Condition)
                {
                    case ScriptableObjects.Condition.Synergy:
                        return $"<b>Synergy</b> {condition.Value}";
                    default:
                        return "";
                }
            }));

            var effectText = "";
            switch (this.Type)
            {
                case EffectType.Damage:
                    effectText = $"Deal {this.Amount} damage to {TargetToFriendlyString(this.Target)}.";
                    break;
                case EffectType.Heal:
                    effectText = $"Heal {TargetToFriendlyString(this.Target)} for {this.Amount}.";
                    break;
                default:
                    return "No action";
            }

            return $"{conditionText}{(string.IsNullOrEmpty(conditionText) ? "": ": ")}{effectText}";
        }

        private static string TargetToFriendlyString(EffectTarget target)
        {
            switch(target)
            {
                case EffectTarget.EnemyActive:
                    return "enemy";
                case EffectTarget.EnemyPack:
                    return "enemy pack";
                case EffectTarget.EnemyActiveAndPack:
                    return "all enemies";
                case EffectTarget.CaravanActive:
                    return "active member";
                case EffectTarget.CaravanActiveOrCaravan:
                    return "active or caravan member";
                case EffectTarget.Caravan:
                    return "caravan";
                case EffectTarget.CaravanAndActive:
                    return "all caravan members";
                case EffectTarget.CaravanLowestHealth:
                    return "lowest health caravan member";
                case EffectTarget.CaravanHighestHealth:
                    return "highest health caravan member";
                default:
                    throw new Exception("No valid value");
            }
        }
    }
}
