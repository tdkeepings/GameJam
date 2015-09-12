using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Block : MonoBehaviour
{
	public float health = 100;
	public ParticleSystem digEffect;

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
		// TOOD: particle/animation of block crumbling

		// Inform our neighbours
		var controller = GetComponentInParent<GroundGenerator>();
		
	}
}
