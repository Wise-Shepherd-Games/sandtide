using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Map
{
    public class ProceduralMap : MonoBehaviour
    {
        public int width = 256, height = 256;
        [Range(0, 1f)] public float biomesSpread;

        [SerializeField] private List<BiomeWithChance> biomes;
        private Tile[,] tiles;

        private void Start()
        {
            if (biomes == null || biomes.Count == 0) return;

            Initialize();
            Render();
        }

        private void Initialize()
        {
            tiles = new Tile[width, height];
            ForEachCoordinate((x, y) => tiles[x, y] = new Tile(biomes[0].biome, false));

            Biomes();
            AddLakes();
        }

        private void Render()
        {
            Texture2D texture = new(width, height, TextureFormat.RGB24, false);
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            ForEachCoordinate((x, y) =>
            {
                Tile tile = tiles[x, y];
                texture.SetPixel(x, y, tile.isWater ? Color.blue : tile.biome.color);
            });

            texture.Apply();
            renderer.sprite = Sprite.Create(texture, new(0, 0, width, height), new(.5f, .5f));
        }

        private void AddLakes()
        {
            const float landThreshold = .12f;
            Vector2 offset = new(Random.Range(-128, 127), Random.Range(-128, 127));

            ForEachCoordinate((x, y) =>
            {
                float noise = Noise(x, y, 8, offset);

                bool isWater = false;
                if (tiles[x, y].biome.canHaveWater && noise < landThreshold) isWater = true;

                tiles[x, y].isWater = isWater;
            });
        }

        private void Biomes()
        {
            List<Vector2Int> centerPoints = new();
            int numPoints = (int)(width * height * biomesSpread);

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

                Tile tile = tiles[coords.x, coords.y];

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

    class Tile
    {
        public Biome biome;
        public bool isWater;

        public Tile(Biome biome, bool isWater)
        {
            this.biome = biome;
            this.isWater = isWater;
        }
    }
}