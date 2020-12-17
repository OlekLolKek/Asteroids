using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public class ShootController : IExecutable
    {
        //TODO: Сделать из ужасного кода ниже что-то нормальное
        
        #region Fields

        private readonly Dictionary<int, IDisposable> _coroutines = new Dictionary<int, IDisposable>();
        private readonly List<Bullet> _bullets = new List<Bullet>();
        private readonly Transform _barrelTransform;
        private readonly BulletPool _bulletPool;
        private readonly AudioSource _audioSource;
        private readonly float _shootForce;
        private readonly float _bulletLifespan;
        private readonly float _scale;
        private readonly float _shootCooldown;
        private float _timer;
        
        private IDisposable _coroutine;

        #endregion

        
        public ShootController(BulletData bulletData, Transform barrelTransform, 
            LaserFactory laserFactory, AudioSource audioSource)
        {
            _bulletPool = new BulletPool(12, bulletData, laserFactory);
            _barrelTransform = barrelTransform;
            _shootForce = bulletData.ShootForce;
            _bulletLifespan = bulletData.BulletLifespan;
            _scale = bulletData.SpriteScale;
            _shootCooldown = bulletData.ShootCooldown;
            _audioSource = audioSource;
        }

        public void Execute(float deltaTime)
        {
            Shoot(deltaTime);
        }

        private void Shoot(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer >= _shootCooldown)
            {
                _timer = 0.0f;
                var bullet = _bulletPool.GetBullet(BulletTypes.Laser);
                
                if (!_coroutines.ContainsKey(bullet.ID))
                {
                    var coroutine = _coroutine = ReturnToPool(bullet.ID, _bulletLifespan).ToObservable().Subscribe();
                    _coroutines.Add(bullet.ID, coroutine);
                    _bullets.Add(bullet);
                }
                else
                {
                    _coroutines[bullet.ID] = ReturnToPool(bullet.ID, _bulletLifespan).ToObservable().Subscribe();
                }
                
                var transform = bullet.transform;
                var rigidbody = bullet.GetComponent<Rigidbody2D>();
                bullet.gameObject.SetActive(true);
                transform.localScale = new Vector3(_scale, _scale);
                transform.position = _barrelTransform.position;
                rigidbody.AddForce(transform.up * _shootForce);
                bullet.OnBulletHit += OnBulletHit;
                _audioSource.Play();
            }
        }

        private void OnBulletHit(Bullet bullet)
        {
            bullet.OnBulletHit -= OnBulletHit;
            if (bullet.gameObject.activeSelf)
            {
                _bulletPool.ReturnToPool(bullet.transform);
            }
                
            _coroutine.Dispose();
        }

        private IEnumerator ReturnToPool(int id, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            _bullets[id].OnBulletHit -= OnBulletHit;
            if (_bullets[id].gameObject.activeSelf)
                _bulletPool.ReturnToPool(_bullets[id].transform);
        }
    }
}