using UnityEngine;
using System.Collections.Generic;

public class GroundGenerator : MonoBehaviour 
{
	public GameObject[] block;
	public int gridWidth = 10;
	public int gridHeight = 5;
	public int size = 1;
	public int randomSeed = 123;
	
	public Array2DGameObjects[] generatedBlocks2D;

	[ContextMenu("Generate ground")]
	public void GenerateBlocks () 
	{
		if (transform.childCount > 0)
		{
			DeleteBlocks();
		}

		// Create our 2d array
		generatedBlocks2D = new Array2DGameObjects[gridWidth];
		for (int i = 0; i < gridWidth; ++i)
		{
			generatedBlocks2D[i] = new Array2DGameObjects(i, gridHeight);
		}

		Random.seed = randomSeed;
		for (int vertical = 0; vertical < gridHeight; vertical++) 
		{
			for (int horizontal = 0; horizontal < gridWidth; horizontal++)
			{
				GameObject currentBlock = Instantiate(block[Random.Range(0, block.Length)]);
				generatedBlocks2D[horizontal][vertical] = currentBlock;
				currentBlock.name = "(" + horizontal + "," + vertical + ")";
				currentBlock.transform.position = new Vector3(horizontal * size, vertical * size, 0) + transform.position;
				currentBlock.transform.parent = transform;
			}
		}
	}

	[ContextMenu("Delete ground")]
	public void DeleteBlocks() 
	{
		foreach(Transform child in transform)
		{
			DestroyImmediate(child.gameObject);
		}
	}
}
