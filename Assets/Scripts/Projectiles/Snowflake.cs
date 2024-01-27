using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace Demo
{
    public abstract class Snowflake : MonoBehaviour
    {
        public enum BulletType
        {
            White,
            Yellow,
            Blue
        }
        
        [SerializeField] protected float speed;
        [SerializeField] protected int damage;
        [SerializeField] protected int health;
        [SerializeField] public BulletType type;
        [SerializeField] private Player player;

        private Vector3 _position;
        private Vector3 _direction;

        void Start()
        {
            _position = transform.position;
            _direction = Vector3.forward;
        }
        
        void Update()
        {
            UpdatePosition();
            if (health <= 0) OnDestroy();
            OnFrame();
        }

        // TODO: Math
        void UpdatePosition()
        {
            // Apply speed
            // Rotate
            // Save variables
        }

        private void OnTriggerEnter(Collider other)
        {
            //if (other.gameObject.tag = "Bullet") health -= player.damage;
        }

        protected abstract void OnFrame();
        protected abstract void OnDestroy();
    }
}
