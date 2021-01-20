﻿using System;
using UnityEngine;


namespace DefaultNamespace
{
    public class BaseBulletController
    {
        public event Action<int> OnBulletHit = delegate {  };
        
        private Transform _barrelTransform;
        private AudioSource _audioSource;

        private readonly Rigidbody2D _rigidbody;
        private readonly GameObject _instance;
        
        private readonly float _shootForce;


        public bool IsActive 
        {
            get => _instance.activeSelf;
            private set => _instance.SetActive(value);
        }
        public int ID { get; set; }

        public BaseBulletController(BulletData bulletData, IBulletFactory factory)
        {
            _instance = factory.Create(bulletData);
            
            var collision = _instance.GetComponent<BulletCollision>();
            collision.OnBulletHit += BulletHit;

            _rigidbody = _instance.GetComponent<Rigidbody2D>();

            _shootForce = bulletData.ShootForce;
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

        private void BulletHit()
        {
            OnBulletHit.Invoke(ID);
        }
    }
}