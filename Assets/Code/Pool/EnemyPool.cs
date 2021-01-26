using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;
using Object = UnityEngine.Object;


namespace DefaultNamespace
{
    public class EnemyPool
    {
        public event Action<BaseEnemyController> OnEnemyKilledAndReturned = delegate(BaseEnemyController controller) {  };
        
        private readonly Dictionary<EnemyTypes, HashSet<BaseEnemyController>> _enemyPool;
        private readonly IEnemyFactory _factory;
        private readonly EnemyData _enemyData;
        private readonly Transform _poolRoot;
        private readonly int _poolCapacity;
        private int _id;


        public EnemyPool(int poolCapacity, EnemyData enemyData, IEnemyFactory factory)
        {
            _factory = factory;
            _enemyPool = new Dictionary<EnemyTypes, HashSet<BaseEnemyController>>();
            _poolCapacity = poolCapacity;
            _enemyData = enemyData;
            if (!_poolRoot)
            {
                _poolRoot = new GameObject(NameManager.POOL_ASTEROIDS).transform;
            }
        }

        public BaseEnemyController GetEnemy(EnemyTypes type)
        {
            BaseEnemyController result;
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

        private HashSet<BaseEnemyController> GetListEnemies(EnemyTypes type)
        {
            if (!_enemyPool.ContainsKey(type))
            {
                _enemyPool[type] = new HashSet<BaseEnemyController>();
            }

            return _enemyPool[type];
        }

        private BaseEnemyController GetAsteroid(HashSet<BaseEnemyController> enemies)
        {
            var enemy = enemies.FirstOrDefault(a => !a.IsActive);
            
            if (enemy == null)
            {
                for (int i = 0; i < _poolCapacity; i++)
                {
                    var newEnemy = new BaseEnemyController(_enemyData, _factory);
                    newEnemy.ID = _id++;
                    ReturnToPool(newEnemy);
                    enemies.Add(newEnemy);
                }
                
                GetAsteroid(enemies);
            }

            enemy = enemies.FirstOrDefault(a => !a.IsActive);
            return enemy;
        }

        public void ReturnToPool(BaseEnemyController enemy)
        {
            enemy.ReturnToPool(_poolRoot);
        }

        public void ReturnKilledToPool(BaseEnemyController enemy)
        {
            enemy.ReturnToPool(_poolRoot);
            OnEnemyKilledAndReturned.Invoke(enemy);
        }

        public void DeletePool()
        {
            Object.Destroy(_poolRoot.gameObject);
        }
    }
}