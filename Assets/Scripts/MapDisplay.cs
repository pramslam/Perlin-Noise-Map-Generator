using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRender;

    // Takes the noise map and turns it into a texture
    public void DrawNoiseMap(float[,] noiseMap) {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        // Set color of each pixel
        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        // Apply color to texture
        texture.SetPixels(colorMap);
        texture.Apply();

        // Apply the texture to a plane in the scene
        textureRender.sharedMaterial.mainTexture = texture;                     // sharedmaterial displays in editor instead of runtime
        textureRender.transform.localScale = new Vector3(width, 1, height);
    }
}