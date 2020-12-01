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
        protected ActorBase instance;

        public ActorBase Instance { get => instance; }

        public string ActorName { get; set; }

        public string IntentName { get; protected set; }

        public List<Effect> Intent { get; protected set; } = new List<Effect>();

        /// <summary>
        /// Update Intent and IntentName
        /// </summary>
        public abstract void Think();

        public static EnemyBase Create(string actorName)
        {
            EnemyBase obj;
            switch(actorName)
            {
                case "Legion Armor":
                    obj = new Raider();
                    obj.ActorName = "Diseased Scavenger";
                    break;
                case "Raider":
                    obj = new Raider();
                    break;
                case "Diseased Scavenger":
                    obj = new Scavenger();
                    obj.ActorName = "Diseased Scavenger";
                    break;
                case "Weak Scavenger":
                    obj = new WeakScavenger();
                    break;
                case "Raider Scout":
                    obj =  new RaiderScout();
                    break;
                case "Scavenger":
                    obj = new Scavenger();
                    break;
                default:
                    throw new MissingComponentException();
            }
            obj.instance = Object.Instantiate(EnemyBase.actors[obj.ActorName]);
            return obj;
        }
    }
}
