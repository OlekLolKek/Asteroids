using UnityEngine;


namespace DefaultNamespace
{
    public class CameraModel
    {
        private readonly Transform _cameraTransform;

        public Transform CameraTransform => _cameraTransform;


        public CameraModel(IFactory factory)
        {
            factory.Create();
            _cameraTransform = factory.GetTransform();
        }
    }
}