using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Cards
{
    public class Bandage : CardBase
    {
        public override string Name => "Bandage";

        public override string Description => "Heal active member for 2.";

        public override BoardState OnPlay(BoardState state)
        {
            if(state.activeCaravanMember != null)
            {
                state.activeCaravanMember.CurrentHealth += 3;
            }
            return state;
        }
    }
}
