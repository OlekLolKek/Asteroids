using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace
{
    public class BaseEnemyController
    {
        public event Action<int> OnEnemyKilled = delegate {  };
        
        private readonly GameObject _instance;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Vector2 _flyVelocity;
        
        private Transform _poolRoot;
        private Transform _player;
        private Health _health;
        
        private readonly float _yOffset;
        private readonly float _minX;
        private readonly float _maxX;


        public bool IsActive
        {
            get => _instance.activeSelf;
            private set => _instance.SetActive(value);
        }
        public int ID { get; set; }

        public BaseEnemyController(EnemyData enemyData, IEnemyFactory factory)
        {
            var enemy = factory.Create(enemyData);
            _instance = enemy.instance;

            var collision = enemy.collision;
            collision.OnEnemyHit += EnemyHit;

            _rigidbody2D = enemy.rigidbody2D;

            _yOffset = enemyData.YSpawnOffset;
            _minX = enemyData.MinXPosition;
            _maxX = enemyData.MaxXPosition;

            _health = new Health(enemyData.Health, enemyData.Health);

            _flyVelocity = enemyData.InitialVelocity;
        }

        public void InjectPlayerTransform(Transform player)
        {
            _player = player;
        }

        public void Activate()
        {
            IsActive = true;
            _instance.transform.position = new Vector3(Random.Range(_minX, _maxX),
                _player.position.y + _yOffset);
            _rigidbody2D.velocity = _flyVelocity;
        }

        public void ReturnToPool(Transform poolRoot)
        {
            _instance.transform.SetParent(poolRoot);
            _instance.transform.localPosition = Vector3.zero;
            _instance.transform.localRotation = Quaternion.identity;
            _health.ResetCurrent();
            _rigidbody2D.velocity = Vector2.zero;
            IsActive = false;
        }

        private void EnemyHit(float damage)
        {
            if (_health.TryKill(damage))
            {
                OnEnemyKilled.Invoke(ID);
            }
        }
    }
}