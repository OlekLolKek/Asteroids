using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Data/AbilityData")]
    public class ExplosionData : ScriptableObject, IActiveAbilityData
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _damage;
        [SerializeField] private float _cooldown;
        
        [SerializeField] private Vector3 _maxScale;
        [SerializeField] private float _tweenTime;
        
        public GameObject Prefab => _prefab;
        public int Damage => _damage;
        public float Cooldown => _cooldown;
        
        public Vector3 MaxScale => _maxScale;
        public float TweenTime => _tweenTime;
    }
}