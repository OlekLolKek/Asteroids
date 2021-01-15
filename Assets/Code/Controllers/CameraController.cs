using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : IExecutable
    {
        private readonly Transform _playerTransform;
        private readonly Transform _cameraTransform;
        private readonly Vector3 _cameraPosition;


        public CameraController(CameraModel cameraModel, PlayerModel playerFactory, CameraData cameraData)
        {
            _cameraTransform = cameraModel.CameraTransform;
            _playerTransform = playerFactory.Transform;
            _cameraPosition = cameraData.CameraPosition;
        }
        
        public void Execute(float deltaTime)
        {
            Move();
        }

        private void Move()
        {
            var newPosition = _cameraTransform.position;
            newPosition.y = _playerTransform.position.y + _cameraPosition.y;
            _cameraTransform.position = newPosition;
        }
    }
}