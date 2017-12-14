using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartinsHead : MonoBehaviour {

    public Martin martin;

    bool isCatching;

    // Use this for initialization
    void Start () {
        isCatching = false;
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCatching && collision.gameObject.tag.Equals("catchObject"))
        {
            isCatching = true;
            martin.GetComponent<Martin>().Grab(collision.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("catchObject"))
        {
            isCatching = false;
        }
    }
}
