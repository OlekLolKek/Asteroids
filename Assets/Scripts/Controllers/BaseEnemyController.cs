using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace
{
    public class BaseEnemyController
    {
        public event Action<int> OnEnemyHit = delegate {  };
        
        private GameObject _instance;
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
            _instance = factory.Create(enemyData);

            var collision = _instance.GetComponent<EnemyCollision>();
            collision.OnEnemyHit += EnemyHit;

            _yOffset = enemyData.YSpawnOffset;
            _minX = enemyData.MINXPosition;
            _maxX = enemyData.MAXXPosition;
        }

        public BaseEnemyController InjectPlayerTransform(Transform player)
        {
            _player = player;
            return this;
        }

        public void Activate()
        {
            IsActive = true;
            _instance.transform.position = new Vector3(Random.Range(_minX, _maxX),
                _player.position.y + _yOffset);
        }

        public void ReturnToPool(Transform poolRoot)
        {
            _instance.transform.SetParent(poolRoot);
            _instance.transform.localPosition = Vector3.zero;
            _instance.transform.localRotation = Quaternion.identity;
            IsActive = false;
        }

        private void EnemyHit()
        {
            OnEnemyHit.Invoke(ID);
        }
    }
}