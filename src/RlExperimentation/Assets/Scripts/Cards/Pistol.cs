namespace Assets.Scripts.Cards
{
    public class Pistol : CardBase
    {
        public override string Name => "Pistol";

        public override string Description => "Deal <b>3</b> damage. <i>Synergy</i> Pistoleer, deal <b>2</b> damage.";

        public override BoardState OnPlay(BoardState state)
        {
            if(state.enemy != null)
            {
                state.enemy.CurrentHealth -= 3;
                if (state.activeCaravanMember != null && state.activeCaravanMember.Name == "Pistoleer")
                {
                    state.enemy.CurrentHealth -= 2;
                }
            }
            return state;
        }
    }
}
