using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Martin : MonoBehaviour {
    
    private bool _isJumping;
    private bool _isBalancing;

    private Rigidbody2D rigidBody;
    private FixedJoint2D joint;
    private GameObject lastTrapeze;

    //private float jumpTimeStart = 0;
    Stopwatch stopwatch = new Stopwatch();

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
            stopwatch.Start();
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (!_isJumping)
            {
                stopwatch.Stop();
                Jump(stopwatch.ElapsedMilliseconds);
                stopwatch.Reset();
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

    void Jump(float time)
    {
        time = System.Math.Min(400, time);
        time = System.Math.Max(200, time);
        float puissance = time / 400;
        print(puissance);
        _isJumping = true;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 10f* puissance);

        //GetComponent<Animator>().SetTrigger("Jump");
        //GetComponent<AudioSource>().Play();

        if(_isBalancing)
        {
            Release();
            _isBalancing = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        rigidBody.constraints = RigidbodyConstraints2D.None;

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
        //joint = null;
        lastTrapeze.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
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
