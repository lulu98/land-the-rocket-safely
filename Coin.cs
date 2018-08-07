using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    Vector3 lastPosition;
    // Use this for initialization
	void Start () {
        lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(45 * Vector3.up * Time.deltaTime);
        if (transform.position != lastPosition)
        {
            PlaySound();
            lastPosition = transform.position;
        }
	}

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
