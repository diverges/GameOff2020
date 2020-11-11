using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CaravanManager : MonoBehaviour
    {
        public GameObject[] orderedCaravanSpawns = new GameObject[4];
        public GameObject activeCaravanMember;

        void Update()
        {
            foreach(var spawn in orderedCaravanSpawns)
            {
                var spawnMember = spawn.GetComponent<CaravanMember>();
                spawn.SetActive(spawnMember.actor != null);
            }
            var activeMember = activeCaravanMember.GetComponent<CaravanMember>();
            activeCaravanMember.SetActive(activeMember.actor != null);
        }
    }
}
