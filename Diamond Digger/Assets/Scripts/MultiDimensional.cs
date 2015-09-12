using UnityEngine;
using System.Collections;

	// Unity inspector can not serialise a standard 2d array so we do this little 'hack'
[System.Serializable]
public class MultiDimensional<T>
{
	[HideInInspector]
	public string index;

	public T[] innerList;
	public T this[int i]
	{
		get { return innerList[i]; }
		set { innerList[i] = value; }
	}
}

[System.Serializable]
public class Array2DGameObjects : MultiDimensional<GameObject>
{
	public Array2DGameObjects(int xIndex, int size)
	{
		index = xIndex.ToString();
		innerList = new GameObject[size];
	}
}