using System;
using UnityEngine;

namespace Abilities
{
    public interface IActiveAbilityView
    {
        event Action<Collision2D> OnCollisionEnter2D; 
    }
}