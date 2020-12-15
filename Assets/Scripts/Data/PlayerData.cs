using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerData : ScriptableObject, IUnit
    {
        #region Fields

        [SerializeField] private GameObject _particlesPrefab;
        [SerializeField] private Rigidbody2D _bulletPrefab;
        [SerializeField] private Sprite _playerSprite;
        [SerializeField] private Vector3 _barrelPosition;
        [SerializeField] private float _spriteScale;
        [SerializeField, Range(0, 10)] private float _speed;
        [SerializeField, Range(0, 10)] private float _acceleration;
        [SerializeField, Range(0, 10000)] private float _shootForce;
        [SerializeField, Range(0, 100)] private float _bulletLifespan;
        [SerializeField] private float _maxLeft;
        [SerializeField] private float _maxRight;
        [SerializeField] private string _playerName;
        [SerializeField] private string _barrelName;
        [SerializeField] private int _playerLayerID;

        #endregion


        #region Properties

        public GameObject ParticlesPrefab => _particlesPrefab;
        public Rigidbody2D BulletPrefab => _bulletPrefab;
        public Sprite PlayerSprite => _playerSprite;
        public Vector3 BarrelPosition => _barrelPosition;
        public float SpriteScale => _spriteScale;
        public float Speed => _speed;
        public float Acceleration => _acceleration;
        public float ShootForce => _shootForce;
        public float BulletLifespan => _bulletLifespan;
        public string PlayerName => _playerName;
        public string BarrelName => _barrelName;
        public float MAXLeft => _maxLeft;
        public float MAXRight => _maxRight;
        public int PlayerLayerID => _playerLayerID;

        #endregion
    }
}