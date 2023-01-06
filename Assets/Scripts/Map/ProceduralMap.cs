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
        public int maxMontainHeight = 5;
        [Range(-1f, 1f)] public float montainThreshold = 0.5f;
        [Range(0, 1f)] public float biomesSpread;
        public float spreadDivider;
        public Tilemap lowground;
        public Tilemap ground;
        public Tilemap highground;

        [SerializeField] private List<BiomeWithChance> biomes;
        private int minimumMontainHeight = 3;
        private TileInfo[,,] tiles;

        private void Start()
        {
            if (biomes == null || biomes.Count == 0 || lowground == null || ground == null || highground == null)
                return;

            Initialize();
            Render();
        }

        private void Initialize()
        {
            tiles = new TileInfo[width, height, CountOdd(minimumMontainHeight, maxMontainHeight) + 1];
            ForEachCoordinate((x, y) => tiles[x, y, 0] = new TileInfo(biomes[0].biome, false));

            Biomes();
            AddLakes();
            AddMountains();
        }

        private void Render()
        {
            ForEachCoordinate((x, y, z) =>
            {
                TileInfo tile = tiles[x, y, z];

                if (z > 0)
                {
                    if (tile == null) return;
                    int randomIndex = Random.Range(0, tile.biome.upperBlocks.Length - 1);
                    highground.SetTile(new(x, y, z * 2 + 1), tile.biome.upperBlocks[randomIndex]);
                }
                else if (tile.isWater)
                {
                    lowground.SetTile(new(x, y, 0), tile.biome.water);
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
                float xCoord = (float)x / width * 8 + offset.x;
                float yCoord = (float)y / height * 8 + offset.y;
                float noise = Mathf.PerlinNoise(xCoord, yCoord);

                bool isWater = false;
                if (tiles[x, y, 0].biome.canHaveWater && noise <= seaLevel) isWater = true;

                tiles[x, y, 0].isWater = isWater;
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
                        tiles[x, y, 0].biome = biome;
                        break;
                    }
                }
            }

            foreach (Vector2Int coords in centerPoints)
            {

                TileInfo tile = tiles[coords.x, coords.y, 0];

                int biomeHalfWidth = tile.biome.maxSize.x / 2;
                int biomeHalfHeight = tile.biome.maxSize.y / 2;

                for (int x = coords.x - biomeHalfWidth; x <= coords.x + biomeHalfWidth; x++)
                {
                    if (x < 0 || x >= width) continue;

                    for (int y = coords.y - biomeHalfHeight; y <= coords.y + biomeHalfHeight; y++)
                    {
                        if (y < 0 || y >= height) continue;

                        tiles[x, y, 0].biome = tile.biome;
                    }
                }
            }
        }

        private void AddMountains()
        {
            ForEachCoordinate((x, y) =>
            {
                TileInfo tile = tiles[x, y, 0];
                if (tile.isWater) return;

                int currentHeight = maxMontainHeight;
                const float scale = 0.0625f;

                for (int i = 0; currentHeight > minimumMontainHeight - 2; currentHeight -= 2, i++)
                {
                    float rng = Mathf.Abs(1 - 0.25f * (i + 1));
                    if (Random.value <= rng) continue;

                    float fbm = FractalBrownianMotion.Calculate(new(x * scale, y * scale), 6);
                    if (fbm >= montainThreshold) break;
                }

                if (currentHeight <= minimumMontainHeight - 2) return;

                int oddCount = CountOdd(minimumMontainHeight, currentHeight);
                for (int i = 0; i < oddCount; i++)
                    tiles[x, y, i + 1] = new(tile.biome, false);
            });
        }

        private void ForEachCoordinate(Action<int, int> callback)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    callback(x, y);
                }
            }
        }
        private void ForEachCoordinate(Action<int, int, int> callback)
        {
            ForEachCoordinate((x, y) =>
            {
                for (int z = 0; z < tiles.GetLength(2); z++)
                {
                    callback(x, y, z);
                }
            });
        }
        private int CountOdd(int L, int R)
        {
            int N = (R - L) / 2;

            // if either R or L is odd
            if (R % 2 != 0 || L % 2 != 0)
                N++;

            return N;
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

    static class FractalBrownianMotion
    {
        public static float Calculate(Vector2 vec, int octaves)
        {
            float value = 0, amplitude = 0.5f;
            float lacunarity = 2, gain = 0.5f;

            for (int i = 0; i < octaves; i++)
            {
                value += amplitude * Noise(vec);

                vec *= lacunarity;
                amplitude *= gain;
            }

            return value;
        }

        private static float Noise(Vector2 vec)
        {
            Vector2Int i = Vector2Int.FloorToInt(vec);
            Vector2 f = new(vec.x % 1, vec.y % 1);

            float a = Random(i);
            float b = Random(i + Vector2.right);
            float c = Random(i + Vector2.up);
            float d = Random(i + new Vector2(1, 1));

            Vector2 u = f * f * (new Vector2(3, 3) - (2f * f));

            return Mathf.Lerp(a, b, u.x) + (c - a) * u.y * (1f - u.x) + (d - b) * u.x * u.y;
        }

        private static float Random(Vector2 vec)
        {
            float dot = Vector2.Dot(vec, new(12.9898f, 78.233f));
            float sin = Mathf.Sin(dot);
            float value = sin * 43758.5453123f;
            return value % 1;
        }
    }
}