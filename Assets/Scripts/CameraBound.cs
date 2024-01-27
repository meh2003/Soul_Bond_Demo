using UnityEngine;

namespace Demo
{
    public class CameraBound : MonoBehaviour
    {
        [Header("Camera Bounds")]
        public Camera mainCamera;

        //Bounds player to main camera
        [SerializeField] float minX = -8f;
        [SerializeField] float maxX = 8f;
        [SerializeField] float minY = -4f;
        [SerializeField] float maxY = 4f;
        
        Vector3 currentVelocity;
        [SerializeField] float smoothness = 0.1f;

        public Vector3 ClampToCamera(Vector3 targetPosition)
        {
            targetPosition.z = 0f;

            // Calculate the min and max X and Y positions for the player based on the camera view
            var position = mainCamera.transform.position;
            var minPlayerX = position.x + minX;
            var maxPlayerX = position.x + maxX;
            var minPlayerY = position.y + minY;
            var maxPlayerY = position.y + maxY;

            // Clamp the player's position to the camera view
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY); ;

            // Lerp the player's position to the target position
            return Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
        }
    }
}