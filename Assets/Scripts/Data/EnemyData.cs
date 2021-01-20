﻿using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private Sprite _asteroidSprite;
        [SerializeField] private Health _health;
        [SerializeField] private float _spriteScale;
        [SerializeField] private float _enemyTimer;
        [SerializeField] private float _minXPosition;
        [SerializeField] private float _maxXPosition;
        [SerializeField] private float _ySpawnOffset;
        [SerializeField] private float _colliderRadius;
        [SerializeField] private int _asteroidPoolSize;

        #endregion


        #region Properties
        
        public Sprite AsteroidSprite => _asteroidSprite;
        public Health Health => _health;
        public float SpriteScale => _spriteScale;
        public float EnemyTimer => _enemyTimer;
        public float MINXPosition => _minXPosition;
        public float MAXXPosition => _maxXPosition;
        public float YSpawnOffset => _ySpawnOffset;
        public float ColliderRadius => _colliderRadius;
        public int AsteroidPoolSize => _asteroidPoolSize;

        #endregion
    }
}