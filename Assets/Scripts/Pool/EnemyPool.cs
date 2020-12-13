using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace DefaultNamespace
{
    public class EnemyPool
    {
        private readonly Dictionary<EnemyTypes, HashSet<EnemyController>> _enemyPool;
        private IEnemyFactory _factory;
        private readonly int _poolCapacity;
        private readonly EnemyData _enemyData;
        private readonly Transform _poolRoot;


        public EnemyPool(int poolCapacity, EnemyData enemyData, IEnemyFactory factory)
        {
            _factory = factory;
            _enemyPool = new Dictionary<EnemyTypes, HashSet<EnemyController>>();
            _poolCapacity = poolCapacity;
            _enemyData = enemyData;
            if (!_poolRoot)
            {
                _poolRoot = new GameObject(NameManager.POOL_AMMUNITION).transform;
            }
        }

        public EnemyController GetEnemy(EnemyTypes type)
        {
            EnemyController result;
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

        private HashSet<EnemyController> GetListEnemies(EnemyTypes type)
        {
            return _enemyPool.ContainsKey(type) ? _enemyPool[type] : _enemyPool[type] = new HashSet<EnemyController>();
        }

        private EnemyController GetAsteroid(HashSet<EnemyController> enemies)
        {
            var enemy = enemies.FirstOrDefault(a => !a.Instance.activeSelf);
            if (enemy == null)
            {
                var laser = _factory.Create(_enemyData.Health);
                for (int i = 0; i < _poolCapacity; i++)
                {
                    var instantiate = laser;
                    ReturnToPool(instantiate.Instance.transform);
                    enemies.Add(instantiate);
                }

                GetAsteroid(enemies);
            }

            enemy = enemies.FirstOrDefault(a => !a.Instance.activeSelf);
            return enemy;
        }

        private void ReturnToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.SetParent(_poolRoot);
        }

        public void RemovePool()
        {
            Object.Destroy(_poolRoot.gameObject);
        }
    }
}