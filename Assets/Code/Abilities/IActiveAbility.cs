using DefaultNamespace;
using UnityEngine;

namespace Abilities
{
    public interface IActiveAbility : IAbility, IExecutable, ICleanable
    {
        GameObject Instance { get; }
        void Cast();
    }
}