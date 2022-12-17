using Statics;
using Build;
using UnityEngine;

namespace Player
{
    public class MainController : MonoBehaviour
    {
        private static Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            OnWorldPlayerClick();
            EnableBuildMode();
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
                Builder.BuildingState = !Builder.BuildingState;
            }
        }
    }
}