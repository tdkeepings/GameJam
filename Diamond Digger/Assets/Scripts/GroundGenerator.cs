using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GroundGenerator : MonoBehaviour 
{
	public GameObject[] block;
	public int gridWidth = 10;
	public int gridHeight = 5;
	public int size = 1;
	public int randomSeed = 123;
    public GameObject TopLayer;

	public Array2DGameObjects[] generatedBlocks2D;

    public ScoreKeeper scoreKeeper;

    void Awake() {
        GenerateBlocks();
    }

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
        GameObject currentBlock;

		for (int vertical = 0; vertical < gridHeight; vertical++) 
		{
			for (int horizontal = 0; horizontal < gridWidth; horizontal++)
			{
                
                //Set top layer to GrassBlocks if they're included
                if (vertical + 1 == gridHeight && TopLayer != null) 
                    currentBlock = Instantiate(TopLayer) as GameObject;
                else
                    currentBlock = Instantiate(block[Random.Range(0, block.Length)]) as GameObject;
                
                generatedBlocks2D[horizontal][vertical] = currentBlock;
				currentBlock.name = horizontal + "," + vertical;
                currentBlock.GetComponent<Block>().destroyed.AddListener(scoreKeeper.UpdateScore);
				currentBlock.transform.position = new Vector3(horizontal * size, vertical * size, 0) + transform.position;
				currentBlock.transform.parent = transform;
			}
		}
	}

	[ContextMenu("Delete ground")]
	public void DeleteBlocks()
	{
		int childCount = transform.childCount;
		for(int i = 0; i < childCount; ++i)
		{
			DestroyImmediate(transform.GetChild(0).gameObject);
		}
	}

	public BlockNeighbours GetNeighbours(GameObject go)
	{
		string[] stringValues = go.name.Split(',');
		int x = int.Parse(stringValues[0]);
		int y = int.Parse(stringValues[1]);

		BlockNeighbours bn = new BlockNeighbours();

		if (x > 0)
			bn.left = generatedBlocks2D[x - 1][y];

		if (x < gridWidth - 1)
			bn.right = generatedBlocks2D[x + 1][y];

		if (y > 0)
			bn.bottom = generatedBlocks2D[x][y - 1];

		if (y < gridHeight -1)
			bn.top = generatedBlocks2D[x][y + 1];

		return bn;
	}
}
