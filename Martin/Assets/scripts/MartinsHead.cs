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
        Debug.LogError("collision ! ");
        if (collision.gameObject.tag.Equals("catchObject"))
        {
            Debug.LogError("collision avec un trapeze ! ");
            HingeJoint2D joint = collision.gameObject.AddComponent<HingeJoint2D>();
            joint.connectedBody = GetComponentInParent<Rigidbody2D>();

            martin.GetComponent<Martin>().Grab();
        }
    }
}
