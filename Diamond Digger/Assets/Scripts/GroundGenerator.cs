using UnityEngine;
using System.Collections.Generic;

public class GroundGenerator : MonoBehaviour 
{
    public GameObject[] block;
    public List<GameObject> generatedBlocks;
    public int gridWidth = 10;
    public int gridHeight = 5;
    public int size = 1;

    [ContextMenu("Generate ground")]
    public void GenerateBlocks () 
    {
        for (int rows = 0; rows < gridHeight; rows++) 
        {
            for (int columns = 0; columns < gridWidth; columns++)
            {
                GameObject currentBlock = Instantiate(block[Random.Range(0, block.Length)]);
                generatedBlocks.Add(currentBlock);
                currentBlock.transform.position = new Vector3(columns * size, rows * size, 0) + transform.position;
            }
        }
    }

}
