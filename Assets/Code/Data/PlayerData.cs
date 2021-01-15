using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerData : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private Sprite _playerSprite;
        [SerializeField] private Vector3 _barrelPosition;
        [SerializeField] private float _spriteScale;
        [SerializeField, Range(0, 10)] private float _speed;
        [SerializeField, Range(0, 10)] private float _acceleration;
        [SerializeField] private float _maxLeft;
        [SerializeField] private float _maxRight;
        [SerializeField] private string _playerName;
        [SerializeField] private string _barrelName;
        [SerializeField] private int _playerLayerID;
        [SerializeField] private AudioClip _shootSFX;
        [SerializeField, Range(0.0f, 1.0f)] private float _shootVolume;

        #endregion


        #region Properties
        
        public Sprite PlayerSprite => _playerSprite;
        public Vector3 BarrelPosition => _barrelPosition;
        public float SpriteScale => _spriteScale;
        public float Speed => _speed;
        public float Acceleration => _acceleration;
        public string PlayerName => _playerName;
        public string BarrelName => _barrelName;
        public float MAXLeft => _maxLeft;
        public float MAXRight => _maxRight;
        public int PlayerLayerID => _playerLayerID;
        public AudioClip ShootSfx => _shootSFX;
        public float ShootVolume => _shootVolume;

        #endregion
    }
}