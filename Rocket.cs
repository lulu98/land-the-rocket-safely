using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

struct StartPositionAndRotation
{
    public Vector3 startPosition;
    public Quaternion startRotation;
}

public class Rocket : MonoBehaviour {

    #region Declarations
    Rigidbody rigidbody;
    AudioSource thrustSound;

    public Camera camera;
    public Text gameText;
    public GameObject Coin;
    public GameObject Enemy;
    public GameObject missile;
    public static GameObject[] EnemyClones;
    
    [SerializeField] float rcsThrust = 50;
    [SerializeField] float mainThrust = 3;

    //private bool isBoost;
    //private float timeSinceBoost;
    private int points;
    private Vector3 cameraOffset;
    private StartPositionAndRotation startPosRot;
    private Vector3 coinStartPosition;
    private Vector3 enemyStartPosition;
    #endregion
    // Use this for initialization
    void Start () {
        EnemyClones = new GameObject[0];
        InitialVariableAssignment();
        Reset();
        thrustSound = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }
	
    void InitialVariableAssignment()
    {
        coinStartPosition = Coin.transform.position;
        enemyStartPosition = Enemy.transform.position;
        startPosRot.startPosition = transform.position;
        startPosRot.startRotation = transform.rotation;
        cameraOffset = camera.transform.position - transform.position;
    }
    // Update is called once per frame
    void Update () {
        Thrust();
        Rotate();
        UpdateUI();
        Shoot();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!thrustSound.isPlaying)
            {
                thrustSound.Play();
            }
            rigidbody.AddRelativeForce(mainThrust * Vector3.up);
        }
        if (Input.GetKeyUp(KeyCode.Space) && thrustSound.isPlaying)
        {
            thrustSound.Stop();
        }
    }

    void Rotate()
    {
        rigidbody.freezeRotation = true; // take manual control over rotation
        float rotationPerFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(rotationPerFrame * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(rotationPerFrame * -Vector3.forward);
        }
        rigidbody.freezeRotation = false; // let the physics engine make rotation
    }

    void UpdateUI()
    {
        gameText.text = "Coins: " + points;
    }

    void Shoot()
    {
        float LaunchOffset = 3.0f;
        if (Input.GetKeyDown(KeyCode.S))
        {
            float DegreeRadian = (2 * Mathf.PI * transform.rotation.z) / 360;
            //Instantiate(missile, transform.position + Quaternion.Euler(0, 0, transform.rotation.z) * Vector3.forward, transform.rotation);
            //new Vector3(Mathf.Sin(DegreeRadian),Mathf.Cos(DegreeRadian),0)
            Instantiate(missile,transform.position + LaunchOffset * transform.TransformDirection(Mathf.Sin(DegreeRadian), Mathf.Cos(DegreeRadian), 0), transform.rotation);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Coin":
                CoinHit(collision);
                break;
            case "Enemy":
                Reset();
                break;
            case "Finish":
                SceneManager.LoadScene("Win Screen");
                break;
            default:
                Reset();
                break;
        }
    }

    //TODO from time to time, when rocket hits coin, game crashes
    void CoinHit(Collision collision)
    {
        points++;
        Vector3 newPosition = transform.position;
        newPosition.y += Random.Range(-20, 20);
        while (newPosition.y <= 1)
        {
            newPosition.y += Random.Range(-20, 20);
        }
        newPosition.x += Random.Range(5, 20);
        collision.gameObject.transform.position = newPosition;
    }

    void Reset()
    {
        points = 0;
        Coin.transform.position = coinStartPosition;
        transform.position = startPosRot.startPosition;
        transform.rotation = startPosRot.startRotation;
        camera.transform.position = transform.position + cameraOffset;
        Enemy.transform.position = enemyStartPosition;
        for (int i = 0; i < EnemyClones.Length; i++)
        {
            Destroy(EnemyClones[i]);
        }
        EnemyClones = new GameObject[0];
    }
}

/*
 *  void LockPositionAndRotation()
    {
        Vector3 newPosition = transform.position;
        Quaternion newRotation = transform.rotation;
        newPosition.z = 0;
        newRotation.x = 0;
        newRotation.y = 0;
        transform.position = newPosition;
        transform.rotation = newRotation;
    }
 */

/*void Boost()
    {
        float boostTime = 3.0f;
        if (Input.GetKey(KeyCode.LeftShift) && !isBoost)
        {
            isBoost = true;
            timeSinceBoost = Time.time;
            mainThrust *= 2;
        }
        else if (isBoost && Time.time - timeSinceBoost >= boostTime)
        {
            isBoost = false;
            mainThrust /= 2;
        }
    }*/
