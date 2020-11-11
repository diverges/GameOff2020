using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Cards
{
    public class Pistol : CardBase
    {
        public override string Name => "Pistol";

        public override string Description => "Deal <b>3</b> damage.";

        public override BoardState OnPlay(BoardState state)
        {
            if(state.enemy != null)
            {
                state.enemy.CurrentHealth -= 3;
            }
            return state;
        }
    }
}
