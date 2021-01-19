using System;
using DefaultNamespace;
using UnityEngine;


namespace Abilities
{
    [Serializable]
    public sealed class Explosion : IActiveAbility
    {
        public GameObject Instance { get; }
        public int Damage { get; }

        private IActiveAbilityView _view;
        private float _coolDown;
        private float _timer;
        private bool _isReady;


        public Explosion(ActiveAbilityData data, PlayerModel playerModel)
        {
            var factory = new AbilityFactory(data);
            Instance = factory.Create();
            Instance.SetActive(false);
            _view = factory.View;
            
            Damage = data.Damage;
            _coolDown = data.CoolDown;
        }

        public void Execute(float deltaTime)
        {
            
        }

        public void Cast()
        {
            Instance.SetActive(true);
            Instance.transform.
        }
    }
}