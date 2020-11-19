using Assets.Scripts.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "DefaultActor", menuName = "Member", order = 2)]
    public class ActorBase : ScriptableObject
    {
        private int currentHealth;

        public ActorBase()
        {
            currentHealth = MaxHealth;
        }

        public void OnEnable()
        {
            currentHealth = MaxHealth;
        }

        public string Name;

        [TextArea]
        public string Description;

        public CaravanClass Class;

        public int MaxHealth;

        public List<CardBase> Backpack;

        public int CurrentHealth
        {
            get => currentHealth;
        }

        public List<Effect> OnEnter;

        public List<Effect> OnExit;

        public List<Effect> OnPrepare;

        private readonly Dictionary<string, StatusBase> statusCollection = new Dictionary<string, StatusBase>();

        public IEnumerable<StatusBase> GetStatus() => statusCollection.Values;

        public bool HasStatus(string status) => statusCollection.ContainsKey(status);

        public void ApplyStatus(StatusBase status)
        {
            if (statusCollection.ContainsKey(status.Name))
            {
                statusCollection[status.Name].IncreaseIntensity(status.Intensity);
                return;
            }
            statusCollection.Add(status.Name, status);
        }

        public void OnActorTurnStart()
        {
            var expiredKeys = new List<string>();
            foreach (var key in statusCollection.Keys)
            {
                statusCollection[key].OnActorTurnStart(this);
                if (statusCollection[key].IsExpired)
                {
                    expiredKeys.Add(key);
                }
            }
            foreach (var key in expiredKeys)
                statusCollection.Remove(key);
        }

        public Effect OnEffectSource(Effect effect)
        {
            foreach(var entry in statusCollection)
            {
                effect = entry.Value.OnEffectSource(effect);
            }
            return effect;
        }

        public Effect OnEffectTarget(Effect effect)
        {
            foreach (var entry in statusCollection)
            {
                effect = entry.Value.OnEffectTarget(effect);
            }
            return effect;
        }

        public void ClearStatus() => statusCollection.Clear();

        public void Heal(int amount)
        {
            this.currentHealth = Math.Min(this.currentHealth + amount, MaxHealth);
        }

        public void Damage(int amount)
        {
            this.currentHealth = Math.Max(this.currentHealth - amount, 0);
        }

        public List<Tuple<string, string>> GetTooltipDescription()
        {
            var name = (CaravanClass.None == this.Class) ? this.Name : $"{this.Name}, <i>{this.Class.ToString()}</i>";
            var result = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>(name, this.Description)
            };

            if(OnEnter.Any())
            {
                result.Add(new Tuple<string, string>("Tag In", GetEffectText(this.OnEnter)));
            }

            if (OnExit.Any())
            {
                result.Add(new Tuple<string, string>("Tag Out", GetEffectText(this.OnExit)));
            }

            if (OnPrepare.Any())
            {
                result.Add(new Tuple<string, string>("As Active", GetEffectText(this.OnPrepare)));
            }

            return result;
        }

        private string GetEffectText(List<Effect> effects)
        {
            return string.Join("\r\n", effects.Select(effect => effect.ToString()));
        }
    }
}
