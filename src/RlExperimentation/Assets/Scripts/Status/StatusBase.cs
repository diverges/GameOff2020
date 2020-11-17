using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Status.Source;
using Assets.Scripts.Status.Target;
using Assets.Scripts.Status.TurnStart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Status
{
    public enum StatusId
    {
        None,
        // Per Turn
        Bleed = 0,

        // As Source
        Weaken = 100,
        Empowered,

        // As Target
        Protected = 200,
        Pierce
    }

    public abstract class StatusBase
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        /// <summary>
        /// At 0 intensity a boon is considered to be expired.
        /// </summary>
        public int Intensity { get; protected set; }

        public virtual bool IsExpired { get => Intensity > 0; }

        public virtual void IncreaseIntensity(int amount)
        {
            this.Intensity += amount;
        }

        public virtual bool DecrementIntensity(int amount)
        {
            this.Intensity -= amount;
            return this.IsExpired;
        }

        /// <summary>
        /// On turn start of actor, ensure this boon is properly decremented.
        /// </summary>
        /// <param name="source"></param>
        public virtual void OnActorTurnStart(ActorBase source)
        {
            this.DecrementIntensity(1);
        }

        /// <summary>
        /// Modifiers to card played and enemy action from source (active enemy or caravan member).
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        public virtual Effect OnEffectSource(Effect effect)
        {
            return effect;
        }

        /// <summary>
        /// Effect trigger on actor this boon is attached to.
        /// </summary>
        /// <param name="effect"></param>
        /// <returns>Modified effect</returns>
        public virtual Effect OnEffectTarget(Effect effect)
        {
            return effect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static StatusBase Create(StatusId type)
        {
            switch(type)
            {
                case StatusId.Bleed:
                    return new Bleed();
                case StatusId.Empowered:
                    return new Empower();
                case StatusId.Weaken:
                    return new Weaken();
                case StatusId.Pierce:
                    return new Pierce();
                case StatusId.Protected:
                    return new Protected();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
