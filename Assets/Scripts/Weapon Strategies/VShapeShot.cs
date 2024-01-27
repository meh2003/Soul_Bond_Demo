using System;
using UnityEngine;

namespace Demo
{
    [CreateAssetMenu(fileName = "VShapeShot", menuName = "Demo/WeaponStrategy/VShapeShot")]
    public class VShapeShot : WeaponStrategy
    {
        [SerializeField] private float distanceX; //horizontal distance between bullets
        [SerializeField] private float distanceY; //vertical distance between bullets
        [SerializeField] private int numberOfProjectiles;

        public override void Fire(Transform firePoint, LayerMask layer)
        {
            for (var i = -numberOfProjectiles / 2; i <= numberOfProjectiles / 2; i++)
            {
                if (numberOfProjectiles % 2 == 0 && i == 0) continue;
                
                var projectile = Instantiate(projectilePrefab, firePoint.position +  new Vector3(distanceX * i , distanceY * Math.Abs(i), 0), firePoint.rotation);
                projectile.transform.SetParent(firePoint);
                projectile.layer = layer;

                var projectileComponent = projectile.GetComponent<Projectile>();
                projectileComponent.SetDamage(Damage);

                Destroy(projectile, projectileLifetime);
            }
        }
    }
}