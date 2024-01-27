using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    [CreateAssetMenu(fileName = "SingleShot", menuName = "Demo/WeaponStrategy/SingleShot")]
    public class SingleShot : WeaponStrategy {
        public override void Fire(Transform firePoint, LayerMask layer) {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.layer = layer;
            
            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetDamage(Damage);
            
            Destroy(projectile, projectileLifetime);
        }
    }
}
