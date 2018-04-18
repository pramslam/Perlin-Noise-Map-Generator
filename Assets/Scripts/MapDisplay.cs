using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRender;

    // Takes the noise map and turns it into a texture
    public void DrawTexture(Texture2D texture) {
        // Apply the texture to a plane in the scene
        textureRender.sharedMaterial.mainTexture = texture;                 // sharedmaterial displays in editor instead of runtime
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}