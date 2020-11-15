using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ScriptableObjects
{
    public enum EffectTarget
    {
        EnemyActive,
        EnemyPack,
        EnemyActiveAndPack,
        CaravanActive,
        CaravanActiveOrCaravan,
        Caravan,
        CaravanAndActive,
        CaravanLowestHealth,
        NextActive,
        CaravanHighestHealth
    }
}
