using UnityEngine;
using UnityEngine.UIElements;

namespace Demo
{
    /// <summary>
    /// Spirit Bomb clears all enemy projectiles and prevents them from spawning for 2 seconds
    /// Deal 50 damages to the current boss stage
    /// </summary>
    public class SpiritBomb : FriendlyProjectile
    {
        [SerializeField] private GameObject onDestroyPrefab;
        private Camera mainCam;
        void Start()
        {
            var collider = GetComponent<Collider2D>();
            if (collider != null) collider.enabled = false;
            mainCam = Camera.main;
        }

        private void OnDestroy()
        {
            if (onDestroyPrefab != null) {
                var onDestroyVFX = Instantiate(onDestroyPrefab, gameObject.transform.position, Quaternion.identity);
                DestroyVFX(onDestroyVFX);
            }

            GamePlayManager.DestroyProjectiles();
            //TODO: Damage boss parts of the current stage
        }

        public override void OnFrame()
        {
            transform.up = mainCam.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, mainCam.transform.position, speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, mainCam.transform.position) < 0.001) Destroy(gameObject);
        }
    }
}