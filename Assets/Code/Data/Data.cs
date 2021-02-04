using System.IO;
using Abilities;
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
        [SerializeField] private string _bulletDataPath;
        [SerializeField] private string _explosionDataPath;

        private PlayerData _playerData;
        private EnemyData _enemyData;
        private CameraData _cameraData;
        private BulletData _bulletData;
        private ExplosionData _explosionData;


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

        public BulletData BulletData
        {
            get
            {
                if (_bulletData == null)
                {
                    _bulletData = Load<BulletData>(_dataRootPath + _bulletDataPath);
                }

                return _bulletData;
            }
        }

        public ExplosionData ExplosionData
        {
            get
            {
                if (_explosionData == null)
                {
                    _explosionData = Load<ExplosionData>(_dataRootPath + _explosionDataPath);
                }

                return _explosionData;
            }
        }

        private T Load<T>(string resourcesPath) where T : Object
        {
            return Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
        }
    }
}