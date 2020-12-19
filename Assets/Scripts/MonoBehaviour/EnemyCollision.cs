using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyCollision : MonoBehaviour
    {
        public event Action OnEnemyHit = delegate {  };
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(TagManager.BULLET_TAG))
            {
                OnEnemyHit.Invoke();
            }
        }
    }
}