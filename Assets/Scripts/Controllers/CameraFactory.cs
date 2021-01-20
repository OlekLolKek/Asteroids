using UnityEngine;

namespace DefaultNamespace
{
    public class CameraFactory : IFactory
    {
        private CameraData _cameraData;
        private Camera _camera;

        public CameraFactory(CameraData cameraData)
        {
            _cameraData = cameraData;
        }

        public GameObject Create()
        {
            var camera = new GameObject(_cameraData.CameraName);
            _camera = camera.AddComponent<Camera>();
            _camera.transform.localPosition = _cameraData.CameraPosition;
            _camera.gameObject.AddComponent<AudioListener>();
            _camera.orthographic = true;
            _camera.clearFlags = CameraClearFlags.Color;
            _camera.backgroundColor = Color.black;
            _camera.orthographicSize = _cameraData.CameraSize;

            return _camera.gameObject;
        }

        public Transform GetTransform()
        {
            return _camera.transform;
        }
    }
}