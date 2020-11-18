using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ScriptableObjects
{
    public enum EffectTarget
    {
        EnemyActive = 0,
        EnemyPack,
        EnemyActiveAndPack,

        CaravanActive = 100,
        CaravanActiveOrCaravan,
        Caravan,
        CaravanAndActive,
        CaravanLowestHealth,
        CaravanHighestHealth,
        Self
    }
}
