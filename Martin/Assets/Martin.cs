﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martin : MonoBehaviour {

    private Vector2 _speed = new Vector2(5, 5);
    private Vector2 _direction;
    // Use this for initialization
    void Start () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(
            10,
            0);
    }
	
	// Update is called once per frame
	void Update () {

    }

    void FixedUpdate()
    {
        //GetComponent<Rigidbody2D>().position = new Vector3(transform.position.x+1, transform.position.y);
        //GetComponent<Rigidbody2D>().position.Set(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y) ;
    }
}
