using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Enums;
using UniRx;
using UnityEngine;


namespace Controllers
{
    public sealed class AsteroidController : IExecutable
    {
        private readonly List<IDisposable> _coroutines = new List<IDisposable>();
        private readonly List<BaseEnemyController> _asteroids = new List<BaseEnemyController>();
        private readonly Transform _playerTransform;
        private readonly EnemyPool _pool;
        private readonly PointModel _pointModel;
        private readonly float _spawnTime;
        private readonly int _pointPerEnemy;
        
        private IDisposable _coroutine;
        private float _timer;

        
        public AsteroidController(EnemyData enemyData, PlayerModel playerModel, 
            PointModel pointModel, AsteroidFactory asteroidFactory,
            EnemyPool pool)
        {
            _pool = pool;
            _spawnTime = enemyData.EnemyTimer;
            _pointPerEnemy = enemyData.PointsPerEnemy;
            _playerTransform = playerModel.Transform;
            _pointModel = pointModel;
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

                enemy.OnEnemyKilled += OnAsteroidDestroyed;
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

        private void OnAsteroidDestroyed(BaseEnemyController enemy)
        {
            var asteroid = _asteroids[enemy.ID];
            asteroid.OnEnemyKilled -= OnAsteroidDestroyed;
            if (asteroid.IsActive)
            {
                _pool.ReturnKilledToPool(asteroid);
            }
                
            _pointModel.AddPoints(_pointPerEnemy);
            
            _coroutines[enemy.ID].Dispose();
        }
        
        private IEnumerator ReturnToPool(int id, float delay)
        {
            yield return new WaitForSeconds(delay);

            var asteroid = _asteroids[id];
            asteroid.OnEnemyKilled -= OnAsteroidDestroyed;
            if (asteroid.IsActive)
            {
                _pool.ReturnToPool(asteroid);
            }
        }
    }
}