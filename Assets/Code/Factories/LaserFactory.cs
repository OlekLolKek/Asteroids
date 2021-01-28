using UnityEngine;


namespace DefaultNamespace
{
    public sealed class LaserFactory : IBulletFactory
    {
        private GameObject _instance;
        
        private bool _created;

        public (GameObject, BulletCollision, Rigidbody2D) Create(BulletData data)
        {
            if (!_created)
            {
                _instance = new GameObject(ObjectNames.LASER);

                _instance.layer = data.Layer;
                
                _instance.AddComponent<SpriteRenderer>().sprite = data.BulletSprite;

                var rigidbody2D = _instance.AddComponent<Rigidbody2D>();
                rigidbody2D.gravityScale = 0.0f;
                rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

                var collider = _instance.AddComponent<CapsuleCollider2D>();
                collider.size = data.BulletColliderSize;

                var bulletCollision = _instance.AddComponent<BulletCollision>();

                _instance.tag = Tags.BULLET_TAG;

                _instance.transform.localScale = new Vector3(data.SpriteScale, data.SpriteScale);

                _created = true;
            
                return (_instance, bulletCollision, rigidbody2D);
            }
            else
            {
                _instance = Object.Instantiate(_instance);
                _instance.name = ObjectNames.LASER;

                var bulletCollision = _instance.GetComponent<BulletCollision>();
                var rigidbody2D = _instance.GetComponent<Rigidbody2D>();

                return (_instance, bulletCollision, rigidbody2D);
            }
        }
    }
}