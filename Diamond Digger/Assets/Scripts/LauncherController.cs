using UnityEngine;
using System.Collections;

public class LauncherController : MonoBehaviour {

    public float Angle;
    public float Power;

    private SurfaceEffector2D LauncherSE;

	// Use this for initialization
	void Start () {
	    LauncherSE = GetComponent<SurfaceEffector2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float angleInput = Input.GetAxis("Horizontal");
        Angle += angleInput;

        float powerInput = Input.GetAxis("Vertical");
        Power += powerInput;

        UpdateLauncher();
	}

    private void UpdateLauncher(){
        //Power
        if (Power > 100)
            Power = 100;
        else if (Power < 10)
            Power = 10;
        else {
            LauncherSE.speed = Power;
        }


        //Angle
        if (Angle > 45) {
            Angle = 45f;
        } else if (Angle < 10) {
            Angle = 10f;
        } else {
            this.transform.rotation = Quaternion.Euler(0, 0, Angle);
        }
    }

}
