using UnityEngine;
using View;


namespace DefaultNamespace
{
    public sealed class PlayerFactory : IFactory
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
            _player = Object.Instantiate(_playerData.Prefab.gameObject);
            _transform = _player.transform;

            _audioSource = _player.GetComponent<AudioSource>();
            _audioSource.clip = _playerData.ShootSfx;
            _audioSource.volume = _playerData.ShootVolume;

            var playerView = _player.GetComponent<PlayerView>();
            _barrelTransform = playerView.Barrel.transform;

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