using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo
{
    public class BulletSpawnerMovement : MonoBehaviour
    {
        [SerializeField] protected MovementStrategy movementStrategy;

        private void Start()
        {
            movementStrategy.Initialize();
        }

        public void SetMovementStrategy(MovementStrategy strategy)
        {
            movementStrategy = strategy;
            movementStrategy.Initialize();
        }

        private void Update()
        {
            movementStrategy.Move(gameObject.transform);
        }
    }
}