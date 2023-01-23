using UnityEngine;
using EventManagers;
using UnityEngine.InputSystem;

namespace PlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerInputActions inputActions;

        private Vector2 direction;
        public float speed = 5f;

        private void Awake() => inputActions = new();

        private void OnEnable()
        {
            inputActions.Player.Move.performed += OnMovePerformed;
            inputActions.Player.Move.canceled += OnMoveCanceled;

            inputActions.Player.LeftClick.performed += OnLeftClickPerformed;

            inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            inputActions.Player.Move.performed -= OnMovePerformed;
            inputActions.Player.Move.canceled -= OnMoveCanceled;

            inputActions.Player.LeftClick.performed -= OnLeftClickPerformed;

            inputActions.Player.Disable();
        }

        void Update()
        {
            Camera cam = Camera.main;
            Vector2 camPos = cam.transform.position;
            Vector2 newPos = Vector2.Lerp(camPos, camPos + direction * speed, Time.deltaTime);
            cam.transform.position = new(newPos.x, newPos.y, cam.transform.position.z);
        }

        private void OnLeftClickPerformed(InputAction.CallbackContext ctx)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            PlayerInputEventManager.OnMouseLeftClick(hit.collider ? hit.collider.gameObject : gameObject);
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx) => direction = ctx.ReadValue<Vector2>();
        private void OnMoveCanceled(InputAction.CallbackContext ctx) => direction = Vector2.zero;
    }
}
