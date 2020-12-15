using System.IO;
using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public sealed class Data : ScriptableObject
    {
        [SerializeField] private string _dataRootPath;
        [SerializeField] private string _playerDataPath;
        [SerializeField] private string _enemyDataPath;
        [SerializeField] private string _cameraDataPath;

        private PlayerData _playerData;
        private EnemyData _enemyData;
        private CameraData _cameraData;
        
        
        public PlayerData PlayerData
        {
            get
            {
                if (_playerData == null)
                {
                    _playerData = Load<PlayerData>(_dataRootPath + _playerDataPath);
                }

                return _playerData;
            }
        }

        public EnemyData EnemyData
        {
            get
            {
                if (_enemyData == null)
                {
                    _enemyData = Load<EnemyData>(_dataRootPath + _enemyDataPath);
                }

                return _enemyData;
            }
        }

        public CameraData CameraData
        {
            get
            {
                if (_cameraData == null)
                {
                    _cameraData = Load<CameraData>(_dataRootPath + _cameraDataPath);
                }

                return _cameraData;
            }
        }

        private T Load<T>(string resourcesPath) where T : Object
        {
            return Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
        }
    }
}