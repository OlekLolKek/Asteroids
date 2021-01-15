using UnityEngine;

namespace DefaultNamespace
{
    public interface IEnemyFactory
    {
        GameObject Create(EnemyData data);
    }
}