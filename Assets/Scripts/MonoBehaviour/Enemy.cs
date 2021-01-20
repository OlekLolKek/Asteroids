using System;
using UnityEngine;


namespace DefaultNamespace
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnEnemyHit = delegate {  };
        public static IEnemyFactory Factory;
        private Transform _poolRoot;
        private Health _health;

        public Health Health
        {
            get
            {
                if (_health.Current <= 0.0f)
                {
                    ReturnToPool();
                }

                return _health;
            }
            protected set => _health = value;
        }

        public Transform PoolRoot
        {
            get
            {
                if (_poolRoot == null)
                {
                    var find = GameObject.Find(NameManager.POOL_ASTEROIDS);
                    _poolRoot = find == null ? null : find.transform;
                }

                return _poolRoot;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(TagManager.BULLET_TAG))
            {
                OnEnemyHit.Invoke(this);
            }
        }

        public static Asteroid CreateAsteroidEnemy(Health hp)
        {
            var enemy = Instantiate(Resources.Load<Asteroid>(PathManager.ENEMY_ASTEROID_PATH));
            enemy.Health = hp;
            return enemy;
        }

        public static Enemy CreateAsteroidEnemyWithPool(EnemyPool enemyPool, Health hp)
        {
            var enemy = enemyPool.GetEnemy(EnemyTypes.Asteroid);
            enemy.transform.position = Vector3.one;
            enemy.gameObject.SetActive(true);
            enemy._health = hp;

            return enemy;
        }

        private void ActivateEnemy(Vector3 position, Quaternion rotation)
        {
            transform.localPosition = position;
            transform.localRotation = rotation;
            gameObject.SetActive(true);
            transform.SetParent(null);
        }

        protected void ReturnToPool()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            gameObject.SetActive(false);
            transform.SetParent(PoolRoot);

            if (!PoolRoot)
            {
                Destroy(gameObject);
            }
        }
    }
}