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
    public int maxBlocks = 20;
    public Vector3 offset = new Vector3(0, 0, 0);
    public GameObject levelBlock;
    public GameObject powerup;

    protected PerlinNoise levelSeedGenerate;
    protected RandomXORShift powerupGenerate;

    // Use this for initialization
    void Start() {
        // round position properly
        Vector3 myPosition = this.transform.position;
        myPosition.x = Mathf.Round(myPosition.x);
        myPosition.y = Mathf.Round(myPosition.y);
        myPosition.z = Mathf.Round(myPosition.z);
        this.transform.position = myPosition;

        powerupGenerate = new RandomXORShift();
        powerupGenerate.seed = seed;

        // generate level
        Generate();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Generate() {
        levelSeedGenerate = new PerlinNoise(seed, resolution, detail);
        int i = 0;
        int j = 0;
        int k = 0;
        int sDiv = size / 2;
        float sDivF = size / 2; // force float division instead of integer
        Vector3 myPosition = this.transform.position;

        // Generate the level in a sphere around the location of the level generator
        int blocksOut = 0;
        int blocksCreated = 0;
        while (blocksOut < sDiv || blocksCreated < maxBlocks) {
            for (i = -blocksOut; i < blocksOut; i++) {
                for (j = -blocksOut; j < blocksOut; j++) {
                    for (k = -blocksOut; k < blocksOut; k++) {
                        if (Math.Abs(i) == blocksOut || Math.Abs(j) == blocksOut || Math.Abs(k) == blocksOut) {
                            blocksCreated += CreateLevel(myPosition, new Vector3(i, j, k));
                        }
                    }
                }
            }

            blocksOut++;
        }
    }

    public int CreateLevel(Vector3 myPosition, Vector3 blockPosition) {
        float i = blockPosition.x;
        float j = blockPosition.y;
        float k = blockPosition.z;
        Vector3 point = new Vector3(i + myPosition.x, j + myPosition.y, k + myPosition.z);
        float levelSeed = levelSeedGenerate.PerlinNoiseGenerate(point);

        if ((levelSeed > 0.55 && levelSeed < .65) || levelSeed < 0.2 || levelSeed > 0.9) {
            var newBlock = GameObject.Instantiate(levelBlock);
            newBlock.transform.position = new Vector3(i + myPosition.x, j + myPosition.y, k + myPosition.z);
            return 1;
        }
        // make horizontal platforms a bit bigger
        else if (levelSeed > 0.45 && levelSeed < .7) {
            Vector3[] points = new Vector3[] {
                new Vector3(i + myPosition.x + 1, j + myPosition.y, k + myPosition.z),
                new Vector3(i + myPosition.x - 1, j + myPosition.y, k + myPosition.z),
                new Vector3(i + myPosition.x, j + myPosition.y, k + myPosition.z + 1),
                new Vector3(i + myPosition.x, j + myPosition.y, k + myPosition.z - 1)
            };

            for (int ctr = 0; ctr < points.Length; ctr++) {
                float levelSeedNextTo = levelSeedGenerate.PerlinNoiseGenerate(points[ctr]);
                if (levelSeedNextTo > 0.5 && levelSeed < 0.6) {
                    GameObject newBlock = GameObject.Instantiate(levelBlock);
                    newBlock.transform.position = new Vector3(i + myPosition.x, j + myPosition.y, k + myPosition.z);
                    return 1;
                }
            }
        }
        //generate powerup
        else {
            point = new Vector3(point.x, point.y - 2, point.z);
            levelSeed = levelSeedGenerate.PerlinNoiseGenerate(point);

            if (levelSeed > 0.55 && levelSeed < .65) {
                powerupGenerate.seed = (int)(blockPosition.x + myPosition.x +
                    10 * (blockPosition.y + blockPosition.y) +
                    100 * (blockPosition.z + blockPosition.z) +
                    seed);

                if (powerupGenerate.range(0, 50) == 10) {
                    GameObject newPowerup = GameObject.Instantiate(powerup);
                    point = new Vector3(point.x, point.y + 2, point.z);
                    newPowerup.transform.position = point;
                }
                //Debug.Log(a);
            }
        }

        return 0;
    }
}