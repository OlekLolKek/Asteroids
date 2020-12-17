using System;
using System.Collections;
using UniRx;
using UnityEngine;


namespace DefaultNamespace
{
    public sealed class AsteroidController : IExecutable
    {
        private readonly Transform _playerTransform;
        private readonly EnemyPool _pool;
        private readonly float _spawnTime;
        private float _timer;

        private IDisposable _coroutine;
        
        public AsteroidController(EnemyData enemyData, PlayerModel playerModel, AsteroidFactory asteroidFactory)
        {
            //TODO: Заменить число на поле
            _pool = new EnemyPool(enemyData.AsteroidPoolSize, enemyData, asteroidFactory);
            _spawnTime = enemyData.EnemyTimer;
            _playerTransform = playerModel.Transform;
        }
    
        public void Execute(float deltaTime)
        {
            SpawnAsteroids(deltaTime);
        }

        private void SpawnAsteroids(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer >= _spawnTime)
            {
                _timer = 0.0f;
                var enemy = _pool.GetEnemy(EnemyTypes.Asteroid);
                var position = _playerTransform.position;
                //TODO: Заменить число на поле
                enemy.transform.position = new Vector3(position.x, position.y + 15.0f);
                enemy.gameObject.SetActive(true);
                enemy.OnEnemyHit += OnAsteroidHit;
                _coroutine = ReturnToPool(enemy, _spawnTime).ToObservable().Subscribe();
            }
        }

        private void OnAsteroidHit(Enemy asteroid)
        {
            asteroid.OnEnemyHit -= OnAsteroidHit;
            if (asteroid.transform)
                _pool.ReturnToPool(asteroid.transform);
            _coroutine.Dispose();
        }
        
        private IEnumerator ReturnToPool(Enemy asteroid, float delay)
        {
            yield return new WaitForSeconds(delay);
            asteroid.OnEnemyHit -= OnAsteroidHit;
            if (asteroid.gameObject.activeSelf) 
                _pool.ReturnToPool(asteroid.transform);
        }
    }
}