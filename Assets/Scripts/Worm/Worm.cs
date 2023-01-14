using UnityEngine;

namespace Worm
{
    public class Worm : MonoBehaviour
    {
        private Vector2 currentTargetCoordinate;
        [SerializeField, Header("Target Coordinate")]
        private Vector2 targetCoordinate;

        [SerializeField, Header("Movement Speed")]
        private float movSpeed;

        void OnEnable()
        {
            //EventManager.MakeNoise += ChangeTarget;
        }

        void Update()
        {
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            if (currentTargetCoordinate != targetCoordinate)
                currentTargetCoordinate = targetCoordinate;
            else
            {
                transform.position = Vector2.MoveTowards
                    (this.transform.position,
                    currentTargetCoordinate,
                    movSpeed * Time.deltaTime);
            }
        }

        private void ChangeTarget(Vector2 position)
        {
            targetCoordinate = position;
        }

    }
}
