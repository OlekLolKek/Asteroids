using UnityEngine;


namespace DefaultNamespace
{
    public sealed class PlayerModel
    {
        private readonly Transform _transform;
        private readonly Transform _barrelTransform;
        private readonly Camera _camera;
        
        
        public Transform Transform => _transform;

        public Transform BarrelTransform => _barrelTransform;

        public Camera Camera => _camera;

        
        public PlayerModel(IPlayerFactory playerFactory)
        {
            playerFactory.CreatePlayer();
            _transform = playerFactory.GetTransform();
            _barrelTransform = playerFactory.GetTransform();
            _camera = playerFactory.GetCamera();
        }
    }
}