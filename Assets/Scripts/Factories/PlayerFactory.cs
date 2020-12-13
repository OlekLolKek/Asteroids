using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly PlayerData _playerData;
        private Transform _transform;
        private Transform _barrelTransform;
        private Camera _camera;
        private GameObject _player;


        public PlayerFactory(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        public void CreatePlayer()
        {
            _player = new GameObject(_playerData.PlayerName);
            _transform = _player.transform;
            var spriteRenderer = _player.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _playerData.PlayerSprite;
            _player.AddComponent<PolygonCollider2D>();
            
            var barrel = new GameObject(_playerData.BarrelName);
            _barrelTransform = barrel.transform;
            _barrelTransform.SetParent(_player.transform);
            _barrelTransform.localPosition = _playerData.BarrelPosition;

            var camera = new GameObject(_playerData.CameraName);
            camera.transform.SetParent(_player.transform);
            _camera = camera.AddComponent<Camera>();
            _camera.transform.localPosition = _playerData.CameraPosition;
            _camera.gameObject.AddComponent<AudioListener>();
            _camera.orthographic = true;
            _camera.clearFlags = CameraClearFlags.Color;
            _camera.backgroundColor = Color.black;
            _camera.orthographicSize = _playerData.CameraSize;

            Object.Instantiate(_playerData.ParticlesPrefab, _player.transform);
        }

        public Transform GetTransform()
        {
            return _transform;
        }

        public Transform GetBarrelTransform()
        {
            return _barrelTransform;
        }

        public Camera GetCamera()
        {
            return _camera;
        }
    }
}