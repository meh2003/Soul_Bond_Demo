using UnityEngine;

namespace Demo
{
    [CreateAssetMenu(fileName = "StraightMovement", menuName = "Demo/MovementStrategy/StraightMovement")]
    public class StraightMovement : MovementStrategy
    {
        [SerializeField] private float speed;

        private void SwitchDirection()
        {
            Direction = -Direction;
        }

        public override void Move(Transform spawnerTransform)
        {
            spawnerTransform.position += Direction * (speed * Time.deltaTime);
            // Check if the bullet has hit the vertical edge of the camera
            if (spawnerTransform.position.x >= MainCamera.transform.position.x + MainCamera.orthographicSize * 2 ||
                spawnerTransform.position.x <= MainCamera.transform.position.x - MainCamera.orthographicSize * 2)
            {
                // Reverse the direction by flipping the sign of the movement
                SwitchDirection();
            }
        }
    }
}