using UnityEngine;
using System.Collections;
using System;

public class RandomXORShift 
{
	// ********** GENERATES RANDOM NUMBERS USING THE XORSHIFT ALGORITHM **********
	public int seed = 0;
	
	// Converts the result of the XORshift algorithm to a value held within a specified tange
	public int range(int min, int max, int period = 1023)
	{
		double num = number();
		double divided = Math.Floor (num / period);		
		double baseNum = (num - (divided * period));
		
		int difference = max - min;
		divided = Math.Floor (baseNum / difference);
		double toReturn = (baseNum - (divided * max)) + min;
		
		return (int)toReturn;
	}
	
	// XORshift algorithm
	public int number()
	{
		int x = seed;
		int y = 362436069;
		int z = 521288629;
		int w = 88675123;
		
		x ^= x << 16;
		x ^= x >> 5;
		x ^= x << 1;
		
		int t = x ^ (x << 11);
 		x = y; 
		y = z; 
		z = w;
  		return w = w ^ (w >> 19) ^ (t ^ (t >> 8));
	}
	
	// Interpolates between two points (note: interpolation is linear... lol haven't got the cosine function to work here yet :P)
	public static float interpolateCosine(float height1, float height2, float distPoints, float distCoords)
	{
		float heightReturn;
		
		float heightDif = Mathf.Abs(height1 - height2);
		float distance = distPoints / distCoords;
		
		heightReturn = (float)distance * heightDif;
		
		if (height1 > height2)
		{
			heightReturn = height1 - heightReturn;
		}
		else
		{
			heightReturn = height1 + heightReturn;
		}
		
		return heightReturn;
	}
	
	
	
	
	// Functions used in various Qbots terrain generation code... I need to trim this function up a little at some point.
	public int randomFromLocation(int point1, int point2, int distance, int max, int zeros = 1)
	{
		return (Mathf.RoundToInt(randomFromLocationFloat(point1, point2, distance, max, zeros)));
	}
	
	public float randomFromLocationFloat(int point1, int point2, int distance, int max, int zeros = 1)
	{
		int originalSeed = seed;
		float height = 0;
		
		int sourceX = Mathf.RoundToInt (point1/distance);
		int sourceY = Mathf.RoundToInt (point2/distance);
		
		int sourceX2 = sourceX;
		int sourceY2 = sourceY;
		int sourceX3 = sourceX;
		int sourceY3 = sourceY;
		int sourceX4 = sourceX;
		int sourceY4 = sourceY;
		
		int height1;
		int height2;
		int height3;
		int height4;
		
		//find coordinates to base calcs off of
		if (sourceX * distance > point1)
		{
			sourceX2 = sourceX - 1;
			sourceX3 = sourceX - 1;
		}
		else if (sourceX * distance < point1)
		{
			sourceX2 = sourceX + 1;
			sourceX3 = sourceX + 1;
		}
		else
		{
			sourceX2 = sourceX;
			sourceX3 = sourceX;
		}
		
		if (sourceY * distance > point2)
		{
			sourceY4 = sourceY - 1;
			sourceY3 = sourceY - 1;
		}
		else if (sourceY * distance < point2)
		{
			sourceY4 = sourceY + 1;
			sourceY3 = sourceY + 1;
		}
		else
		{
			sourceY4 = sourceY;
			sourceY3 = sourceY;
		}
		
		//get points
		/*Random.seed = seed + sourceX + (100*sourceY);
		height1 = Random.Range (0, maxHeight * 4);*/
		seed = originalSeed + sourceX + (100*sourceY);
		height1 = range (0, max * zeros);
		height1 = height1 - (max * (zeros - 1));
		if (height1 < 0)
		{
			height1 = 0;
		}
		
		seed = originalSeed + sourceX2 + (100*sourceY2);
		height2 = range (0, max * zeros);
		height2 = height2 - (max * (zeros - 1));
		if (height2 < 0)
		{
			height2 = 0;
		}
		
		seed = originalSeed + sourceX3 + (100*sourceY3);
		height3 = range (0, max * zeros);
		height3 = height3 - (max * (zeros - 1));
		if (height3 < 0)
		{
			height3 = 0;
		}
		
		seed = originalSeed + sourceX4 + (100*sourceY4);
		height4 = range (0, max * zeros);
		height4 = height4 - (max * (zeros - 1));
		if (height4 < 0)
		{
			height4 = 0;
		}
		
		//Debug.Log (sourceX + " " + sourceY + " " + sourceX2 + " " + sourceY2 + " " + 
		//			sourceX3 + " " + sourceY3 + " " + sourceX4 + " " + sourceY4 + " ");
		
		//interpolate
		float inrpol1 = interpolateCosine(height1, height2, Mathf.Abs(point1 - (sourceX * distance)), distance);
		float inrpol2 = interpolateCosine(height4, height3, Mathf.Abs(point1 - (sourceX * distance)), distance);
		height = interpolateCosine(inrpol1, inrpol2, Mathf.Abs(point2 - (sourceY * distance)), distance);
		
		seed = originalSeed;
		
		return height;
	}
}
