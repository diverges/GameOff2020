using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyManager : MonoBehaviour
    {
        public EnemyView enemySpawn;

        private List<EnemyBase> enemyPack;
        private EnemyBase enemy;

        public EnemyManager()
        {
            enemyPack = new List<EnemyBase>();
            enemy = null;
        }

        public void OnEnemyTurnStart()
        {
            if (this.enemy != null)
            {
                this.enemy.Instance.OnActorTurnStart();
            }
            foreach (var member in enemyPack)
            {
                member.Instance.OnActorTurnStart();
            }
        }

        public void SetEnemyPack(List<EnemyBase> enemyPack)
        {
            this.enemyPack = enemyPack;
            enemy = null;
        }

        public EnemyBase GetCurrentEnemy() => enemy;

        public int GetReminingEnemyCount()
        {
            var packSize = enemyPack.Count();
            return (enemy != null) ? 1 + packSize : packSize;
        }

        public bool IsActiveEnemyAlive() => enemy != null && enemy.Instance.CurrentHealth > 0;

        public bool TrySpawnNextEnemy(out EnemyBase result)
        {
            result = null;
            if (enemyPack.Any())
            {
                enemy = enemyPack.First();
                enemy.Instance.animations = GetComponentInChildren<ActorAnimations>();
                enemySpawn.actor = enemy;
                enemyPack.RemoveAt(0);
                result = enemy;
                return true;
            }
            return false;
        }
    }
}
