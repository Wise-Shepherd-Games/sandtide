using System;
using Statics;
using UnityEngine;

namespace Player
{
    public class MainController : MonoBehaviour
    {
        private static Camera mainCamera;

        [SerializeField] private float cameraMovementVelocity;
        [SerializeField] private float cameraThreshold;
        
        private void Start()
        {
            mainCamera = Camera.main;
            Cursor.lockState = CursorLockMode.Confined;
        }

        void Update()
        {
            OnWorldPlayerClick();
            EnableBuildMode();
            CameraMovement();
        }
        
        private void OnWorldPlayerClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPosition.z = 0;
                EventManager.OnPlayerClick(mouseWorldPosition);
            }
        }

        public static Vector3  GetWorldMousePosition()
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            return mouseWorldPosition;
        }

        private void EnableBuildMode()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                EventManager.OnEnableBuildMode();
            }
        }

        private void CameraMovement()
        {
            Vector3 screen = new Vector3(Screen.width, Screen.height, 0f);
            Vector3 mousePos = Input.mousePosition;
            Vector3 cameraMovement = new Vector2(0f, 0f);
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (Math.Abs(mousePos.x - screen.x) < screen.x * cameraThreshold || Math.Abs(mousePos.x - screen.x) < screen.x * cameraThreshold)
                x++;

            if (mousePos.x < screen.x * cameraThreshold )
                x--;

            if (Math.Abs(mousePos.y - screen.y) < screen.y * cameraThreshold)
                y++;

            if (mousePos.y < screen.y * cameraThreshold || Math.Abs(mousePos.y) < screen.y * cameraThreshold)
                y--;

            x *= cameraMovementVelocity;
            y *= cameraMovementVelocity;

            if (x != 0 || y != 0)
            {
                Vector3 currentPosition = mainCamera.transform.position;
                cameraMovement.x = currentPosition.x + x;
                cameraMovement.y = currentPosition.y + y;
                cameraMovement.z = currentPosition.z;
                currentPosition =
                    Vector3.Lerp(currentPosition, cameraMovement, Time.deltaTime);
                mainCamera.transform.position = currentPosition;
            }
        }
    }
}