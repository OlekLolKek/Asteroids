using System;
using UnityEngine;


namespace DefaultNamespace
{
    public sealed class PlayerModel
    {
        public event Action<float> OnShootCooldownChanged = delegate(float f) {  }; 
        
        public Transform Transform { get; }
        public Transform BarrelTransform { get; }
        public AudioSource AudioSource { get; }

        public PlayerModel(PlayerFactory factory)
        {
            factory.Create();
            Transform = factory.GetTransform();
            BarrelTransform = factory.GetBarrelTransform();
            AudioSource = factory.GetAudioSource();
        }

        public void SetShootCooldown(float newCooldown)
        {
            OnShootCooldownChanged.Invoke(newCooldown);
        }
    }
}