using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Controllers
{
    public class BaseEnemyController
    {
        public event Action<BaseEnemyController> OnEnemyKilled = delegate {  };
        
        private Transform _poolRoot;
        private Transform _player;
        
        private readonly GameObject _instance;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Vector2 _flyVelocity;
        private readonly Health _health;

        private readonly float _yOffset;
        private readonly float _minX;
        private readonly float _maxX;


        public string Name => _instance.name;

        public bool IsActive
        {
            get => _instance.activeSelf;
            private set => _instance.SetActive(value);
        }
        public int ID { get; set; }

        public BaseEnemyController(EnemyData enemyData, IEnemyFactory factory)
        {
            var (instance, collision, rigidbody2D) = factory.Create(enemyData);
            _instance = instance;

            collision.OnEnemyHit += EnemyHit;

            _rigidbody2D = rigidbody2D;

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
                OnEnemyKilled.Invoke(this);
            }
        }
    }
}