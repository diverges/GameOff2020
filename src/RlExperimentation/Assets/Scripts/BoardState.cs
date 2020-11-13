using Assets.Scripts.Core;
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

        public List<EnemyBase> enemyPack;
        public EnemyBase enemy;

        public Deck deck;
        public List<CardBase> hand;
    }
}
