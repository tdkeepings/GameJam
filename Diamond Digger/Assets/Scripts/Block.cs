using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Block : MonoBehaviour
{
	public float health = 100;
	Animator animator;
	int healthHash;

	void Start()
	{
		animator = GetComponent<Animator>();
		healthHash = Animator.StringToHash("health");
	}

	public void Dig(float dmg)
	{
		health -= dmg;
		animator.SetFloat(healthHash, health);
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
