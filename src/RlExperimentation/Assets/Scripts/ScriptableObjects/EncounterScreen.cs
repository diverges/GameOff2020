using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Screen", menuName = "Screen/Encounter", order = 3)]
    public class EncounterScreen : Screen
    {
        public List<ActorBase> EnemyPack;
    }
}
