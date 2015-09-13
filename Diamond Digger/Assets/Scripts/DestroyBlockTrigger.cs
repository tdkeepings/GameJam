using UnityEngine;
using System.Collections;

public class DestroyBlockTrigger : MonoBehaviour {

    public AudioSource destroyClip;

    void OnTriggerEnter2D(Collider2D col) {
        Destroy(col.gameObject);

        if (destroyClip != null)
            destroyClip.Play();
    }
}
