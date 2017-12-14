using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martin : MonoBehaviour {

    private Vector2 _speed = new Vector2(5, 5);
    private Vector2 _direction;
    private Rigidbody2D rigidBody;


    // Use this for initialization
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(
            10,
            0);
    }
	
	// Update is called once per frame
	void Update () {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
        transform.Translate(x, 0f, 0f, Space.World);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        //GetComponent<Rigidbody2D>().position = new Vector3(transform.position.x+1, transform.position.y);
        //GetComponent<Rigidbody2D>().position.Set(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y) ;
    }

    private void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 5f);
        //rigidBody.AddTorque(-10f, ForceMode2D.Impulse);

        GetComponent<Animator>().SetTrigger("Jump");
        GetComponent<AudioSource>().Play();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.tag.Equals("catchObject"))
        {
        }*/
    }
}
