using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainAppearance : MonoBehaviour {
    public Terrain LevelTerrain;
    public Material LevelMaterial;
	// Use this for initialization
	void Start () {
        LevelTerrain.materialTemplate = LevelMaterial;
	}
	
	
}
