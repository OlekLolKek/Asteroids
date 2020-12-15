using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "Data/Camera")]
    public class CameraData : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private Vector3 _cameraPosition;
        [SerializeField, Range(0, 10)] private float _cameraSize;
        [SerializeField] private string _cameraName;


        #endregion


        #region Properties


        public Vector3 CameraPosition => _cameraPosition;
        public float CameraSize => _cameraSize;
        public string CameraName => _cameraName;

        #endregion
    }
}