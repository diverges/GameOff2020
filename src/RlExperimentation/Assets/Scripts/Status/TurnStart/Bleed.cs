using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Status.TurnStart
{
    class Bleed : StatusBase
    {
        public override string Name => "Bleed";

        public override string Description => $"Deal {this.Intensity} damage on round start.";

        public override void OnActorTurnStart(ActorBase source)
        {
            Debug.Log($"{source.Name} took {this.Intensity} bleed damage.");
            source.Damage(this.Intensity);
        }
    }
}
