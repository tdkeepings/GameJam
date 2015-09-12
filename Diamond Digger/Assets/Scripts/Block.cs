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
	public void Dig(float dmg)
	{
		// Rotate towards the player
		digEffect.transform.parent.LookAt(Player.transform);
		digEffect.Emit(30);

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
