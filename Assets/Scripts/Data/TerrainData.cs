using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TerrainData : UpdatableData {

    public float uniformScale = 2.5f;

    public bool useFlatShading;
    public bool useFalloff;

    public float meshHeightMultiplier;          // Scales on y axis
    public AnimationCurve meshHeightCurve;
}
