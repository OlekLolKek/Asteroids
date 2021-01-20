using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UniRx;
using UnityEngine;


namespace Abilities
{
    [Serializable]
    public sealed class Explosion : IActiveAbility
    {
        public GameObject Instance { get; }
        public int Damage { get; }

        private IActiveAbilityView _view;
        private SpriteRenderer _spriteRenderer;
        private Vector3 _maxScale;
        private float _tweenTime;
        private float _coolDown;
        private float _timer;

        private readonly Transform _playerTransform;
        private readonly Vector3 _rotation = new Vector3(0, 0, 180);
        private readonly float _endTweenTime = 0.25f;
        private readonly float _delay = 0.1f;


        public Explosion(ExplosionData data, PlayerModel playerModel)
        {
            var factory = new AbilityFactory(data);
            Instance = factory.Create();
            Instance.SetActive(false);
            _spriteRenderer = factory.SpriteRenderer;
            _view = factory.View;
            _view.OnCollision += OnCollisionEnter;
            
            Damage = data.Damage;
            _coolDown = data.Cooldown;
            _tweenTime = data.TweenTime;
            _maxScale = data.MaxScale;

            _playerTransform = playerModel.Transform;
        }

        public void Execute(float deltaTime)
        {
            if (_timer > 0)
            {
                _timer -= deltaTime;
            }

            var position = Instance.transform.position;
            position.y = _playerTransform.position.y;
            Instance.transform.position = position;
        }

        public void Cast()
        {
            if (_timer > 0) return;
            Expand().ToObservable().Subscribe();
        }

        private void OnCollisionEnter(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyCollision collision))
            {
                Debug.Log(collision);
                collision.Hit(Damage);
            }
        }

        private IEnumerator Expand()
        {
            _timer = _coolDown;
            Instance.SetActive(true);
            Instance.transform.position = _playerTransform.position;
            Instance.transform.DOScale(_maxScale, _tweenTime);
            
            yield return new WaitForSeconds(_tweenTime + _delay);
            
            Instance.transform.DORotate(_rotation, _endTweenTime);
            Instance.transform.DOScale(Vector3.zero, _endTweenTime);
            
            yield return new WaitForSeconds(_endTweenTime + _delay);
            
            Instance.transform.localScale = Vector3.zero;
            Instance.transform.rotation = Quaternion.identity;
            Instance.SetActive(false);
        }

        public void Cleanup()
        {
            _view.OnCollision -= OnCollisionEnter;
        }
    }
}