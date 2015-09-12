using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private bool playerIsLocked = true;
    private Rigidbody2D player;
    private float initialGravity;
	void Start () {
        player = GetComponent<Rigidbody2D>();
        initialGravity = player.gravityScale;
        player.gravityScale = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown("space")) {
            playerIsLocked = false;
        }

        if (!playerIsLocked) {
            player.gravityScale = initialGravity;
        }    
	}

}
