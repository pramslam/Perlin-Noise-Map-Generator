using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

    public enum NormalizeMode { Local, Global};

    // Generates a noise map
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset, NormalizeMode normalizeMode)    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // Handles seed
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];             // Each octave sampled from a different location

        float maxPossibleHeight = 0;
        float amplitude = 1;
        float frequency = 1;

        for (int i = 0; i < octaves; i++) {
            // Prevents too high of number given to Mathf.Perlin Noise or will return same value
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude;
            amplitude *= persistance;
        }

        // Ensure scale != zero
        if (scale <= 0) {
            scale = 0.0001f;
        }

        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2.0f;
        float halfHeight = mapHeight / 2.0f;

        // Loop through noiseMap
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                amplitude = 1;
                frequency = 1;
                float noiseHeight = 0;

                // Loop through octaves
                for (int i = 0; i < octaves; i++) {
                    float sampleX = (x - halfWidth + octaveOffsets[i].x) / scale * frequency;
                    float sampleY = (y - halfHeight + octaveOffsets[i].y) / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;        // Allows perlin noise value to sometimes be negative
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                // Updates max/min values of noiseHeight
                if (noiseHeight > maxLocalNoiseHeight) {
                    maxLocalNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minLocalNoiseHeight) {
                    minLocalNoiseHeight = noiseHeight;
                }

                // Apply noiseHeight to noiseMap
                noiseMap[x, y] = noiseHeight;
            }
        }

        // Normalize noiseMap
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                if (normalizeMode == NormalizeMode.Local) {
                    noiseMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x, y]);
                }
                else {
                    float normalizedHeight = (noiseMap[x, y] + 1) / maxPossibleHeight;
                    noiseMap[x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue);
                }
            }
        }

        return noiseMap;
    }
}
