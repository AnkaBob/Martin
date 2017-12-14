using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartinsHead : MonoBehaviour {

    public Martin martin;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("catchObject"))
        {
            martin.GetComponent<Martin>().Grab(collision.gameObject);
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
