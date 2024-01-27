using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    /// <summary>
    /// Weapon Strategy controls the firing pattern of bullets
    /// whether it's single, twin shot, triple, v-shape, etc.
    /// </summary>
    public abstract class WeaponStrategy : ScriptableObject {
        [SerializeField] private int damage = 10;
        [SerializeField] private float fireRate = 0.5f;
        // [SerializeField] protected float projectileSpeed = 10f;
        [SerializeField] protected float projectileLifetime = 4f;
        [SerializeField] protected GameObject projectilePrefab;

        protected int Damage => damage;
        public float FireRate => fireRate;

        public virtual void Initialize() {
        }

        public abstract void Fire(Transform firePoint, LayerMask layer);
    }
}
