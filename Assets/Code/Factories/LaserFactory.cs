using UnityEngine;


namespace DefaultNamespace
{
    public sealed class LaserFactory : IBulletFactory
    {
        public GameObject Create(BulletData data)
        {
            var bullet = new GameObject(NameManager.LASER);

            bullet.AddComponent<SpriteRenderer>().sprite = data.BulletSprite;

            var rigidbody = bullet.AddComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0.0f;
            rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            var collider = bullet.AddComponent<CapsuleCollider2D>();
            collider.size = data.BulletColliderSize;

            bullet.AddComponent<BulletCollision>();

            bullet.tag = TagManager.BULLET_TAG;

            bullet.transform.localScale = new Vector3(data.SpriteScale, data.SpriteScale);
            
            return bullet;
        }
    }
}