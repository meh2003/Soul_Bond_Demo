using UnityEngine;
using UnityEngine.Events;

namespace Demo
{
    public class PlayerController : MonoBehaviour {
        // [SerializeField] float speed = 5f; used for keyboard control
        
        [SerializeField] GameObject model;
        [SerializeField] private Transform startPoint;

        [Header("Camera Bounds")]
        [SerializeField] private CameraBound _cameraBound;
        
        //TODO: Implement other input systems for different platforms
        // InputReader input;
        Vector3 targetPosition;
        private Vector3 lastPosition;
        private Vector3 modelOffset;
        
        private const float MovementThreshold = 0.1f;
        private Vector3 _movementThresholdVector;

        //Might need to move to another component
        [Header("Movement Event")] 
        public UnityEvent onLeftTurn;
        public UnityEvent onRightTurn;
        public UnityEvent onBackwardsTurn;
        public UnityEvent onIdle;
        
        void Start()
        {
            _movementThresholdVector = new Vector3(MovementThreshold, MovementThreshold, MovementThreshold);
            modelOffset = model.transform.position - transform.position;
            gameObject.transform.position = startPoint.position;
            // input = GetComponent<InputReader>();
        }

        void Update()
        {
            lastPosition = targetPosition;
            targetPosition = _cameraBound.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            model.transform.position = targetPosition + modelOffset;
            // targetPosition += new Vector3(input.Move.x, input.Move.y, 0f) * (speed * Time.deltaTime);
            
            targetPosition.z = 0f;
            transform.position = _cameraBound.ClampToCamera(targetPosition);
            
            if (lastPosition.x - targetPosition.x > MovementThreshold)
            {
                onLeftTurn.Invoke();
                return;
            }

            if (lastPosition.x - targetPosition.x < -MovementThreshold)
            {
                onRightTurn.Invoke();
                return;
            }

            if (lastPosition.y > targetPosition.y)
            {
                onBackwardsTurn.Invoke();
                return;
            }
            onIdle.Invoke();
        }
    }
}