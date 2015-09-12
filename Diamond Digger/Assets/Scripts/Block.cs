using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.Events;

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

	public UnityEvent destroyed;

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

	public void Dig(float dmg)
	{
		// Calculate new health
		health -= dmg;
		if(health <= 0)
			DestroyBlock();
	}

	void DestroyBlock()
	{
		SetState(BlockState.Destroyed);
		UpdateEdges();
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
				GetComponent<Collider2D>().enabled = false;
				if(neighbours.top != null)
					neighbours.top.GetComponent<Block>().OnNeighbourDestroyed();
				if(neighbours.bottom != null)
					neighbours.bottom.GetComponent<Block>().OnNeighbourDestroyed();
				if(neighbours.left != null)
					neighbours.left.GetComponent<Block>().OnNeighbourDestroyed();
				if(neighbours.right != null)
					neighbours.right.GetComponent<Block>().OnNeighbourDestroyed();
				destroyed.Invoke();
				break;

				case BlockState.Hidden:
				hiddenState.SetActive(true);
				break;

				case BlockState.Visible:
				aliveState.SetActive(true);
				break;
		}
	}

	public void OnNeighbourDestroyed()
	{
		if (state != BlockState.Destroyed) 
			SetState(BlockState.Visible);
		UpdateEdges();
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		Dig(50 * collision.relativeVelocity.magnitude);

		Vector3 dir = (Vector2)digEffect.transform.parent.position - collision.contacts[0].point; 
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		digEffect.transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
		// Emit particles
		digEffect.Emit(30);
	}

	public void UpdateEdges()
	{
		if (state == BlockState.Destroyed)
		{
			if(top != null)
				top.SetActive(false);
			if(bottom != null)
				bottom.SetActive(false);
			if(left != null)
				left.SetActive(false);
			if(right != null)
				right.SetActive(false);
		}
		else
		{
			if (top != null)
			{
				top.SetActive(neighbours.top == null || (neighbours.top && neighbours.top.GetComponent<Block>().state == BlockState.Destroyed));
				if(state == BlockState.Hidden && top.activeSelf)
					SetState(BlockState.Visible);
			}

			if (bottom != null)
			{
				bottom.SetActive(neighbours.bottom && neighbours.bottom.GetComponent<Block>().state == BlockState.Destroyed);
				if(state == BlockState.Hidden && bottom.activeSelf)
					SetState(BlockState.Visible);
			}

			if (right != null)
			{
				right.SetActive(neighbours.right && neighbours.right.GetComponent<Block>().state == BlockState.Destroyed);
				if(state == BlockState.Hidden && right.activeSelf)
					SetState(BlockState.Visible);
			}

			if (left != null)
			{
				left.SetActive(neighbours.left && neighbours.left.GetComponent<Block>().state == BlockState.Destroyed);
				if(state == BlockState.Hidden && left.activeSelf)
					SetState(BlockState.Visible);
			}
		}
	}
}
