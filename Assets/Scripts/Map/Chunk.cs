using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Map
{
    class Chunk
    {
        private Dictionary<Vector3Int, TileInfo> tiles;
        private BiomesInfo biomesInfo;
        private Tilemaps tilemaps;
        private TerrainInfo terrainInfo;
        private Vector2Int size;
        private Vector2Int offset;

        public Chunk(ref Dictionary<Vector3Int, TileInfo> tiles, Vector2Int size, Vector2Int offset, BiomesInfo bi, TerrainInfo ti, Tilemaps tm)
        {
            this.tiles = tiles;
            this.size = size;
            this.offset = offset;
            this.biomesInfo = bi;
            this.terrainInfo = ti;
            this.tilemaps = tm;
        }

        public void Initialize()
        {
            ForEachCoordinate(size, offset, (coord) =>
            {
                tiles[coord] = new(biomesInfo.biomes[0].biome, false);
            });

            Biomes(tiles, size, biomesInfo.rate, biomesInfo.biomes);
            AddLakes(tiles, size, offset, terrainInfo.seaLevel);
            AddMountains(tiles, size, offset, terrainInfo.maxMontainHeight, terrainInfo.montainThreshold);
        }

        public void Render()
        {
            ForEachCoordinate(new Vector3Int(size.x, size.y, terrainInfo.LayersCount), offset, (coord) =>
            {
                if (!tiles.ContainsKey(coord)) return;

                TileInfo tile = tiles[coord];

                if (coord.z > 0)
                {
                    if (tile == null) return;
                    int randomIndex = Random.Range(0, tile.biome.upperBlocks.Length - 1);
                    tilemaps.highground.SetTile(new(coord.x, coord.y, coord.z * 2 + 1), tile.biome.upperBlocks[randomIndex]);
                }
                else if (tile.isWater)
                {
                    tilemaps.lowground.SetTile(new(coord.x, coord.y, 0), tile.biome.water);
                }
                else
                {
                    int randomIndex = Random.Range(0, tile.biome.groundBlocks.Length - 1);
                    tilemaps.ground.SetTile(new(coord.x, coord.y, 1), tile.biome.groundBlocks[randomIndex]);
                }

            });
        }

        private static void AddLakes(Dictionary<Vector3Int, TileInfo> tiles, Vector2Int size, Vector2Int offset, float seaLevel, int scale = 8)
        {
            Vector2 random = new(Random.Range(-128, 127), Random.Range(-128, 127));

            ForEachCoordinate(size, offset, (coord) =>
            {
                float xCoord = (float)coord.x / size.x * scale + random.x;
                float yCoord = (float)coord.y / size.y * scale + random.y;
                float noise = Mathf.PerlinNoise(xCoord, yCoord);

                bool isWater = false;
                if (tiles[coord].biome.canHaveWater && noise <= seaLevel) isWater = true;

                tiles[coord].isWater = isWater;
            });
        }

        private static void Biomes(Dictionary<Vector3Int, TileInfo> tiles, Vector2Int size, float rate, List<BiomeWithChance> biomes)
        {
            List<Vector2Int> centerPoints = new();
            int numPoints = (int)(size.x * size.y * rate);

            float totalProbability = biomes.Aggregate(0f, (acc, biome) => acc + biome.probability);

            for (int i = 0; i < numPoints; i++)
            {
                int x = Random.Range(0, size.x - 1);
                int y = Random.Range(0, size.y - 1);
                centerPoints.Add(new(x, y));

                float random = Random.Range(0, totalProbability);

                float currentProbability = 0;
                foreach ((Biome biome, float probability) in biomes)
                {
                    currentProbability += probability;
                    if (random <= currentProbability)
                    {
                        tiles[new(x, y)].biome = biome;
                        break;
                    }
                }
            }

            foreach (Vector2Int coords in centerPoints)
            {
                TileInfo tile = tiles[new(coords.x, coords.y, 0)];

                int biomeHalfWidth = tile.biome.maxSize.x / 2;
                int biomeHalfHeight = tile.biome.maxSize.y / 2;

                for (int x = coords.x - biomeHalfWidth; x <= coords.x + biomeHalfWidth; x++)
                {
                    if (x < 0 || x >= size.x) continue;
                    for (int y = coords.y - biomeHalfHeight; y <= coords.y + biomeHalfHeight; y++)
                    {
                        if (y < 0 || y >= size.y) continue;
                        tiles[new(x, y)].biome = tile.biome;
                    }
                }
            }
        }

        private static void AddMountains(Dictionary<Vector3Int, TileInfo> tiles, Vector2Int size, Vector2Int offset, int maxMontainHeight, float montainThreshold)
        {
            const float scale = 0.0625f;

            ForEachCoordinate(size, offset, (coord) =>
            {
                TileInfo tile = tiles[coord];
                if (tile.isWater) return;

                int currentHeight = maxMontainHeight;

                for (int i = 0; currentHeight > TerrainInfo.minimumMontainHeight - 2; currentHeight -= 2, i++)
                {
                    float rng = Mathf.Abs(1 - 0.25f * (i + 1));
                    if (Random.value <= rng) continue;

                    float fbm = FractalBrownianMotion.Calculate(new(coord.x * scale, coord.y * scale), 6);
                    if (fbm >= montainThreshold) break;
                }

                if (currentHeight <= TerrainInfo.minimumMontainHeight - 2) return;

                int oddCount = TerrainInfo.CountLayers(currentHeight);
                for (int i = 0; i < oddCount; i++)
                    tiles[new(coord.x, coord.y, i + 1)] = new(tile.biome, false);
            });
        }

        private static void ForEachCoordinate(Vector2Int size, Vector2Int offset, Action<Vector3Int> callback)
        {
            for (int x = 0 + offset.x; x < size.x + offset.x; x++)
            {
                for (int y = 0 + offset.y; y < size.y + offset.y; y++)
                {
                    callback(new(x, y));
                }
            }
        }

        private static void ForEachCoordinate(Vector3Int size, Vector2Int offset, Action<Vector3Int> callback)
        {
            ForEachCoordinate(((Vector2Int)size), offset, (coord) =>
            {
                for (int z = 0; z < size.z; z++)
                {
                    callback(new(coord.x, coord.y, z));
                }
            });
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