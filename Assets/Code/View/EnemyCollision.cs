using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyCollision : MonoBehaviour
    {
        public event Action<float> OnEnemyHit = delegate(float damage) {  };
        
        public void Hit(float damage)
        {
            OnEnemyHit.Invoke(damage);
        }
    }
}