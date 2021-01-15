using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "Data/Camera")]
    public class CameraData : ScriptableObject
    {
        #region Fields

        [SerializeField] private GameObject _particlesPrefab;
        [SerializeField] private PostProcessResources _postProcessResources;
        [SerializeField] private LayerMask _postProcessingLayer;
        [SerializeField] private Vector3 _cameraPosition;
        [SerializeField] private Vector3 _particlesPosition;
        [SerializeField, Range(0, 10)] private float _cameraSize;
        [SerializeField] private string _cameraName;
        [SerializeField] private int _assetsPPU;


        #endregion


        #region Properties

        public GameObject ParticlesPrefab => _particlesPrefab;
        public PostProcessResources PostProcessResources => _postProcessResources;
        public LayerMask PostProcessingLayer => _postProcessingLayer;
        public Vector3 CameraPosition => _cameraPosition;
        public Vector3 ParticlesPosition => _particlesPosition;
        public float CameraSize => _cameraSize;
        public string CameraName => _cameraName;
        public int AssetsPPU => _assetsPPU;

        #endregion
    }
}