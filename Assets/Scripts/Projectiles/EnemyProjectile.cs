using System;
using UnityEngine;

namespace Demo
{
    public abstract class EnemyProjectile : Projectile, IDamageable
    {
        [SerializeField] protected int maxHealth;
        private int _health;
        
        protected virtual void Awake() => _health = maxHealth;

        public void SetMaxHealth(int amount) => maxHealth = amount;
        
        public int GetMaxHealth => maxHealth;

        public void TakeDamage(int amount)
        {
            _health -= amount;
            if (_health <= 0)
            {
                Die();
            }
        }
        
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (hitPrefab != null) {
                ContactPoint2D contact = collision.contacts[0];
                var hitVFX = Instantiate(hitPrefab, contact.point, Quaternion.identity);
                
                DestroyVFX(hitVFX);
            }
            
            var entities = collision.gameObject.GetComponents<IDamageable>();
            if (entities == null) return;
            foreach (var entity in entities)
            {
                entity.TakeDamage(Damage);
            }
        }

        private void OnDestroy()
        {
            Die();
        }

        protected abstract void Die();
    }
}