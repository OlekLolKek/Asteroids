using UnityEngine;

namespace DefaultNamespace
{
    public class LookController : IExecutable
    {
        private readonly Transform _playerTransform;
        private readonly Camera _camera;


        public LookController(Transform playerTransform, Camera camera)
        {

            _playerTransform = playerTransform;
            _camera = camera;
        }
        
        public void Execute(float deltaTime)
        {
            Look();
        }

        private void Look()
        {
            var direction = Input.mousePosition - _camera.WorldToScreenPoint(_playerTransform.position);;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _playerTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}