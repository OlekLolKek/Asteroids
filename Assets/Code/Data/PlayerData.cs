using UnityEngine;
using View;


namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerData : ScriptableObject
    {
        #region Fields

        [SerializeField] private PlayerView _prefab;
        [SerializeField, Range(0, 10)] private float _speed;
        [SerializeField] private float _maxLeft;
        [SerializeField] private float _maxRight;
        [SerializeField] private AudioClip _shootSfx;
        [SerializeField, Range(0.0f, 1.0f)] private float _shootVolume;

        #endregion


        #region Properties

        public PlayerView Prefab => _prefab;
        public float Speed => _speed;
        public float MAXLeft => _maxLeft;
        public float MAXRight => _maxRight;
        public AudioClip ShootSfx => _shootSfx;
        public float ShootVolume => _shootVolume;

        #endregion
    }
}