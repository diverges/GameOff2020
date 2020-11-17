using Assets.Scripts.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [Serializable]
    public class Effect
    {
        public Effect()
        { }

        public Effect(Effect right)
        {
            this.Type = right.Type;
            this.Amount = right.Amount;
            this.Target = right.Target;
            this.StatusId = right.StatusId;
            this.Condition = right.Condition;
        }

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
        /// For status effects, the id of the target
        /// </summary>
        public StatusId StatusId;

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
                case EffectType.StatusIntensityBased:
                    effectText = $"Apply {Amount} {StatusId} to {TargetToFriendlyString(this.Target)}.";
                    break;
                case EffectType.StatusDurationBased:
                    effectText = GetDurationBasedStatusText();
                    break;
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

        private string GetDurationBasedStatusText()
        {
            string result = $"Apply {StatusId} to {TargetToFriendlyString(this.Target)}";
            return (Amount == 1) ? result + "." : $"{result} for {Amount} turns.";
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
