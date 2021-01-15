using UnityEngine;


namespace DefaultNamespace
{
    public sealed class AsteroidFactory : IEnemyFactory
    {
        public GameObject Create(EnemyData data)
        {
            var enemy = new GameObject(NameManager.ASTEROID);

            enemy.AddComponent<SpriteRenderer>().sprite = data.AsteroidSprite;

            var rigidbody = enemy.AddComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0.0f;
            rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            var collider = enemy.AddComponent<CircleCollider2D>();
            collider.radius = data.ColliderRadius;

            enemy.AddComponent<EnemyCollision>();

            enemy.tag = TagManager.ENEMY_TAG;

            enemy.transform.localScale = new Vector3(data.SpriteScale, data.SpriteScale);
            
            return enemy;
        }
    }
}