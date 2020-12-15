using UnityEngine;

namespace DefaultNamespace
{
    internal sealed class AsteroidFactory : IEnemyFactory
    {
        public EnemyController Create(Health hp, EnemyData data)
        {
            var enemy = new Asteroid();
            enemy.Instance.AddComponent<SpriteRenderer>().sprite = data.AsteroidSprite;
            enemy.DependencyInjectHealth(data.Health);
            return enemy;
        }
    }
}