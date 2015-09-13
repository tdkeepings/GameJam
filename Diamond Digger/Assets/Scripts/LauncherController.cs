using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

public class LauncherController : MonoBehaviour {

    public List<GameObject> players;
    public float angle;
    public float power;
	public Transform pivot;
	public ProjectileFollow cameraController;
    public Slider powerSlider;
    public Slider angleSlider;

    private SurfaceEffector2D launcherSE;
    private GameObject currentPlayer;

    private int powerMin = 10;
    private int powerMax = 100;
    private int angleMin = 10;
    private int angleMax = 80;

	// Use this for initialization
	void Start () {
	    launcherSE = GetComponent<SurfaceEffector2D>();
        powerSlider.minValue = powerMin;
        powerSlider.maxValue = powerMax;
        angleSlider.minValue = angleMin;
        angleSlider.maxValue = angleMax;
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
        if (power > powerMax)
            power = powerMax;
        else if (power < powerMin)
            power = powerMin;
        else 
            launcherSE.speed = power;
        
        
        //Angle
        if (angle > angleMax)
            angle = angleMax;
         else if (angle < angleMin)
            angle = angleMin;
         else 
            pivot.transform.rotation = Quaternion.Euler(0, 0, angle);

        powerSlider.value = power;
        angleSlider.value = angle;
    }
}
