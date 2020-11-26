using Assets.Scripts.ScriptableObjects;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemy;

namespace Assets.Scripts.Core
{
    public abstract class EnemyBase
    {
        private static Dictionary<string, ActorBase> actors = Resources.LoadAll<ActorBase>("Enemies").ToDictionary(item => item.Name, item => item);
        private ActorBase instance;

        public EnemyBase()
        {
            this.instance = Object.Instantiate(EnemyBase.actors[ActorName]);
        }

        public ActorBase Instance { get => instance; }

        public abstract string ActorName { get; }

        public string IntentName { get; protected set; }

        public List<Effect> Intent { get; protected set; } = new List<Effect>();

        /// <summary>
        /// Update Intent and IntentName
        /// </summary>
        public abstract void Think();

        public static EnemyBase Create(string actorName)
        {
            switch(actorName)
            {
                case "Raider":
                    return new Raider();
                default:
                    throw new MissingComponentException();
            }
        }
    }
}
