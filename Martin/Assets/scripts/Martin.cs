using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martin : MonoBehaviour {
    
    private bool _isJumping;
    private bool _isBalancing;

    private Rigidbody2D rigidBody;
    private FixedJoint2D joint;
    private GameObject lastTrapeze;

    // Use this for initialization
    void Awake()
    {
        _isJumping = false;
        _isBalancing = false;
        rigidBody = GetComponent<Rigidbody2D>();
        StartTheMovement();
    }
	
	// Update is called once per frame
	void Update ()
    {
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

    void StartTheMovement()
    {
        transform.rotation = new Quaternion();
        rigidBody.velocity = new Vector2(
            Loader.getInstance()._martinSpeed,
            0);
    }

    void Jump()
    {
        _isJumping = true;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 8f);

        //GetComponent<Animator>().SetTrigger("Jump");
        //GetComponent<AudioSource>().Play();

        if(_isBalancing)
        {
            Release();
            _isBalancing = false;
        }
    }

    public void EndJump()
    {
        _isJumping = false;
    }

    public void Grab(GameObject catchedObject)
    {
        rigidBody.velocity = new Vector2(0f, 0f);
        _isJumping = false;
        _isBalancing = true;
        if (joint == null)
        {

            lastTrapeze = catchedObject;
            joint = catchedObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = GetComponentInParent<Rigidbody2D>();
        }

        catchedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
        //GetComponent<Animator>().SetTrigger("Jump");
        //GetComponent<AudioSource>().Play();
    }

    void Release()
    {
        lastTrapeze.GetComponent<Collider2D>().enabled = false;
        joint.connectedBody = null;
        joint = null;
        //StartTheMovement();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            EndJump();
            StartTheMovement();
        }
    }
}
