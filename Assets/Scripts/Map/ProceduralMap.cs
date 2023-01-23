using UnityEngine;
using System.Collections.Generic;

namespace Map
{
    public class ProceduralMap : MonoBehaviour
    {
        public int initialChunkCount = 4;
        public Vector2Int chunkSize;
        public Tilemaps tilemaps;
        public BiomesInfo biomesInfo;
        public TerrainInfo terrainInfo;

        private List<Chunk> chunks;
        private Dictionary<Vector3Int, TileInfo> tiles;

        private void Awake()
        {
            tiles = new();
            chunks = new(initialChunkCount);

            for (int i = 0; i < initialChunkCount; i++)
                chunks.Add(new(ref tiles, chunkSize, chunkSize * i, biomesInfo, terrainInfo, tilemaps));
        }

        private void Start()
        {
            foreach (var chunk in chunks)
            {
                chunk.Initialize();
                chunk.Render();
            }
        }

    }
}