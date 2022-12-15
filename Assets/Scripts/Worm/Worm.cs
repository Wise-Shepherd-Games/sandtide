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

        public Grid myGrid;
    
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
                Vector3 toCell = myGrid.CellToWorld(new Vector3Int((int)currentTargetCoordinate.x, (int)currentTargetCoordinate.y));
                transform.position = Vector2.MoveTowards(this.transform.position, toCell,
                    movSpeed * Time.deltaTime);
            }
        }

    }
}
