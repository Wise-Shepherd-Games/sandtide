using UnityEngine;

namespace Map
{
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