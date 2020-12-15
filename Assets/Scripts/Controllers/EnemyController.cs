using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyController
    {
        public static IEnemyFactory Factory;
        private GameObject _instance;
        private Transform _poolRoot;
        private Health _health;

        public GameObject Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject();
                }

                return _instance;
            }
        }

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
                    var find = GameObject.Find(NameManager.POOL_AMMUNITION);
                    _poolRoot = find == null ? null : find.transform;
                }

                return _poolRoot;
            }
        }

        public static EnemyController CreateAsteroidEnemyWithPool(EnemyPool enemyPool, Health hp)
        {
            var enemy = enemyPool.GetEnemy(EnemyTypes.Asteroid);
            enemy.Instance.transform.position = Vector3.one;
            enemy.Instance.SetActive(true);
            enemy._health = hp;

            return enemy;
        }

        private void ActivateEnemy(Vector3 position, Quaternion rotation)
        {
            Instance.transform.localPosition = position;
            Instance.transform.localRotation = rotation;
            Instance.SetActive(true);
            Instance.transform.SetParent(null);
        }

        private void ReturnToPool()
        {
            Instance.transform.localPosition = Vector3.zero;
            Instance.transform.localRotation = Quaternion.identity;
            Instance.SetActive(false);
            Instance.transform.SetParent(PoolRoot);

            if (!PoolRoot)
            {
                Object.Destroy(Instance);
            }
        }
    }
}