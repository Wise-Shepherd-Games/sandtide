using UnityEngine;
using EventManagers;

namespace PlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerInputActions inputActions;

        private Vector2 direction;
        public float speed = 5f;

        private void Awake()
        {
            inputActions = new();

            inputActions.Player.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => direction = Vector2.zero;
        }

        private void OnEnable() => inputActions.Player.Enable();

        private void OnDisable() => inputActions.Player.Disable();

        void Update()
        {
            Camera cam = Camera.main;
            Vector2 camPos = cam.transform.position;
            cam.transform.position = Vector2.Lerp(camPos, camPos + (direction * speed), Time.deltaTime);
            GetGameObjectLeftClicked();
        }

        private void GetGameObjectLeftClicked()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                PlayerInputEventManager.OnMouseLeftClick(hit.collider ? hit.collider.gameObject : gameObject);
            }
        }
    }
}
