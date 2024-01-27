using System;
using System.Collections;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

namespace Demo
{
    /// <summary>
    /// The Projectile class handles attributes of each projectile
    /// </summary>
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected GameObject hitPrefab;
        private float _vfxDuration = 0.67777f;// TODO: Get length of the onHit/destroy animation
        private Transform _parent;
        protected int Damage;

        public void SetSpeed(float speed) => this.speed = speed;
        public void SetParent(Transform parent) => _parent = parent;
        public void SetDamage(int damage) => Damage = damage;
        // public Action Callback;
        void Update()
        {
            transform.SetParent(null);
            OnFrame();
            // transform.position += transform.up * (speed * Time.deltaTime);
            // use for tracking projectiles
            //Callback?.Invoke();
        }

        //Can move to an util perhaps?
        protected void DestroyVFX(GameObject vfx)
        {
            var ps = vfx.GetComponent<SkeletonAnimation>();
            if (ps != null)
            {
                var anim = ps.Skeleton.Data.FindAnimation("fx-bullet");
                if (anim != null) _vfxDuration = anim.Duration;
            }
            Destroy(vfx, _vfxDuration);
        }

        public abstract void OnFrame();
    }
}