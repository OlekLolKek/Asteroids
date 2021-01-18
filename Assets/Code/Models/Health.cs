using System;
using UnityEngine;


namespace DefaultNamespace
{
    [Serializable]
    public sealed class Health
    {
        [SerializeField] private float _max;
        [SerializeField] private float _current;
        
        public float Max 
        { 
            get => _max;
            private set => _max = value;
        }

        public float Current
        {
            get => _current; 
            private set => _current = value;
        }

        #region Methods

        public Health(float max, float current)
        {
            Max = max;
            Current = current;
        }

        public void ChangeCurrentHealth(float hp)
        {
            Current = hp;
        }

        public bool TryKill(float damage)
        {
            Current -= damage;
            return Current <= Max;
        }

        public void ResetCurrent()
        {
            Current = Max;
        }

        #endregion
    }
}