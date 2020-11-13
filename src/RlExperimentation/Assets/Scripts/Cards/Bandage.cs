using System;
using Assets.Scripts.Core;


namespace Assets.Scripts.Cards
{
    public class Bandage : CardBase
    {
        public override string Name => "Bandage";

        public override string Description => "Heal active member for <b>2</b>. <i>Synergy</i> Medic, heal caravan members for <b>2</b>.";

        public override BoardState OnPlay(BoardState state)
        {
            //if(state.activeCaravanMember != null)
            //{
            //    state.activeCaravanMember.CurrentHealth = Math.Min(state.activeCaravanMember.CurrentHealth+2, state.activeCaravanMember.MaxHealth);
            //    if (state.activeCaravanMember.Name == "Medic")
            //    {
            //        state.caravan.ForEach(member => member.CurrentHealth = Math.Min(member.CurrentHealth + 2, member.MaxHealth));
            //    }
            //}
            return state;
        }
    }
}
