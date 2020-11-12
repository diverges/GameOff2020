namespace Assets.Scripts
{
    public abstract class EnemyBase
    {
        public EnemyBase()
        {
            CurrentHealth = MaxHealth;
        }

        public abstract string Name { get; }

        public abstract int MaxHealth { get; }

        public int CurrentHealth;

        public abstract string Intent { get; }

        public virtual BoardState OnEnter(BoardState state, ActorBase previous)
        {
            return state;
        }

        public virtual BoardState OnExit(BoardState state, ActorBase next)
        {
            return state;
        }

        public virtual BoardState OnPrepare(BoardState state)
        {
            return state;
        }
    }
}
