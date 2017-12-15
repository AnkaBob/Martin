using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour {

    public Text score;
	// Use this for initialization
	void Start () {
        score.text = "Score : " + PlayerPrefs.GetInt("Score");
        SoundEffectsHelper.Instance.MakeDeathSound();
    }
	
	// Update is called once per frame
	void Update () {
         if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
