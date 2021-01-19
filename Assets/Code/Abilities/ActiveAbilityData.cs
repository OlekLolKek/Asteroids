using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Data/AbilityData")]
    public class ActiveAbilityData : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _damage;
        [SerializeField] private float _coolDown;
        
        public GameObject Prefab => _prefab;
        public int Damage => _damage;
        public float CoolDown => _coolDown;
    }
}