﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreGame : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        Loader.getInstance();

        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("Main");
        }
    }
}