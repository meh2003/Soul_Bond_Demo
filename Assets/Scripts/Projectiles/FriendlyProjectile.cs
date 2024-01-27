using UnityEngine;

namespace Demo
{
    /// <summary>
    /// FriendlyProjectile inherits from Projectile,
    /// and is not damageable (can implement ShieldShot later on)
    /// </summary>
    public abstract class FriendlyProjectile : Projectile
    {
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (hitPrefab != null) {
                ContactPoint2D contact = collision.contacts[0];
                var hitVFX = Instantiate(hitPrefab, contact.point, Quaternion.identity);
                
                DestroyVFX(hitVFX);
            }
            
            //For sound effect
            // onHit.Invoke();
            
            var entity = collision.gameObject.GetComponent<IDamageable>();
            if (entity != null)
            {
                entity.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
    }
}