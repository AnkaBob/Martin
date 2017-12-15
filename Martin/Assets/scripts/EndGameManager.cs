using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float score = PlayerPrefs.GetFloat("Score");
        print(score);
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
