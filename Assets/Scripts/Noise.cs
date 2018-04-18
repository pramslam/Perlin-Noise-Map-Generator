using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

    // Generates a noise map
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // Ensure scale != zero
        if (scale <= 0)        {
            scale = 0.0001f;
        }

        // Loop through noiseMap
        for (int y = 0; y < mapHeight; y++)        {
            for (int x = 0; x < mapWidth; x++)            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }
}
