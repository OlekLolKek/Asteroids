using UnityEngine;

namespace DefaultNamespace
{
    public class Service : IService
    {
        public void Test()
        {
            Debug.Log(nameof(Service));
        }
    }
}