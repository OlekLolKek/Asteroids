using DefaultNamespace;
using UnityEngine;

namespace Abilities
{
    public interface IActiveAbility : IAbility, IExecutable
    {
        GameObject Instance { get; }
        void Cast();
    }
}