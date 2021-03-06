﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float yDefaultPosition;
    public GameObject player;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        yDefaultPosition = player.transform.position.y;
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (player.transform.position.y > yDefaultPosition)
        {
            transform.position = player.transform.position + offset;
        }
	}
}
