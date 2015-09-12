using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class LauncherController : MonoBehaviour {

    public List<GameObject> players;
    public float angle;
    public float power;
	public Transform pivot;
	public ProjectileFollow cameraController;

    private SurfaceEffector2D launcherSE;
    private GameObject currentPlayer;

	// Use this for initialization
	void Start () {
	    launcherSE = GetComponent<SurfaceEffector2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown("space")) {
            LoadLauncher();
            FireCurrentPlayer();
        }

        angle += Input.GetAxis("Horizontal");
        power += Input.GetAxis("Vertical");

        UpdateLauncher();
	}

    private void LoadLauncher() {
        if (players.Count > 0) {
            currentPlayer = null;
            currentPlayer = PrefabUtility.InstantiatePrefab(players[0]) as GameObject;
	        cameraController.projectile = currentPlayer.transform;
            currentPlayer.transform.position = new Vector3(-1000f, -1000f, -1000f);
            players.RemoveAt(0);
        } else { 

        }
    }

    private void FireCurrentPlayer() {
        Vector3 temp = this.transform.position;
        temp.y += 2f;
        currentPlayer.transform.position = temp;
        
    }

    private void UpdateLauncher(){
        //Power
        if (power > 100)
            power = 100;
        else if (power < 10)
            power = 10;
        else {
            launcherSE.speed = power;
        }
        
        //Angle
        if (angle > 45) 
            angle = 45f;
         else if (angle < 10) 
            angle = 10f;
         else 
            pivot.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
