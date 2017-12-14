using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martin : MonoBehaviour {

    private Vector2 _speed = new Vector2(5, 5);
    private Vector2 _direction;
    private bool _isJumping;
    private Rigidbody2D rigidBody;


    // Use this for initialization
    void Awake()
    {
        _isJumping = false;
        rigidBody = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = new Vector2(
            Loader.getInstance()._martinSpeed,
            0);
    }
	
	// Update is called once per frame
	void Update () {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
        transform.Translate(x, 0f, 0f, Space.World);

        if (Input.GetButtonDown("Jump"))
        {
            if (!_isJumping)
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
    }

    private void Jump()
    {
        _isJumping = true;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 5f);
        //rigidBody.AddTorque(-10f, ForceMode2D.Impulse);

        //GetComponent<Animator>().SetTrigger("Jump");
        //GetComponent<AudioSource>().Play();
    }

    public void EndJump()
    {
        _isJumping = false;
    }

    public void Grab()
    {
        Debug.LogError("Grab");
        rigidBody.velocity = new Vector2(0f, 0f);
        _isJumping = false;

        //GetComponent<Animator>().SetTrigger("Jump");
        //GetComponent<AudioSource>().Play();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            EndJump();
        }
    }
}
