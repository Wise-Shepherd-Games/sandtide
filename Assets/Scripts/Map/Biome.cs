using UnityEngine;

namespace Map
{
    [CreateAssetMenu(fileName = "Biome", menuName = "New Biome", order = 0)]
    public class Biome : ScriptableObject
    {
        public Color color;
        public Vector2Int maxSize;
        public bool canHaveWater;
    }
}