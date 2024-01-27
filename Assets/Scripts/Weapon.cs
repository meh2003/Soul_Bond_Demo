using Utilities;
using UnityEngine;

namespace Demo
{
    public abstract class Weapon : MonoBehaviour {
        [SerializeField] protected WeaponStrategy weaponStrategy;
        [SerializeField] protected Transform firePoint;
        [SerializeField, Layer] protected int layer;
        void Start() => weaponStrategy.Initialize();
        
        public void SetWeaponStrategy(WeaponStrategy strategy) {
            weaponStrategy = strategy;
            weaponStrategy.Initialize();
        }
    }
}
