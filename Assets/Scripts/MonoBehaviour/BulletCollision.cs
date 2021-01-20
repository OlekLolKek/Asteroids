using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletCollision : MonoBehaviour
    {
        public event Action OnBulletHit = delegate {  };
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(TagManager.ENEMY_TAG))
            {
                OnBulletHit.Invoke();
            }
        }
    }
}