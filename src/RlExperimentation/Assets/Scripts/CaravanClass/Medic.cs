using System.Linq;
using UnityEngine;

namespace Assets.Scripts.CaravanClass
{
    public class Medic : ActorBase
    {
        public override string Name => "Medic";

        public override int MaxHealth => 20;

        public override BoardState OnEnter(BoardState state)
        {
            var lowestHealthMember = state.caravan
                .Aggregate<ActorBase>((lowest, next) => next.CurrentHealth < lowest.CurrentHealth ? lowest: next);
            if (lowestHealthMember != null)
            {
                Debug.Log($"Medic healed {lowestHealthMember.Name}");
                lowestHealthMember.CurrentHealth += 2;
            }
            return state;
        }

        public override BoardState OnExit(BoardState state)
        {
            // TODO: Requires buff
            return state;
        }

        public override BoardState OnPrepare(BoardState state)
        {
            foreach(var member in state.caravan)
            {
                if (member.CurrentHealth < member.MaxHealth)
                {
                    Debug.Log($"Medic healed {member.Name}");
                    member.CurrentHealth++;
                }
            }

            return state;
        }
    }
}
