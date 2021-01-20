using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace DefaultNamespace
{
    public class EnemyPool
    {
        private readonly Dictionary<EnemyTypes, HashSet<Enemy>> _enemyPool;
        private IEnemyFactory _factory;
        private readonly int _poolCapacity;
        private readonly EnemyData _enemyData;
        private readonly Transform _poolRoot;


        public EnemyPool(int poolCapacity, EnemyData enemyData, IEnemyFactory factory)
        {
            _factory = factory;
            _enemyPool = new Dictionary<EnemyTypes, HashSet<Enemy>>();
            _poolCapacity = poolCapacity;
            _enemyData = enemyData;
            if (!_poolRoot)
            {
                _poolRoot = new GameObject(NameManager.POOL_ASTEROIDS).transform;
            }
        }

        public Enemy GetEnemy(EnemyTypes type)
        {
            Enemy result;
            switch (type)
            {
                case EnemyTypes.Asteroid:
                    result = GetAsteroid(GetListEnemies(type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Не предусмотрен в программе");
            }

            return result;
        }

        private HashSet<Enemy> GetListEnemies(EnemyTypes type)
        {
            return _enemyPool.ContainsKey(type) ? _enemyPool[type] : _enemyPool[type] = new HashSet<Enemy>();
        }

        private Enemy GetAsteroid(HashSet<Enemy> enemies)
        {
            var enemy = enemies.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (enemy == null)
            {
                var prefab = Resources.Load<Asteroid>(PathManager.ENEMY_ASTEROID_PATH);
                for (int i = 0; i < _poolCapacity; i++)
                {
                    var instantiate = Object.Instantiate(prefab);
                    ReturnToPool(instantiate.transform);
                    enemies.Add(instantiate);
                }

                GetAsteroid(enemies);
            }

            enemy = enemies.FirstOrDefault(a => !a.gameObject.activeSelf);
            return enemy;
        }

        public void ReturnToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.SetParent(_poolRoot);
        }

        public void DeletePool()
        {
            Object.Destroy(_poolRoot.gameObject);
        }
    }
}