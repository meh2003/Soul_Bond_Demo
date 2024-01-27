using UnityEngine;

namespace Demo
{
    [CreateAssetMenu(fileName = "TwinShot", menuName = "Demo/WeaponStrategy/TwinShot")]
    public class TwinShot : WeaponStrategy
    {
        private const float DistanceY = 0.1f;

        public override void Fire(Transform firePoint, LayerMask layer)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                var projectile = Instantiate(projectilePrefab, firePoint.position + new Vector3(0, DistanceY * i, 0), firePoint.rotation);
                projectile.transform.SetParent(firePoint);
                projectile.layer = layer;

                var projectileComponent = projectile.GetComponent<Projectile>();
                projectileComponent.SetDamage(Damage);

                Destroy(projectile, projectileLifetime);
            }
        }
    }
}