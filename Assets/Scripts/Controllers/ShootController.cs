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

        private readonly List<IDisposable> _coroutines = new List<IDisposable>();
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
            var ratio = bulletData.BulletLifespan / bulletData.ShootCooldown;
            var poolSize = (int) Math.Ceiling(ratio);
            _bulletPool = new BulletPool(poolSize, bulletData, laserFactory);
            
            _barrelTransform = barrelTransform;
            _audioSource = audioSource;
            
            _bulletLifespan = bulletData.BulletLifespan;
            _shootCooldown = bulletData.ShootCooldown;
            _shootForce = bulletData.ShootForce;
            _scale = bulletData.SpriteScale;
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
                
                ManagePool(bullet);
                
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

        private void ManagePool(Bullet bullet)
        {
            if (!_bullets.Contains(bullet))
            {
                _bullets.Add(bullet);
                var coroutine = _coroutine = ReturnToPool(bullet.ID, _bulletLifespan).ToObservable().Subscribe();
                _coroutines.Add(coroutine);
            }
            else
            {
                _coroutines[bullet.ID] = ReturnToPool(bullet.ID, _bulletLifespan).ToObservable().Subscribe();
            }
        }

        private void OnBulletHit(int id)
        {
            var bullet = _bullets[id];
            bullet.OnBulletHit -= OnBulletHit;
            if (bullet.gameObject.activeSelf)
            {
                _bulletPool.ReturnToPool(bullet.transform);
            }
            
            _coroutines[id].Dispose();
        }

        private IEnumerator ReturnToPool(int id, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            _bullets[id].OnBulletHit -= OnBulletHit;
            if (_bullets[id].gameObject.activeSelf)
            {
                _bulletPool.ReturnToPool(_bullets[id].transform);
            }
        }
    }
}