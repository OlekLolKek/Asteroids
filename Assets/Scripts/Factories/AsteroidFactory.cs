using UnityEngine;

namespace DefaultNamespace
{
    public sealed class AsteroidFactory : IEnemyFactory
    {
        public Enemy Create(Health hp, EnemyData data)
        {
            var enemy = Object.Instantiate(Resources.Load<Asteroid>("Enemy/Asteroid"));
            enemy.DependencyInjectHealth(hp);
            return enemy;
        }
    }
}