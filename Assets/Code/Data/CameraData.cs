using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "Data/Camera")]
    public sealed class CameraData : ScriptableObject
    {
        #region Fields

        [SerializeField] private GameObject _particlesPrefab;
        [SerializeField] private PostProcessResources _postProcessResources;
        [SerializeField] private LayerMask _postProcessingLayer;
        [SerializeField] private Vector3 _cameraPosition;
        [SerializeField] private Vector3 _particlesPosition;
        [SerializeField] private Color _backgroundColor;
        [SerializeField, Range(0, 10)] private float _cameraSize;
        [SerializeField] private float _playPosition;
        [SerializeField] private float _pausePosition;
        [SerializeField] private float _tweenTime;
        [SerializeField] private string _cameraName;
        [SerializeField] private int _assetsPpu;


        #endregion


        #region Properties

        public GameObject ParticlesPrefab => _particlesPrefab;
        public PostProcessResources PostProcessResources => _postProcessResources;
        public LayerMask PostProcessingLayer => _postProcessingLayer;
        public Vector3 CameraPosition => _cameraPosition;
        public Vector3 ParticlesPosition => _particlesPosition;
        public Color BackgroundColor => _backgroundColor;
        public float CameraSize => _cameraSize;
        public float PlayPosition => _playPosition;
        public float PausePosition => _pausePosition;
        public float TweenTime => _tweenTime;
        public string CameraName => _cameraName;
        public int AssetsPpu => _assetsPpu;

        #endregion
    }
}