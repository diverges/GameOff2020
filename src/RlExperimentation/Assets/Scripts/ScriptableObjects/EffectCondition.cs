using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ScriptableObjects
{
    public enum Condition
    {
        NoOp,
        Synergy
    }

    [Serializable]
    public class EffectCondition
    {
        public Condition Condition;
        public string Value;
    }
}
