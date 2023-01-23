using System;
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

    [Serializable]
    public class BiomeWithChance
    {
        public Biome biome;
        [Range(0, 1f)] public float probability;

        public BiomeWithChance(Biome biome, float probability)
        {
            this.biome = biome;
            this.probability = probability;
        }

        public void Deconstruct(out Biome biome, out float probability)
        {
            biome = this.biome;
            probability = this.probability;
        }
    }
}