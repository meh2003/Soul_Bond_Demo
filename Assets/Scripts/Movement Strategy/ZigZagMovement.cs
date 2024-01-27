using System;
using UnityEngine;

namespace Demo
{
    [CreateAssetMenu(fileName = "ZigZagMovement", menuName = "Demo/MovementStrategy/ZigZagMovement")]
    public class ZigZagMovement : MovementStrategy
    {
        [SerializeField] private float amplitude = 2.0f;
        [SerializeField] private float frequency = 2.0f;
        [SerializeField] private float startTime;
        
        // public ZigZagMovement()
        // {
        //     startTime = Time.time;
        // }

        public override void Move(Transform spawnerTransform)
        {
            float t = (Time.time - startTime) * frequency;
            float offset = Mathf.Sin(t) * amplitude;
            spawnerTransform.Translate(Vector3.right * (Time.deltaTime * 5f) + Vector3.up * offset);

            // Check if the bullet has hit the vertical edge of the camera
            if (spawnerTransform.position.x > MainCamera.orthographicSize ||
                spawnerTransform.position.x < -MainCamera.orthographicSize)
            {
                // Reverse the direction by flipping the sign of the amplitude
                amplitude *= -1.0f;
            }
        }
    }
}