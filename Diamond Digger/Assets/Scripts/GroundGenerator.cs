using UnityEngine;
using System.Collections.Generic;

public class GroundGenerator : MonoBehaviour 
{
	public GameObject[] block;
	public List<GameObject> generatedBlocks;
	public int gridWidth = 10;
	public int gridHeight = 5;
	public int size = 1;
	public int randomSeed = 123;

	[ContextMenu("Generate ground")]
	public void GenerateBlocks () 
	{
		if (generatedBlocks.Count > 0) {
			DeleteBlocks();
		}

		Random.seed = randomSeed;
		for (int vertical = 0; vertical < gridHeight; vertical++) 
		{
			for (int horizontal = 0; horizontal < gridWidth; horizontal++)
			{
				GameObject currentBlock = Instantiate(block[Random.Range(0, block.Length)]);
				generatedBlocks.Add(currentBlock);
				currentBlock.name = horizontal + "," + vertical;
				currentBlock.transform.position = new Vector3(horizontal * size, vertical * size, 0) + transform.position;
				currentBlock.transform.parent = transform;
			}
		}
	}

	[ContextMenu("Delete ground")]
	public void DeleteBlocks() 
	{
		foreach (GameObject block in generatedBlocks) 
		{
			DestroyImmediate(block);
		}

		generatedBlocks.Clear();
	}
}
