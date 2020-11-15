using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class CaravanManager : MonoBehaviour
    {
        // Caravan Members
        public List<ActorBase> caravan;
        public ActorBase activeCaravanActor;

        // Spawn Owbjects Locations
        public GameObject[] orderedCaravanSpawns = new GameObject[4];
        public GameObject activeCaravanSpawn;

        public CaravanManager()
        {
            caravan = new List<ActorBase>();
            activeCaravanActor = null;
        }

        /// <summary>
        /// Build Caravan from actors
        /// </summary>
        /// <param name="actors"></param>
        public void InitializeCaravan(List<ActorBase> actors)
        {
            // Create Caravan
            for (var index = 0; index < orderedCaravanSpawns.Length; ++index)
            {
                if (index >= actors.Count)
                {
                    orderedCaravanSpawns[index].SetActive(false);
                    return;
                }
                var spawnObjectData = orderedCaravanSpawns[index].GetComponent<CaravanMember>();
                spawnObjectData.SetCaravanMember(actors[index]);
                caravan.Add(actors[index]);
                orderedCaravanSpawns[index].SetActive(true);
            }
            activeCaravanActor = null;
        }

        /// <summary>
        /// Check if any caravan members remain
        /// </summary>
        /// <returns></returns>
        public bool HasRemainingMembers() =>
            activeCaravanActor != null || caravan.Any(member => member.CurrentHealth > 0);

        /// <summary>
        /// Set or Swap caravan member with active slot.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public (ActorBase prevActive, ActorBase curActive) SetActiveMember(CaravanMember target)
        {
            // Cleanup
            caravan.Remove(target.actor);
            if(activeCaravanActor != null)
            {
                caravan.Add(activeCaravanActor);
            }

            // Swap
            var temp = activeCaravanActor;
            var activeMember = activeCaravanSpawn.GetComponent<CaravanMember>();
            activeMember.SetCaravanMember(target.actor);
            activeCaravanActor = target.actor;
            target.SetCaravanMember(temp);
            return (temp, activeCaravanActor);
        }

        /// <summary>
        /// Remove dead caravan members
        /// </summary>
        public void CleanupCaravan()
        {
            for (var index = 0; index < orderedCaravanSpawns.Length; ++index)
            {
                var member = orderedCaravanSpawns[index].GetComponent<CaravanMember>();
                if (member.actor != null && member.actor.CurrentHealth <= 0)
                {
                    member.SetCaravanMember(null);
                    orderedCaravanSpawns[index].SetActive(false);
                    return;
                }
            }
            if (activeCaravanActor != null && activeCaravanActor.CurrentHealth <= 0)
            {
                var activeMember = activeCaravanSpawn.GetComponent<CaravanMember>();
                activeMember.SetCaravanMember(null);
                activeCaravanActor = null;
            }
        }

        public ActorBase GetFirstAvailablePlayerTarget()
        {
            if (activeCaravanActor != null)
            {
                return activeCaravanActor;
            }

            if(caravan.Any())
            {
                return caravan.First();
            }

            return null;
        }

        public ActorBase GetHighestHealthCaravanMember()
        {
            ActorBase result = null;
            foreach(var member in caravan)
            {
                if (result == null || member.CurrentHealth > result.CurrentHealth)
                {
                    result = member;
                }
            }
            return result;
        }

        public ActorBase GetLowestHealthCaravanMember()
        {
            ActorBase result = null;
            foreach (var member in caravan)
            {
                if (result == null || member.CurrentHealth < result.CurrentHealth)
                {
                    result = member;
                }
            }
            return result;
        }

        void Update()
        {
            foreach(var spawn in orderedCaravanSpawns)
            {
                var spawnMember = spawn.GetComponent<CaravanMember>();
                spawn.SetActive(spawnMember.actor != null);
            }
            var activeMember = activeCaravanSpawn.GetComponent<CaravanMember>();
            activeCaravanSpawn.SetActive(activeCaravanActor != null);
        }
    }
}
