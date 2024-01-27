
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo {
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour {
        // NOTE: Make sure to set the Player Input component to C# events
        PlayerInput playerInput;
        InputAction moveAction;
        InputAction fireAction;
        private InputAction secondaryFireAction;
        private InputAction shieldAction;
        private InputAction switchViewAction;
        
        public Vector2 Move => moveAction.ReadValue<Vector2>();
        public bool Fire => fireAction.ReadValue<float>() > 0f;
        public bool SecondaryFire => secondaryFireAction.ReadValue<float>() > 0f;
        public bool Shield => shieldAction.ReadValue<float>() > 0f;
        public bool SwitchView => switchViewAction.ReadValue<float>() > 0f;

        void Start() {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            fireAction = playerInput.actions["Fire"];
            secondaryFireAction = playerInput.actions["Secondary Fire"];
            shieldAction = playerInput.actions["Shield"];
            switchViewAction = playerInput.actions["Switch View"];
        }
    }
}

