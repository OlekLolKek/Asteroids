using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletCollision : MonoBehaviour
    {
        public event Action<Collision2D> OnBulletHit = delegate(Collision2D collision) {  };

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnBulletHit.Invoke(other);
        }
    }
}