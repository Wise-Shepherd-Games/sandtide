using System;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Map
{
    public class ProceduralMap : MonoBehaviour
    {
        public int width = 256, height = 256;
        public float seaLevel;
        [Range(0, 1f)] public float biomesSpread;
        public float spreadDivider;
        public Tilemap lowground;
        public Tilemap ground;

        [SerializeField] private List<BiomeWithChance> biomes;
        private TileInfo[,] tiles;

        private void Start()
        {
            if (biomes == null || biomes.Count == 0 || lowground == null || ground == null)
                return;

            Initialize();
            Render();
        }

        private void Initialize()
        {
            tiles = new TileInfo[width, height];
            ForEachCoordinate((x, y) => tiles[x, y] = new TileInfo(biomes[0].biome, false));

            Biomes();
            AddLakes();
        }

        private void Render()
        {
            ForEachCoordinate((x, y) =>
            {
                TileInfo tile = tiles[x, y];

                if (tile.isWater)
                {
                    lowground.SetTile(new(x, y), tile.biome.water);
                }
                else
                {
                    int randomIndex = Random.Range(0, tile.biome.groundBlocks.Length - 1);
                    ground.SetTile(new(x, y, 1), tile.biome.groundBlocks[randomIndex]);
                }

            });
        }

        private void AddLakes()
        {
            Vector2 offset = new(Random.Range(-128, 127), Random.Range(-128, 127));

            ForEachCoordinate((x, y) =>
            {
                float noise = Noise(x, y, 8, offset);

                bool isWater = false;
                if (tiles[x, y].biome.canHaveWater && noise <= seaLevel) isWater = true;

                tiles[x, y].isWater = isWater;
            });
        }

        private void Biomes()
        {
            List<Vector2Int> centerPoints = new();
            int numPoints = (int)(width * height * biomesSpread / spreadDivider);

            float totalProbability = biomes.Aggregate(0f, (acc, biome) => acc + biome.probability);

            for (int i = 0; i < numPoints; i++)
            {
                int x = Random.Range(0, width - 1);
                int y = Random.Range(0, height - 1);
                centerPoints.Add(new(x, y));

                float random = Random.Range(0, totalProbability);

                float currentProbability = 0;
                foreach ((Biome biome, float probability) in biomes)
                {
                    currentProbability += probability;
                    if (random <= currentProbability)
                    {
                        tiles[x, y].biome = biome;
                        break;
                    }
                }
            }

            foreach (Vector2Int coords in centerPoints)
            {

                TileInfo tile = tiles[coords.x, coords.y];

                int biomeHalfWidth = tile.biome.maxSize.x / 2;
                int biomeHalfHeight = tile.biome.maxSize.y / 2;

                for (int x = coords.x - biomeHalfWidth; x <= coords.x + biomeHalfWidth; x++)
                {
                    if (x < 0 || x >= width) continue;

                    for (int y = coords.y - biomeHalfHeight; y <= coords.y + biomeHalfHeight; y++)
                    {
                        if (y < 0 || y >= height) continue;

                        tiles[x, y].biome = tile.biome;
                    }
                }
            }
        }

        private float Noise(int x, int y, int scale, Vector2 offset)
        {
            float xCoord = (float)x / width * scale + offset.x;
            float yCoord = (float)y / height * scale + offset.y;
            float noise = Mathf.PerlinNoise(xCoord, yCoord);

            return noise;
        }

        private void ForEachCoordinate(Action<int, int> callback)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    callback(x, y);
                }
            }
        }

        [Serializable]
        class BiomeWithChance
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

    class TileInfo
    {
        public Biome biome;
        public bool isWater;

        public TileInfo(Biome biome, bool isWater)
        {
            this.biome = biome;
            this.isWater = isWater;
        }
    }
}