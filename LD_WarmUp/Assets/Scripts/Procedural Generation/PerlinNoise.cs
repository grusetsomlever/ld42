using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class PerlinNoise {
	public int seed;				//base seed to start off of
	public int resolution;			//pixel resolution of picture
	public int detail;				//amount of octaves to generate
	public int multiplier;			//generate over a larger distance
	
	public PerlinNoise(int s, int r, int d, int m = 1, bool generatePics = false) {
		seed = s;
		resolution = r;
		detail = d;
		multiplier = m;
	}

	// Perlin Noise Function
	public float PerlinNoiseGenerate(float point1, float point2) {	
		//setup variables (more boring stuff)
		float valReturn = 0;
		
		float interpolate1;
		float interpolate2;
		float interpolated;
		
		int res = resolution;
		
		RandomXORShift rand1 = new RandomXORShift();
		RandomXORShift rand2 = new RandomXORShift();
		RandomXORShift rand3 = new RandomXORShift();
		RandomXORShift rand4 = new RandomXORShift();
		
		// Loop to get the data from each octave
		for (int ctr = 1; ctr < detail; ctr++) {	
			// make boxes smaller each octave
			res = res / 2;
			
			rand1.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt (point1 / res) * -7) + (Mathf.FloorToInt (point2 / res) * 3));
			rand2.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt ((point1 + res) / res) * -7) + (Mathf.FloorToInt (point2 / res) * 3));
			rand3.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt (point1 / res) * -7) + (Mathf.FloorToInt ((point2 + res) / res) * 3));
			rand4.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt ((point1 + res) / res) * -7) + (Mathf.FloorToInt ((point2 + res) / res) * 3));
			
			// interpolation
			interpolate1 = RandomXORShift.interpolateCosine(rand1.range (0, 2), rand2.range (0, 2), point1 - (Mathf.FloorToInt(point1 / res) * res), res);
			interpolate2 = RandomXORShift.interpolateCosine(rand3.range (0, 2), rand4.range (0, 2), point1 - (Mathf.FloorToInt(point1 / res) * res), res);
			interpolated = RandomXORShift.interpolateCosine(interpolate1, interpolate2, point2 - (Mathf.FloorToInt(point2 / res) * res), res);
			
			// add octave value to point; each octave has half as much influence as the one before it
			valReturn += interpolated * Mathf.Pow(2, -ctr);
		}
		
		return valReturn;
	}
	
	public float PerlinNoiseGenerate(Vector3 points) {
		float point1 = points.x;
		float point2 = points.y;
		float point3 = points.z;
		
		//setup variables (more boring stuff)
		float valReturn = 0;
		
		float interpolate1;
		float interpolate2;
		
		float interpolate3;
		float interpolate4;
		float interpolate5;
		float interpolate6;
		
		float interpolated;
		
		int res = resolution;
		
		RandomXORShift rand1 = new RandomXORShift();
		RandomXORShift rand2 = new RandomXORShift();
		RandomXORShift rand3 = new RandomXORShift();
		RandomXORShift rand4 = new RandomXORShift();
		RandomXORShift rand5 = new RandomXORShift();
		RandomXORShift rand6 = new RandomXORShift();
		RandomXORShift rand7 = new RandomXORShift();
		RandomXORShift rand8 = new RandomXORShift();
		
		// Loop to get the data from each octave
		for (int ctr = 1; ctr < detail; ctr++)
		{	
			// make boxes smaller each octave
			res = res / 2;
			
			rand1.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt (point1 / res) * -7) + (Mathf.FloorToInt (point2 / res) * 3) + (Mathf.FloorToInt (point3 / res) * 5));
			rand2.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt ((point1 + res) / res) * -7) + (Mathf.FloorToInt (point2 / res) * 3) + (Mathf.FloorToInt (point3 / res) * 5));
			rand3.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt (point1 / res) * -7) + (Mathf.FloorToInt ((point2 + res) / res) * 3) + (Mathf.FloorToInt (point3 / res) * 5));
			rand4.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt ((point1 + res) / res) * -7) + (Mathf.FloorToInt ((point2 + res) / res) * 3) + (Mathf.FloorToInt (point3 / res) * 5));
			rand5.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt (point1 / res) * -7) + (Mathf.FloorToInt (point2 / res) * 3) + (Mathf.FloorToInt ((point3 + res) / res) * 5));
			rand6.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt ((point1 + res) / res) * -7) + (Mathf.FloorToInt (point2 / res) * 3) + (Mathf.FloorToInt ((point3 + res) / res) * 5));
			rand7.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt (point1 / res) * -7) + (Mathf.FloorToInt ((point2 + res) / res) * 3) + (Mathf.FloorToInt ((point3 + res) / res) * 5));
			rand8.seed = seed + Mathf.RoundToInt((Mathf.FloorToInt ((point1 + res) / res) * -7) + (Mathf.FloorToInt ((point2 + res) / res) * 3) + (Mathf.FloorToInt ((point3 + res) / res) * 5));
			
			// interpolation
			interpolate3 = RandomXORShift.interpolateCosine(rand1.range (0, 2), rand2.range (0, 2), point1 - (Mathf.FloorToInt(point1 / res) * res), res);
			interpolate4 = RandomXORShift.interpolateCosine(rand3.range (0, 2), rand4.range (0, 2), point1 - (Mathf.FloorToInt(point1 / res) * res), res);
			interpolate5 = RandomXORShift.interpolateCosine(rand5.range (0, 2), rand6.range (0, 2), point1 - (Mathf.FloorToInt(point1 / res) * res), res);
			interpolate6 = RandomXORShift.interpolateCosine(rand7.range (0, 2), rand8.range (0, 2), point1 - (Mathf.FloorToInt(point1 / res) * res), res);
			
			interpolate1 = RandomXORShift.interpolateCosine(interpolate3, interpolate4, point1 - (Mathf.FloorToInt(point1 / res) * res), res);
			interpolate2 = RandomXORShift.interpolateCosine(interpolate5, interpolate6, point1 - (Mathf.FloorToInt(point1 / res) * res), res);
			interpolated = RandomXORShift.interpolateCosine(interpolate1, interpolate2, point2 - (Mathf.FloorToInt(point2 / res) * res), res);
			
			// add octave value to point; each octave has half as much influence as the one before it
			valReturn += interpolated * Mathf.Pow(2, -ctr);
		}
		
		return valReturn;
	}
}
