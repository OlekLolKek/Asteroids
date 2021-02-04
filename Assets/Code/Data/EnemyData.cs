using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy")]
    public sealed class EnemyData : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private Sprite _asteroidSprite;
        [SerializeField] private Vector2 _initialVelocity;
        [SerializeField] private float _health;
        [SerializeField] private float _spriteScale;
        [SerializeField] private float _enemyTimer;
        [SerializeField] private float _minXPosition;
        [SerializeField] private float _maxXPosition;
        [SerializeField] private float _ySpawnOffset;
        [SerializeField] private float _colliderRadius;
        [SerializeField] private int _asteroidPoolSize;
        [SerializeField] private int _pointsPerEnemy;
        [SerializeField] private int _mass;

        #endregion


        #region Properties
        
        public Sprite AsteroidSprite => _asteroidSprite;
        public Vector2 InitialVelocity => _initialVelocity;
        public float Health => _health;
        public float SpriteScale => _spriteScale;
        public float EnemyTimer => _enemyTimer;
        public float MinXPosition => _minXPosition;
        public float MaxXPosition => _maxXPosition;
        public float YSpawnOffset => _ySpawnOffset;
        public float ColliderRadius => _colliderRadius;
        public int AsteroidPoolSize => _asteroidPoolSize;
        public int PointsPerEnemy => _pointsPerEnemy;
        public int Mass => _mass;

        #endregion
    }
}