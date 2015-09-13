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
    public Text livesText;

    private SurfaceEffector2D launcherSE;
    private GameObject currentPlayer;
    private bool isFlying = false;

	// Use this for initialization
	void Start () {
	    launcherSE = GetComponent<SurfaceEffector2D>();
        UpdateLivesText();
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
        if (currentPlayer == null) {
            isFlying = false;
        }
        if (players.Count > 0 && !isFlying) {
            currentPlayer = null;
            currentPlayer = PrefabUtility.InstantiatePrefab(players[0]) as GameObject;
	        cameraController.projectile = currentPlayer.transform;
            
            players.RemoveAt(0);
        } else { 

        }
    }

    private void FireCurrentPlayer() {
        if (!isFlying && players.Count > 0) {
            isFlying = true;

            UpdateLivesText();
            
            //Set player on the launcher
            Vector3 positionTemp = this.transform.position;
            positionTemp.y += 1f;
            positionTemp.z = 0f;

            currentPlayer.transform.position = positionTemp;
            currentPlayer.transform.rotation = this.transform.rotation;
        }
    }

    private void UpdateLauncher(){
        //Power
        if (power > powerSlider.maxValue)
            power = powerSlider.maxValue;
        else if (power < powerSlider.minValue)
            power = powerSlider.minValue;
        else 
            launcherSE.speed = power;
        
        
        //Angle
        if (angle > angleSlider.maxValue)
            angle = angleSlider.maxValue;
         else if (angle < angleSlider.minValue)
            angle = angleSlider.minValue;
         else 
            pivot.transform.rotation = Quaternion.Euler(0, 0, angle);

        powerSlider.value = power;
        angleSlider.value = angle;
    }

    private void UpdateLivesText() {
        livesText.text = "Lives: " + players.Count;
    }
}
