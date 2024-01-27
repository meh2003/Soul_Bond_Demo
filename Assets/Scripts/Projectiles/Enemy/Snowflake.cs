using UnityEngine;

namespace Demo
{
    /// <summary>
    /// Snowflake is an enemy projectile
    /// falling in a sine wave pattern
    /// </summary>
    public class Snowflake : EnemyProjectile
    {
        [SerializeField] private float frequency = 2.5f;
        [SerializeField] private float magnitude = 0.01f;
        [SerializeField] private float offset = 0f;
        private float startTime;

        protected override void Awake()
        {
            base.Awake();
            startTime = Time.time;
        }

        protected override void Die()
        {
            Destroy(gameObject);
        }

        public override void OnFrame()
        {
            transform.position += transform.up * (speed * Time.deltaTime);
            transform.position += transform.right * (Mathf.Sin((Time.time - startTime) * frequency + offset) * magnitude);
        }
    }
}