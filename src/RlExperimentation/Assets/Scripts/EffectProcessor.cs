using Assets.Scripts.ScriptableObjects;

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

        public void ProcessEffect(Effect effect)
        {
            switch(effect.Type)
            {
                default:
                    break;
            }
        }
    }
}
