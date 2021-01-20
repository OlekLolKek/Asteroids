using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;


namespace DefaultNamespace
{
    public sealed class CameraFactory : IFactory
    {
        private readonly CameraData _cameraData;
        private Camera _camera;

        public CameraFactory(CameraData cameraData)
        {
            _cameraData = cameraData;
        }

        public GameObject Create()
        {
            var camera = new GameObject(_cameraData.CameraName);
            camera.transform.localPosition = _cameraData.CameraPosition;

            camera.AddComponent<AudioListener>();
            
            _camera = camera.AddComponent<Camera>();
            _camera.orthographic = true;
            _camera.orthographicSize = _cameraData.CameraSize;
            _camera.clearFlags = CameraClearFlags.Color;
            _camera.backgroundColor = _cameraData.BackgroundColor;
            
            var postProcessing = camera.AddComponent<PostProcessLayer>();
            postProcessing.Init(_cameraData.PostProcessResources);
            postProcessing.volumeTrigger = camera.transform;
            postProcessing.volumeLayer = _cameraData.PostProcessingLayer;
            postProcessing.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;

            var pixelPerfect = camera.AddComponent<PixelPerfectCamera>();
            pixelPerfect.assetsPPU = _cameraData.AssetsPPU;
            pixelPerfect.upscaleRT = true;

            var particles = Object.Instantiate(_cameraData.ParticlesPrefab, camera.transform);
            var position = camera.transform.position;
            particles.transform.localPosition = _cameraData.ParticlesPosition;
            
            return camera;
        }

        public Transform GetTransform()
        {
            return _camera.transform;
        }
    }
}