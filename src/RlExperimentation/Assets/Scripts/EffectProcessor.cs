using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Status;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class EffectProcessor
    {
        private readonly HandManager hand;
        private readonly CaravanManager caravan;
        private readonly EnemyManager enemy;

        public EffectProcessor(HandManager hand, CaravanManager caravan, EnemyManager enemy)
        {
            this.hand = hand;
            this.caravan = caravan;
            this.enemy = enemy;
        }

        public IEnumerable<string> ProcessEffect(IEnumerable<Effect> effects, ActorBase source)
        {
            return effects.SelectMany(effect => ProcessEffect(new Effect(effect), source));
        }

        public IEnumerable<string> ProcessEffect(Effect effect, ActorBase source)
        {
            return GetTarget(effect.Target, source).Select(target =>
            {
                if(target == null)
                {
                    return "No valid target.";
                }

                var conditionLog = "";
                if(effect.Condition != null)
                {
                    foreach (var condition in effect.Condition)
                    {
                        switch (condition.Condition)
                        {
                            case Condition.Synergy:
                                if (!caravan.activeCaravanActor
                                    || caravan.activeCaravanActor.Class.ToString() != condition.Value)
                                {
                                    return "Synergy condition not met.";
                                }
                                conditionLog += "Synergy: ";
                                break;
                            case Condition.EnemyStatus:
                                if (enemy.GetCurrentEnemy() == null
                                    || !enemy.GetCurrentEnemy().Instance.HasStatus(condition.Value))
                                {
                                    return "Status condition not met.";
                                }
                                conditionLog += "Status: ";
                                break;
                        }
                    }
                }

                effect = target.OnEffectTarget(effect);

                var effectLog = "";
                switch (effect.Type)
                {
                    case EffectType.GainAction:
                        this.hand.remainingActions += effect.Amount;
                        effectLog = $"Gained {effect.Amount} actions.";
                        break;
                    case EffectType.Draw:
                        this.hand.DrawCard(effect.Amount);
                        effectLog = $"Draw {effect.Amount}.";
                        break;
                    case EffectType.StatusDurationBased:
                    case EffectType.StatusIntensityBased:
                        var status = StatusBase.Create(effect.StatusId);
                        status.IncreaseIntensity(effect.Amount);
                        target.ApplyStatus(status);
                        effectLog = $"{target.Name} now has {effect.StatusId} damage";
                        break;
                    case EffectType.Damage:
                        target.Damage(effect.Amount);
                        effectLog = $"{target.Name} took {effect.Amount} damage";
                        break;
                    case EffectType.Heal:
                        target.Heal(effect.Amount);
                        effectLog = $"{target.Name} was healed {effect.Amount} damage";
                        break;
                    case EffectType.CanSwapAgain:
                        hand.swapAvailable = true;
                        effectLog = $"Swap refreshed";
                        break;
                    default:
                        Debug.LogError($"Effect not implemented: {JsonUtility.ToJson(effect)}");
                        return "No action";
                }

                return $"{conditionLog}{effectLog}";
            }).ToList();
        }

        private IEnumerable<ActorBase> GetTarget(EffectTarget target, ActorBase source)
        {
            switch(target)
            {
                case EffectTarget.Self:
                    yield return source;
                    break;
                case EffectTarget.EnemyActive:
                    yield return enemy.GetCurrentEnemy().Instance;
                    break;
                case EffectTarget.CaravanActive:
                    yield return caravan.activeCaravanActor;
                    break;
                case EffectTarget.Caravan:
                    foreach(var item in caravan.caravan)
                        yield return item;
                    break;
                case EffectTarget.CaravanActiveOrCaravan:
                    yield return caravan.GetFirstAvailablePlayerTarget();
                    break;
                case EffectTarget.CaravanHighestHealth:
                    yield return caravan.GetHighestHealthCaravanMember();
                    break;
                case EffectTarget.CaravanLowestHealth:
                    yield return caravan.GetLowestHealthCaravanMember();
                    break;
                default:
                    Debug.LogError($"Target not implemented: {target.ToString()}");
                    yield break;
            }
        }
    }
}
