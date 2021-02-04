using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet")]
    public class BulletData : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private Sprite _bulletSprite;
        [SerializeField] private Vector2 _bulletColliderSize;
        [SerializeField] private float _shootForce;
        [SerializeField] private float _bulletLifespan;
        [SerializeField] private float _spriteScale;
        [SerializeField] private float _shootCooldown;
        [SerializeField] private float _damage;
        [SerializeField] private int _layer;

        #endregion
        
        public Sprite BulletSprite => _bulletSprite;
        public Vector2 BulletColliderSize => _bulletColliderSize;
        public float ShootForce => _shootForce;
        public float BulletLifespan => _bulletLifespan;
        public float SpriteScale => _spriteScale;
        public float ShootCooldown => _shootCooldown;
        public float Damage => _damage;
        public int Layer => _layer;
    }
}