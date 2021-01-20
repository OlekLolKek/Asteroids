using System;
using UnityEngine;

namespace Abilities
{
    public class ActiveAbilityView : MonoBehaviour, IActiveAbilityView
    {
        public event Action<Collision2D> OnCollision = delegate(Collision2D collision2D) {  };

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollision.Invoke(other);
        }
    }
}