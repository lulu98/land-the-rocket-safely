using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel01()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
