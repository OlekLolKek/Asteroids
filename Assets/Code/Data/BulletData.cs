using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet")]
    public class BulletData : ScriptableObject
    {
        #region Fields

        [SerializeField] private Rigidbody2D _laserPrefab;
        [SerializeField] private Sprite _bulletSprite;
        [SerializeField] private Vector2 _bulletColliderSize;
        [SerializeField] private float _shootForce;
        [SerializeField] private float _bulletLifespan;
        [SerializeField] private float _spriteScale;
        [SerializeField] private float _shootCooldown;

        #endregion

        public Rigidbody2D LaserPrefab => _laserPrefab;
        public Sprite BulletSprite => _bulletSprite;
        public Vector2 BulletColliderSize => _bulletColliderSize;
        public float ShootForce => _shootForce;
        public float BulletLifespan => _bulletLifespan;
        public float SpriteScale => _spriteScale;
        public float ShootCooldown => _shootCooldown;
    }
}