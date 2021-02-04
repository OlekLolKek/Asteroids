using UnityEngine;


namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private GameObject _barrel;

        public GameObject Barrel => _barrel;
    }
}