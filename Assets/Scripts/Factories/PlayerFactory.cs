using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFactory : IFactory
    {
        private readonly PlayerData _playerData;
        private Transform _transform;
        private Transform _barrelTransform;
        private GameObject _player;
        private AudioSource _audioSource;


        public PlayerFactory(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        public GameObject Create()
        {
            _player = new GameObject(_playerData.PlayerName);
            _transform = _player.transform;
            var scale = _playerData.SpriteScale;
            _transform.localScale = new Vector3(scale, scale);
            
            var spriteRenderer = _player.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _playerData.PlayerSprite;
            
            _audioSource = _player.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
            _audioSource.clip = _playerData.ShootSfx;
            _audioSource.volume = _playerData.ShootVolume;
                
            _player.AddComponent<PolygonCollider2D>();
            _player.layer = _playerData.PlayerLayerID;
            
            var barrel = new GameObject(_playerData.BarrelName);
            _barrelTransform = barrel.transform;
            _barrelTransform.SetParent(_player.transform);
            _barrelTransform.localPosition = _playerData.BarrelPosition;

            Object.Instantiate(_playerData.ParticlesPrefab, _player.transform);

            return _player;
        }

        public Transform GetTransform()
        {
            return _transform;
        }

        public Transform GetBarrelTransform()
        {
            return _barrelTransform;
        }

        public AudioSource GetAudioSource()
        {
            return _audioSource;
        }
    }
}