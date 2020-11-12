using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    [System.Serializable]
    public enum TurnState
    {
        EnemyPrepare,
        EnemyAct,
        PlayerDraw,
        PlayerPrepare,
        PlayerAct,
        PlayerEnd,
        End
    }

    [System.Serializable]
    public class BoardState
    {
        public TurnState turnState;

        public List<ActorBase> caravan;
        public ActorBase activeCaravanMember;


        public List<ActorBase> enemyPack;
        public ActorBase enemy;

        public Deck deck;
        public List<CardBase> hand;

        public ActorBase GetFirstAvailablePlayerTarget()
        {
            if(activeCaravanMember != null)
            {
                return activeCaravanMember;
            }

            return caravan.Where(member => member.CurrentHealth > 0).First();
        }
    }
}
