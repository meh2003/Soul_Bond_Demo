using System;
using UnityEngine;
using UnityEngine.Events;

namespace Demo
{
    
    //Remove later
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField] protected int maxHealth;
        private int _health;
        public UnityEvent<int> onTakingDamage;

        protected virtual void Awake() => _health = maxHealth;

        public void SetMaxHealth(int amount) => maxHealth = amount;
        
        public int GetMaxHealth => maxHealth;

        private void Start()
        {
            _health = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;
            onTakingDamage?.Invoke(amount);
            if (_health <= 0)
            {
                Die();
            }
        }

        public int GetHealth => _health;

        public float GetHealthNormalized() => _health / (float)maxHealth;


        protected abstract void Die();
    }

    //Implement
    public interface IDamageable
    {
        public void TakeDamage(int amount);
    }
}