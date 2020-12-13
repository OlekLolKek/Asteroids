using UnityEngine;

namespace DefaultNamespace
{
    internal sealed class AsteroidFactory : IEnemyFactory
    {
        public EnemyController Create(Health hp)
        {
            var enemy = new Asteroid();
            enemy.DependencyInjectHealth(hp);
            return enemy;
        }
    }
}