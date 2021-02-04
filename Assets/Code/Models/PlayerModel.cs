using System;
using UnityEngine;


namespace DefaultNamespace
{
    public sealed class PlayerModel
    {
        public Transform Transform { get; }
        public Transform BarrelTransform { get; }
        public AudioSource AudioSource { get; }

        public PlayerModel(PlayerFactory factory)
        {
            Transform = factory.Create().transform;
            BarrelTransform = factory.GetBarrelTransform();
            AudioSource = factory.GetAudioSource();
        }
    }
}