using UnityEngine;


namespace DefaultNamespace
{
    public sealed class AsteroidFactory : IEnemyFactory
    {
        private GameObject _instance;
    
        private bool _created;

        public (GameObject, EnemyCollision, Rigidbody2D) Create(EnemyData data)
        {
            if (!_created)
            {
                _instance = new GameObject(NameManager.ASTEROID);

                _instance.AddComponent<SpriteRenderer>().sprite = data.AsteroidSprite;

                var rigidbody = _instance.AddComponent<Rigidbody2D>();
                rigidbody.gravityScale = 0.0f;
                rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                rigidbody.mass = data.Mass;

                var collider = _instance.AddComponent<CircleCollider2D>();
                collider.radius = data.ColliderRadius;

                var enemyCollision = _instance.AddComponent<EnemyCollision>();

                _instance.tag = TagManager.ENEMY_TAG;

                _instance.transform.localScale = new Vector3(data.SpriteScale, data.SpriteScale);

                _created = true;
                
                return (_instance, enemyCollision, rigidbody);
            }
            else
            {
                _instance = Object.Instantiate(_instance);

                var enemyCollision = _instance.GetComponent<EnemyCollision>();
                var rigidbody2D = _instance.GetComponent<Rigidbody2D>();

                return (_instance, enemyCollision, rigidbody2D);
            }
        }
    }
}