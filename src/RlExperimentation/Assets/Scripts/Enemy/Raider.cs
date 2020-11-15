using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Raider : EnemyBase
    {
        private bool takeAimBuf = false;
        private int intent = Random.Range(0, 10);

        public override List<Effect> Intent => new List<Effect>();

        public override string IntentName => "TakeAim!";

        public override string ActorName => "Raider";

        //public override string Intent {
        //    get {
        //        var baseDamage = (takeAimBuf) ? 4 : 0;

        //        if (intent >= 5)
        //        {
        //            return $"<b>Take Aim!</b>\r\nDeal <b>{baseDamage+3}</b> damage. Next turn deal <b>+2</b> damage.";
        //        }
        //        return $"<b>Fire!</b>\r\nDeal <b>{baseDamage+7}</b> damage.";
        //    }
        //}
    }
}
