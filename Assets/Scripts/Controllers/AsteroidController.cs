using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public sealed class AsteroidController : IExecutable
    {
        private Transform _playerTransform;
        private EnemyPool _pool;
        private float _timer;
        private float _enemyTimer;
        
        public AsteroidController(EnemyData enemyData, PlayerModel playerModel, AsteroidFactory asteroidFactory)
        {
            //TODO: Заменить число на поле
            _pool = new EnemyPool(2, enemyData, asteroidFactory);
            _enemyTimer = enemyData.EnemyTimer;
            _playerTransform = playerModel.Transform;
        }
    
        public void Execute(float deltaTime)
        {
            SpawnAsteroids(deltaTime);
        }

        private void SpawnAsteroids(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer >= _enemyTimer)
            {
                _timer = 0.0f;
                var enemy = _pool.GetEnemy(EnemyTypes.Asteroid);
                var position = _playerTransform.position;
                //TODO: Заменить число на поле
                enemy.transform.position = new Vector3(position.x, position.y + 15.0f);
                enemy.gameObject.SetActive(true);
                var coroutine = ReturnToPool(enemy.transform, _enemyTimer).ToObservable().Subscribe();
            }
        }
        
        private IEnumerator ReturnToPool(Transform asteroid, float delay)
        {
            yield return new WaitForSeconds(delay);
            _pool.ReturnToPool(asteroid);
        }
    }
}