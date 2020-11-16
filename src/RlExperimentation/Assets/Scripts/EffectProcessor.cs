using Assets.Scripts.ScriptableObjects;
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

        public IEnumerable<string> ProcessEffect(IEnumerable<Effect> effects)
        {
            return effects.SelectMany(effect => ProcessEffect(effect));
        }

        public IEnumerable<string> ProcessEffect(Effect effect)
        {
            return GetTarget(effect.Target).Select(target =>
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
                                if (!caravan.activeCaravanActor || caravan.activeCaravanActor.Class.ToString() != condition.Value)
                                {
                                    return "Synergy condition not met";
                                }
                                conditionLog += "Synergy: ";
                                break;
                        }
                    }
                }

                effect = target.OnEffectTarget(effect);

                var effectLog = "";
                switch (effect.Type)
                {
                    case EffectType.Damage:
                        target.Damage(effect.Amount);
                        effectLog = $"{target.Name} took {effect.Amount} damage";
                        break;
                    case EffectType.Heal:
                        target.Heal(effect.Amount);
                        effectLog = $"{target.Name} was healed {effect.Amount} damage";
                        break;
                    default:
                        Debug.LogError($"Effect not implemented: {JsonUtility.ToJson(effect)}");
                        return "No action";
                }

                return $"{conditionLog}{effectLog}";
            }).ToList();
        }

        private IEnumerable<ActorBase> GetTarget(EffectTarget target)
        {
            switch(target)
            {
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
