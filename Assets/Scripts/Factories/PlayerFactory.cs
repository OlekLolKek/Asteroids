using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFactory : IFactory
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
        
        public GameObject Create()
        {
            _player = new GameObject(_playerData.PlayerName);
            _transform = _player.transform;
            var scale = _playerData.SpriteScale;
            _transform.localScale = new Vector3(scale, scale);
            var spriteRenderer = _player.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _playerData.PlayerSprite;
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

        public Camera GetCamera()
        {
            return _camera;
        }
    }
}