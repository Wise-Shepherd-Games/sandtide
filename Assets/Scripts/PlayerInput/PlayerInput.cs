using UnityEngine;
using EventManagers;

namespace PlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        void Update()
        {
            GetGameObjectLeftClicked();
        }

        private void GetGameObjectLeftClicked()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
                PlayerInputEventManager.OnMouseLeftClick(hit.collider ? hit.collider.gameObject : gameObject);
            }
        }
    }
}
