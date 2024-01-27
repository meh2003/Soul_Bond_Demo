using Unity.VisualScripting;
using UnityEngine;

namespace Demo
{
    /// <summary>
    /// Yellow Snowflake is a medium-sized enemy projectile
    /// splitting on destroy
    /// </summary>
    public class YellowSnowflake : Snowflake
    {
        [Header("Main Bullet Attribute")]
        [SerializeField] private int numberOfProjectiles;
        [SerializeField] private GameObject splitProjectilePrefab;
        [SerializeField] private float spinSpeed;
        private Vector3 _spinVector3;
        private Vector3 _moveDirection;
        
        [Header("Split Bullet Attribute")]
        //Subjected to change, moved to the prefab
        [SerializeField] private float splitSpeed;
        [SerializeField] private float splitProjectileLifeTime;
        
        // [SerializeField] private float angleStart

        void Start()
        {
            _spinVector3 = new Vector3(0f, 0f, spinSpeed);
        }

        protected override void Die()
        {
            var t = transform;
            var lastPosition = t.position;
            var lastRotation = t.rotation;
            var firePoint = t.parent;
            
            var angle = 360f / numberOfProjectiles;
            for (int i = 0; i <= numberOfProjectiles; i++)
            {
                var splitProjectile = Instantiate(splitProjectilePrefab, lastPosition, lastRotation);
                splitProjectile.transform.SetParent(firePoint, true);
                splitProjectile.transform.Rotate(0f, 0f, angle * (i - 1));

                var splitProjectileComponent = splitProjectile.GetComponent<Projectile>();
                splitProjectileComponent.SetSpeed(splitSpeed);
                splitProjectileComponent.SetDamage(Damage);
                
                Destroy(splitProjectile, splitProjectileLifeTime);
            }
            base.Die();
        }

        public override void OnFrame()
        {
            base.OnFrame();
            // gameObject.transform.Rotate(_spinVector3 * Time.deltaTime);
        }
    }
}