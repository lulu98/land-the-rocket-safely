using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject mainPlayer;
    [SerializeField] float upwardThrust;
    [SerializeField] float lateralThrust;
    // Use this for initialization
	void Start () {
        upwardThrust = 5.0f;
        lateralThrust = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
        PointToPlayer();
        if (transform.position.z != 0)
        {
            LockPosition();
        }
        MoveTowardsPlayer();
	}

    void PointToPlayer()
    {
        transform.LookAt(mainPlayer.transform);
    }

    void LockPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = 0;
        transform.position = newPosition;
    }

    void MoveTowardsPlayer()
    {
        float distanceRange = 0.5f;
        float xDifference = transform.position.x - mainPlayer.transform.position.x;
        float yDifference = transform.position.y - mainPlayer.transform.position.y;
        if (xDifference > distanceRange)
        {
            transform.position += lateralThrust * Vector3.left * Time.deltaTime;
        }
        else if (xDifference < -distanceRange)
        {
            transform.position += lateralThrust * Vector3.right * Time.deltaTime;
        }

        if (yDifference > distanceRange)
        {
            transform.position += upwardThrust * Vector3.down * Time.deltaTime;
        }
        else if(yDifference < -distanceRange)
        {
            transform.position += upwardThrust * Vector3.up * Time.deltaTime;
        }
    }
}
