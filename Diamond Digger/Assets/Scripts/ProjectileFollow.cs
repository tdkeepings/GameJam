using UnityEngine;
using System.Collections;

//	Attach this script to the Main Camera.
//	This script will set the transform values for the GameObject it is attached to.
public class ProjectileFollow : MonoBehaviour {

	public Transform projectile;        // The transform of the projectile to follow.
	public Transform farLeft;           // The transform representing the left bound of the camera's position.
	public Transform farRight;          // The transform representing the right bound of the camera's position.
	Vector3 defaultCamPos;
	public float camSpeed = 5;

	void Start()
	{
		defaultCamPos = transform.position;
	}

	void Update ()
	{
		Vector3 target = projectile == null
			? defaultCamPos
			: projectile.transform.position;
		
		// Clamp the x value of the stored position between the left and right bounds.
		target.x = Mathf.Clamp (target.x, farLeft.position.x, farRight.position.x);
		target.z = transform.position.z; // Dont change z

		transform.position = iTween.Vector3Update(transform.position, target, camSpeed);
	}
}
