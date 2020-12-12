using UnityEngine;

namespace DefaultNamespace
{
    public interface IPlayerFactory
    {
        void CreatePlayer();
        Transform GetTransform();
        Transform GetBarrelTransform();
        Camera GetCamera();
    }
}