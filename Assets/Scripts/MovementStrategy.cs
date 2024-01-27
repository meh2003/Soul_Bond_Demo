using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo
{
    public abstract class MovementStrategy : ScriptableObject
    {
        protected Camera MainCamera;
        protected Vector3 Direction;

        public virtual void Initialize()
        {
            MainCamera = Camera.main;
        }
        
        private void OnEnable()
        {
            Direction = Vector3.right;
        }

        public abstract void Move(Transform spawnerTransform);
    }
}