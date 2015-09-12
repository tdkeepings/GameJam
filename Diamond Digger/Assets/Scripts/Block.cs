using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Block : MonoBehaviour
{
	public float health = 100;
	public ParticleSystem digEffect;
	BlockNeighbours neighbours;

	public enum BlockState
	{
		Hidden,       // The player can not see what the block contains
		Visible,      // The blocks contents is visible
		Destroyed     // The block has been destroyed.
	}
	public BlockState state = BlockState.Hidden;

	// States
	public GameObject hiddenState;
	public GameObject aliveState;
	public GameObject deadState;

	// Edges
	public GameObject top;
	public GameObject bottom;
	public GameObject left;
	public GameObject right;

	GameObject Player
	{
		get
		{
			if (_player == null)
				_player = GameObject.FindGameObjectWithTag("Player");
			return _player; 
		}
		set { _player = value; }
	}
	GameObject _player;

	void Start()
	{
		SetState(state);
		neighbours = GetComponentInParent<GroundGenerator>().GetNeighbours(gameObject);
		UpdateEdges();
	}
	
	[ContextMenu("Dig")]
	public void TestDig()
	{
		Dig(10);
	}

	public void Dig(float dmg)
	{
		// Rotate towards the player
		Vector2 deltas = transform.position - Player.transform.position;
		float angle = -Mathf.Rad2Deg * Mathf.Atan(deltas.y / deltas.x);
		digEffect.transform.parent.eulerAngles = new Vector3(0, 0, angle);

		// Emit particles
		digEffect.Emit(30);

		// Calculate new health
		health -= dmg;
		if(health <= 0)
			DestroyBlock();
	}

	void DestroyBlock()
	{
		SetState(BlockState.Destroyed);
	}

	public void SetState(BlockState bs)
	{
		aliveState.SetActive(false);
		hiddenState.SetActive(false);
		deadState.SetActive(false);

		state = bs;
		switch (bs)
		{
				case BlockState.Destroyed:
				deadState.SetActive(true);
				break;

				case BlockState.Hidden:
				hiddenState.SetActive(true);
				break;

				case BlockState.Visible:
				aliveState.SetActive(true);
				break;
		}
	}

	public void UpdateEdges()
	{
		if (top != null)
			top.SetActive(neighbours.top == null || (neighbours.top && neighbours.top.GetComponent<Block>().state == BlockState.Destroyed));

		if (bottom != null)
			bottom.SetActive(neighbours.bottom == null || (neighbours.bottom && neighbours.bottom.GetComponent<Block>().state == BlockState.Destroyed));

		if (right != null)
			right.SetActive(neighbours.right == null || (neighbours.right && neighbours.right.GetComponent<Block>().state == BlockState.Destroyed));

		if (left != null)
			left.SetActive(neighbours.left == null || (neighbours.left && neighbours.left.GetComponent<Block>().state == BlockState.Destroyed));
	}
}
