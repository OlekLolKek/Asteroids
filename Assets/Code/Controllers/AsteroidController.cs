using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace DefaultNamespace
{
    public sealed class AsteroidController : IExecutable
    {
        private readonly List<IDisposable> _coroutines = new List<IDisposable>();
        private readonly List<BaseEnemyController> _asteroids = new List<BaseEnemyController>();
        private readonly Transform _playerTransform;
        private readonly EnemyPool _pool;
        private readonly float _spawnTime;
        private float _timer;

        private IDisposable _coroutine;
        
        public AsteroidController(EnemyData enemyData, PlayerModel playerModel, 
            AsteroidFactory asteroidFactory)
        {
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

                ManagePool(enemy);

                enemy.OnEnemyHit += OnAsteroidHit;
                enemy.Activate();
            }
        }

        private void ManagePool(BaseEnemyController enemy)
        {
            if (!_asteroids.Contains(enemy))
            {
                _asteroids.Add(enemy);
                var coroutine = ReturnToPool(enemy.ID, _spawnTime).
                    ToObservable().Subscribe();
                _coroutines.Add(coroutine);
                enemy.InjectPlayerTransform(_playerTransform);
            }
            else
            {
                _coroutines[enemy.ID] = ReturnToPool(enemy.ID, _spawnTime).
                    ToObservable().Subscribe();
            }
        }

        private void OnAsteroidHit(int id)
        {
            var asteroid = _asteroids[id];
            asteroid.OnEnemyHit -= OnAsteroidHit;
            if (asteroid.IsActive)
            {
                _pool.ReturnToPool(asteroid);
            }
                
            _coroutines[id].Dispose();
        }
        
        private IEnumerator ReturnToPool(int id, float delay)
        {
            yield return new WaitForSeconds(delay);

            var asteroid = _asteroids[id];
            asteroid.OnEnemyHit -= OnAsteroidHit;
            if (asteroid.IsActive)
            {
                _pool.ReturnToPool(asteroid);
            }
        }
    }
}