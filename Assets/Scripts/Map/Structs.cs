using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


namespace Map
{
    [Serializable]
    public struct Tilemaps
    {
        public Tilemap lowground, ground, highground;

        public Tilemaps(Tilemap l, Tilemap g, Tilemap h)
        {
            this.ground = g;
            this.lowground = l;
            this.highground = h;
        }
    }

    [Serializable]
    public struct BiomesInfo
    {
        public List<BiomeWithChance> biomes;
        [Range(0, 1f)] public float biomesSpread;
        public float spreadDivider;
        public float rate { get => biomesSpread / spreadDivider; }

        public BiomesInfo(List<BiomeWithChance> b, float bs, float sd)
        {
            this.biomes = b;
            this.biomesSpread = bs;
            this.spreadDivider = sd;
        }
    }

    [Serializable]
    public struct TerrainInfo
    {
        public float seaLevel;
        public int maxMontainHeight;
        public const int minimumMontainHeight = 3;
        public int LayersCount { get => CountLayers(maxMontainHeight); }
        [Range(-1f, 1f)] public float montainThreshold;

        public TerrainInfo(float sl, int mmh, float mt)
        {
            this.seaLevel = sl;
            this.montainThreshold = mt;
            this.maxMontainHeight = mmh;
        }

        // Count odd between min and max
        public static int CountLayers(int maxHeight)
        {
            int N = (maxHeight - minimumMontainHeight) / 2;

            // if either R or L is odd
            if (maxHeight % 2 != 0 || minimumMontainHeight % 2 != 0)
                N++;

            return N;
        }
    }
}