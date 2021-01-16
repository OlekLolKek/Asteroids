using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public class ShootController : IExecutable
    {
        #region Fields

        private readonly BulletData _data;
        
        private readonly List<IDisposable> _coroutines = new List<IDisposable>();
        private readonly List<BaseBulletController> _bullets = new List<BaseBulletController>();
        private readonly Transform _barrelTransform;
        private readonly BulletPool _bulletPool;
        private readonly AudioSource _audioSource;
        private readonly float _bulletLifespan;
        private float _shootCooldown;
        private float _timer;

        #endregion
        

        public ShootController(BulletData bulletData, PlayerModel playerModel, 
            LaserFactory laserFactory)
        {
            _data = bulletData;
            
            var ratio = _data.BulletLifespan / _data.ShootCooldown;
            var poolSize = (int) Math.Ceiling(ratio);
            _bulletPool = new BulletPool(poolSize, _data, laserFactory);
            
            _barrelTransform = playerModel.BarrelTransform;
            _audioSource = playerModel.AudioSource;
            playerModel.OnShootCooldownChanged += SetShootCooldown;
            
            _bulletLifespan = _data.BulletLifespan;
            _shootCooldown = _data.ShootCooldown;
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
                
                bullet.OnBulletHit += OnBulletHit;
                bullet.Shoot();
            }
        }

        private void ManagePool(BaseBulletController bullet)
        {
            if (!_bullets.Contains(bullet))
            {
                _bullets.Add(bullet);
                var coroutine = ReturnToPool(bullet.ID, _bulletLifespan).ToObservable().Subscribe();
                _coroutines.Add(coroutine);
                bullet.InjectAudioSource(_audioSource).InjectBarrelTransform(_barrelTransform);
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
            if (bullet.IsActive)
            {
                _bulletPool.ReturnToPool(bullet);
            }
            
            _coroutines[id].Dispose();
        }

        private IEnumerator ReturnToPool(int id, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            _bullets[id].OnBulletHit -= OnBulletHit;
            if (_bullets[id].IsActive)
            {
                _bulletPool.ReturnToPool(_bullets[id]);
            }
        }

        public void SetShootCooldown(float cooldown)
        {
            _shootCooldown = cooldown;
        }

        private void ResetShootCooldown()
        {
            _shootCooldown = _data.ShootCooldown;
        }
    }
}