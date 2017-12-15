using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Martin : MonoBehaviour {
    
    private bool _isJumping;
    private bool _isBalancing;

    private Rigidbody2D rigidBody;
    private FixedJoint2D joint;
    private GameObject lastTrapeze;

    private float _martinSpeed;
    private float _jumpPuissanceMax;
    private float _jumpMinTime;
    private float _jumpMaxTime;
    private float _trapezeSpeed;
    private int scorenb;

    public Text Scoretext;

    //private float jumpTimeStart = 0;
    Stopwatch stopwatch = new Stopwatch();

    private void Start()
    {
        _martinSpeed = Loader.getInstance()._martinSpeed;
        _jumpPuissanceMax = Loader.getInstance()._jumpPuissanceMax;
        _jumpMinTime = Loader.getInstance()._jumpMinTime;
        _jumpMaxTime = Loader.getInstance()._jumpMaxTime;
        _trapezeSpeed = Loader.getInstance()._trapezeSpeed;
    }
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
        if (transform.position.y<-2)
        {
            SceneManager.LoadScene("EndScreen");
        }
        scorenb= ((int)transform.position.x + 10) * 10;
        PlayerPrefs.SetInt("Score", scorenb); //mise à jour du score
        Scoretext.text = "Score : " + scorenb;
    }

    void FixedUpdate()
    {
    }

    void StartTheMovement()
    {
        transform.rotation = new Quaternion();
        rigidBody.velocity = new Vector2(
            _martinSpeed,
            0);
    }

    void Jump(float time)
    {
        time = System.Math.Min(_jumpMaxTime, time);
        time = System.Math.Max(_jumpMinTime, time);
        float puissance = time / _jumpMaxTime;
        print(puissance);
        _isJumping = true;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, _jumpPuissanceMax * puissance);

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

        catchedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_trapezeSpeed, _trapezeSpeed);
        //GetComponent<Animator>().SetTrigger("Jump");
        //GetComponent<AudioSource>().Play();
    }

    void Release()
    {
        lastTrapeze.GetComponent<Collider2D>().enabled = false;
        joint.connectedBody = null;
        joint = null;
        //lastTrapeze.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
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
