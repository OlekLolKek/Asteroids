using UnityEngine;


namespace DefaultNamespace
{
    public sealed class PlayerModel
    {
        private readonly Transform _transform;
        private readonly Transform _barrelTransform;
        
        public Transform Transform => _transform;
        public Transform BarrelTransform => _barrelTransform;


        public PlayerModel(PlayerFactory factory)
        {
            factory.Create();
            _transform = factory.GetTransform();
            _barrelTransform = factory.GetBarrelTransform();
        }
    }
}