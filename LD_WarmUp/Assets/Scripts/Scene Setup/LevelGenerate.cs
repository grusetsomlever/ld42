using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGenerate : MonoBehaviour {

    public int seed = 2000;
    public int resolution = 64;
    public int detail = 7;
    public int scale = 30;
    public int size = 24;
    public Vector3 offset = new Vector3(0, 0, 0);
    public GameObject levelBlock;

    protected PerlinNoise levelSeedGenerate;

    // Use this for initialization
    void Start () {
        Debug.Log("NOW CREATING");
        // round position properly
        Vector3 myPosition = this.transform.position;
        myPosition.x = Mathf.Round(myPosition.x);
        myPosition.y = Mathf.Round(myPosition.y);
        myPosition.z = Mathf.Round(myPosition.z);
        this.transform.position = myPosition;

        // generate level
        Generate();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Generate() {
        Debug.Log(this.transform.position);
        levelSeedGenerate = new PerlinNoise(seed, resolution, detail);
        int i = 0;
        int j = 0;
        int k = 0;

        double cI;
        double cJ;
        double cK;

        int sDiv = size / 2;
        float sDivF = size / 2; // force float division instead of integer
        Vector3 myPosition = this.transform.position;

        // Generate the level in a sphere around the location of the level generator
        for (i = -sDiv; i < sDiv; i++) {
            cI = (i / sDivF);
            for (j = -sDiv; j < sDiv; j++) {
                cJ = (j / sDivF);
                double a = (cI * cI + cJ * cJ);
                for (k = -sDiv; k < sDiv; k++) {
                    cK = (k / sDivF);
                    if (a <= 1) {
                        double b = (a * a + cK * cK);
                        if (b <= 1) {
                            Vector3 point = new Vector3(i + myPosition.x, j + myPosition.y, k + myPosition.z);
                            float levelSeed = levelSeedGenerate.PerlinNoiseGenerate(point);

                            if (levelSeed > 0.5) {
                                var newBlock = GameObject.Instantiate(levelBlock);
                                newBlock.transform.position = new Vector3(i + myPosition.x, j + myPosition.y, k + myPosition.z);
                            }
                        }
                    }
                }
            }
        }
    }
}