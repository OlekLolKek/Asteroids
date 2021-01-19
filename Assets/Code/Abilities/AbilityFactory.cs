using UnityEngine;

namespace Abilities
{
    public class AbilityFactory
    {
        public IActiveAbilityView View 
        {
            get
            {
                if (_view == null)
                {
                    Create();
                }

                return _view;
            } 
            private set => _view = value; 
        }
        
        public GameObject Instance { get; private set; }

        private readonly ActiveAbilityData _data;

        private IActiveAbilityView _view;

        public AbilityFactory(ActiveAbilityData data)
        {
            _data = data;
        }

        public GameObject Create()
        {
            Instance = Object.Instantiate(_data.Prefab.gameObject);

            _view = Instance.GetComponent<IActiveAbilityView>();

            return Instance;
        }
    }
}