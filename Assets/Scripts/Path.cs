using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        private int pointsIndex = 1;
        [SerializeField] float speed;
        private bool isMoving = true;

        
        void Start()
        {
            // transform.position = points[pointsIndex].transform.position;
        }

        private void Update()
        {
           
            if (pointsIndex > points.Length - 1) return;
            if (transform.position != points[pointsIndex].transform.position && isMoving)
                transform.position = Vector3.MoveTowards(transform.position, points[pointsIndex].transform.position,
                    speed * Time.deltaTime);
        }

        public void MoveToNextPoint()
        { 
            pointsIndex += 1;
        }
        
        //TODO: Move the starting position after pose switching
        public void ResetPositionAfterViewSwitch()
        {
            transform.position = points[0].transform.position;
        }

        public void StopMoving()
        {
            isMoving = false;
        }
        
        public void StartMoving()
        {
            isMoving = true;
        }
    }
}