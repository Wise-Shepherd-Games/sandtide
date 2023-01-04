using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{

    [CreateAssetMenu(fileName = "Biome", menuName = "New Biome", order = 0)]
    public class Biome : ScriptableObject
    {
        public AnimatedTile water;
        public Tile[] groundBlocks;
        public Tile[] upperBlocks;
        public Vector2Int maxSize;
        public bool canHaveWater;
    }
}