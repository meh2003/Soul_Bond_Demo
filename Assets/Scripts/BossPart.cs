using System;
using Spine.Unity;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Demo
{
    public class BossPart : Damageable, IDamageable
    {
        
        [SerializeField] private GameObject destructionVFX;
        private Collider2D partCollider;
        
        //Trigger clothing destroy animation, camera swap and sound effect
        public UnityEvent onSystemDestroyed;

        protected override void Awake()
        {
            base.Awake();
            partCollider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            partCollider.enabled = true;
        }

        private void Update()
        {
            
        }

        protected override void Die()
        {
            //TODO: trigger next phases
            if (destructionVFX != null)
            {
                Instantiate(destructionVFX, transform.position, quaternion.identity);
            }
            //TODO: remove the corresponding assets
            onSystemDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
    
}