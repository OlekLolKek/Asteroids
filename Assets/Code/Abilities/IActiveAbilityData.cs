using UnityEngine;


namespace Abilities
{
    public interface IActiveAbilityData
    {
        GameObject Prefab { get; }
        int Damage { get; }
        float Cooldown { get; }
    }
}