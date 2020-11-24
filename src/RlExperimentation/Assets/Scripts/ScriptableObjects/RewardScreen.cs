using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Screen", menuName = "Screen/Reward", order = 4)]
    public class RewardScreen : Screen
    {
        public List<ActorBase> member;
    }
}
