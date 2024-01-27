using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace Demo
{
    [CreateAssetMenu(fileName = "HomingShot", menuName = "Demo/WeaponStrategy/HomingShot")]
    public class HomingShot : WeaponStrategy
    {
        [Range(0.0f, 1.0f)]
        [SerializeField] float trackingSpeed;

        Transform target;
        
        public override void Initialize() {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public override void Fire(Transform firePoint, LayerMask layer)
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.layer = layer;
            
            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetDamage(Damage);
            // projectileComponent.Callback = () =>
            // {
            //     if (Mathf.Abs(Vector3.Distance(target.position, projectile.transform.position)) > 0.5f)
            //     {
            //         Vector3 look = projectile.transform.InverseTransformPoint(target.transform.position);
            //         float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg * trackingSpeed;
            //         projectile.transform.Rotate(0, 0, angle - 90);
            //     }
            // };
            
            Destroy(projectile, projectileLifetime);
        }
    }
}