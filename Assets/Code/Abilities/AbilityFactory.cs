﻿using UnityEngine;

namespace Abilities
{
    public sealed class AbilityFactory
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

        public SpriteRenderer SpriteRenderer
        {
            get
            {
                if (_spriteRenderer == null)
                {
                    Create();
                }

                return _spriteRenderer;
            }
            private set => _spriteRenderer = value;
        }

        public AudioSource AudioSource
        {
            get
            {
                if (_audioSource == null)
                {
                    Create();
                }

                return _audioSource;
            }
        }

        public GameObject Instance { get; private set; }

        private readonly ExplosionData _data;

        private IActiveAbilityView _view;
        private SpriteRenderer _spriteRenderer;
        private AudioSource _audioSource;

        public AbilityFactory(ExplosionData data)
        {
            _data = data;
        }

        public GameObject Create()
        {
            Instance = Object.Instantiate(_data.Prefab.gameObject);

            _view = Instance.GetComponent<IActiveAbilityView>();
            _spriteRenderer = Instance.GetComponent<SpriteRenderer>();
            _audioSource = Instance.GetComponent<AudioSource>();

            return Instance;
        }
    }
}