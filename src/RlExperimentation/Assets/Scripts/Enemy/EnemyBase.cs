using Assets.Scripts.ScriptableObjects;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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

        public abstract string IntentName { get; }

        public abstract List<Effect> Intent { get; }
    }
}
