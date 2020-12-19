using System;
using UnityEngine;


namespace DefaultNamespace
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnEnemyHit = delegate {  };
        public static IEnemyFactory Factory;
        private Transform _poolRoot;
        private Health _health;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(TagManager.BULLET_TAG))
            {
                OnEnemyHit.Invoke(this);
            }
        }
    }
}