using System;
using DefaultNamespace;
using UnityEngine;


namespace Controllers
{
    public class BaseBulletController : ICleanable
    {
        public event Action<int> OnBulletHit = delegate {  };
        
        private Transform _barrelTransform;
        private AudioSource _audioSource;

        private readonly BulletCollision _collision;
        private readonly Rigidbody2D _rigidbody;
        private readonly GameObject _instance;
        
        private readonly float _shootForce;
        private readonly float _damage;


        public bool IsActive 
        {
            get => _instance.activeSelf;
            private set => _instance.SetActive(value);
        }
        public int ID { get; set; }

        public BaseBulletController(BulletData bulletData, IBulletFactory factory)
        {
            var bullet = factory.Create(bulletData);
            _instance = bullet.instance;
            
            _collision = bullet.collision;
            _collision.OnBulletHit += BulletHit;

            _rigidbody = bullet.rigidbody2D;

            _shootForce = bulletData.ShootForce;
            _damage = bulletData.Damage;
        }

        public BaseBulletController InjectBarrelTransform(Transform barrel)
        {
            _barrelTransform = barrel;
            return this;
        }

        public BaseBulletController InjectAudioSource(AudioSource source)
        {
            _audioSource = source;
            return this;
        }

        public void Shoot()
        {
            IsActive = true;
            _instance.transform.position = _barrelTransform.position;
            _rigidbody.AddForce(_instance.transform.up * _shootForce);
            _audioSource.Play();
        }

        public void ReturnToPool(Transform poolRoot)
        {
            _instance.transform.SetParent(poolRoot);
            _instance.transform.localPosition = Vector3.zero;
            _instance.transform.localRotation = Quaternion.identity;
            IsActive = false;
        }

        private void BulletHit(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyCollision enemyCollision))
            {
                enemyCollision.Hit(_damage);
            }
            OnBulletHit.Invoke(ID);
        }

        public void Cleanup()
        {
            _collision.OnBulletHit -= BulletHit;
        }
    }
}