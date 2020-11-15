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

                switch (effect.Type)
                {
                    case EffectType.Damage:
                        target.Damage(effect.Amount);
                        return $"{target.Name} took {effect.Amount} damage";
                    case EffectType.Heal:
                        target.Heal(effect.Amount);
                        return $"{target.Name} was healed {effect.Amount} damage";
                    default:
                        Debug.LogError($"Effect not implemented: {JsonUtility.ToJson(effect)}");
                        return "No action";
                }
            }).ToList();
        }

        private IEnumerable<ActorBase> GetTarget(EffectTarget target)
        {
            switch(target)
            {
                case EffectTarget.CaravanActiveOrCaravan:
                    yield return caravan.GetFirstAvailablePlayerTarget();
                    break;
                case EffectTarget.EnemyActive:
                    yield return enemy.GetCurrentEnemy().Instance;
                    break;
                case EffectTarget.CaravanHighestHealth:
                    yield return caravan.GetHighestHealthCaravanMember();
                    break;
                default:
                    Debug.LogError($"Target not implemented: {target.ToString()}");
                    yield break;
            }
        }
    }
}
