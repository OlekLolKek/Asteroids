using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private Sprite _asteroidSprite;
        [SerializeField] private Health _health;
        [SerializeField] private float _enemyTimer;

        #endregion


        #region Properties
        
        public Sprite AsteroidSprite => _asteroidSprite;
        public Health Health => _health;
        public float EnemyTimer => _enemyTimer;

        #endregion
    }
}