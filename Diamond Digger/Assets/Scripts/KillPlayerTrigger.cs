using UnityEngine;
using System.Collections;

public class KillPlayerTrigger : MonoBehaviour
{
	public AudioSource playerDeathClip;

	void OnTriggerEnter2D(Collider2D col)
	{
		Destroy(col.gameObject);

		if (playerDeathClip != null)
			playerDeathClip.Play();
	}
}
