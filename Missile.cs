using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    [SerializeField] float mainThrust;
    [SerializeField] float maximalHitDistance;
    [SerializeField] float lifeTime;
    private float startTime;
    public GameObject Enemy;
	// Use this for initialization
	void Start () {
        mainThrust = 100.0f;
        maximalHitDistance = 5.0f;
        lifeTime = 5.0f;
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(mainThrust * Vector3.up * Time.deltaTime);
        LockPosition();
        CheckForDestroy();
	}

    void CheckForDestroy()
    {
        if (Time.time - startTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void LockPosition()
    {
        if (Mathf.Abs(transform.position.z) > 0.25f)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = 0;
            transform.position = newPosition;
        }
    }

    void ResetLocation()
    {
        Vector3 newLocation = transform.position;
        newLocation.z = 0;
        transform.position = newLocation;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Destroy(collision.gameObject);
        float distance = 5.0f;
        float DegreeRadian = (2 * Mathf.PI * collision.gameObject.transform.rotation.z) / 360;
        //Instantiate(collision.gameObject, collision.gameObject.transform.position + distance * Vector3.Cross(new Vector3(Mathf.Sin(DegreeRadian), Mathf.Cos(DegreeRadian), 0), new Vector3(0, 0, 1)), collision.gameObject.transform.rotation);
        GameObject Temp = Instantiate(collision.gameObject, collision.gameObject.transform.position + distance * Vector3.Cross(new Vector3(Mathf.Sin(DegreeRadian), Mathf.Cos(DegreeRadian), 0), new Vector3(0, 0, 1)), collision.gameObject.transform.rotation);
        GameObject[] tempArr = Rocket.EnemyClones;
        Rocket.EnemyClones = new GameObject[Rocket.EnemyClones.Length + 1];
        for (int i = 0; i < tempArr.Length; i++)
        {
            Rocket.EnemyClones[i] = tempArr[i];
        }
        Rocket.EnemyClones[Rocket.EnemyClones.Length - 1] = Temp;
        Destroy(gameObject);
    }

}
