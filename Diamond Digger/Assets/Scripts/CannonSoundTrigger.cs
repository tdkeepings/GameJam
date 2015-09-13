using UnityEngine;
using System.Collections;

public class CannonSoundTrigger : MonoBehaviour {

    public AudioSource clip;

    private bool hasPlayed = false;

    void Update() {
        if (Input.GetKeyDown("space") && !hasPlayed && clip != null) {
            clip.Play();
        }
    }
}
