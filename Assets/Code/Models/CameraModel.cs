using UnityEngine;


namespace DefaultNamespace
{
    public class CameraModel
    {
        public Transform CameraTransform { get; }


        public CameraModel(IFactory factory)
        {
            CameraTransform = factory.Create().transform;
        }
    }
}