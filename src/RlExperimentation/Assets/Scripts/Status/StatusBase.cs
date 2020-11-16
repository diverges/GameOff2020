using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Status
{
    public abstract class StatusBase
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        /// <summary>
        /// At 0 intensity a boon is considered to be expired.
        /// </summary>
        public int Intensity { get; protected set; }

        public bool IsExpired { get => Intensity > 0; }

        public abstract void IncreaseIntensity(int amount);

        public abstract bool DecrementIntensity(int amount);

        /// <summary>
        /// On turn start of actor, ensure this boon is properly decremented.
        /// </summary>
        /// <param name="source"></param>
        public abstract void OnActorTurnStart(ActorBase source);

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
    }
}
