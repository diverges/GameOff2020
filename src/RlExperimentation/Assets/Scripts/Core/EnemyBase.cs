using System;

namespace Assets.Scripts.Core
{
    public abstract class EnemyBase : ActorBase
    {
        public EnemyBase() : base()
        {
        }

        public abstract string Intent { get; }
    }
}
