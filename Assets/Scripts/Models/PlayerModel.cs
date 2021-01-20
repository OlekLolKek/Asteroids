using UnityEngine;


namespace DefaultNamespace
{
    public sealed class PlayerModel
    {
        public Transform Transform { get; }
        public Transform BarrelTransform { get; }


        public PlayerModel(PlayerFactory factory)
        {
            factory.Create();
            Transform = factory.GetTransform();
            BarrelTransform = factory.GetBarrelTransform();
        }
    }
}