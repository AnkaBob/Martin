using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Martin : MonoBehaviour {

    private bool _isJumping;
    private bool _canStartANewJump;
    private bool _isBalancing;

    private Rigidbody2D rigidBody;
    private FixedJoint2D joint;
    private GameObject lastTrapeze;

    private float _martinSpeed;
    private float _jumpPuissanceMax;
    private float _jumpMinTime;
    private float _jumpMaxTime;
    private float _trapezeSpeed;
    private int scorenb=0;

    public Text Scoretext;
    public GameObject BlackScreen;

    //private float jumpTimeStart = 0;
    //Stopwatch stopwatch = new Stopwatch();
    public MartinsHead head;

    float lastTimeRecordedJump;
    float startJumpTime;


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
        _canStartANewJump = false;
        rigidBody = GetComponent<Rigidbody2D>();
        StartTheMovement();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            JumpStart();
        }
        if (_isJumping)
        {            
            JumpUpgrade(Time.time * 1000);
        }
        if (Input.GetButtonUp("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            JumpStop();
        }
        if (transform.position.y < -5)
        {
            StartCoroutine(EndScreen());
        }
        
        scorenb = Mathf.Max(((int)transform.position.x + 10) * 10, scorenb);
        PlayerPrefs.SetInt("Score", scorenb); //mise à jour du score
        Scoretext.text = scorenb.ToString();
    }

    private IEnumerator EndScreen()
    {
        BlackScreen.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("EndScreen");
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

    bool JumpStart()
    {
        if (_isBalancing)
        {
            Release();
            _isBalancing = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            SoundEffectsHelper.Instance.MakeJump2Sound();
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Jump");
        }
        if (!_canStartANewJump)
            return false;

        if (rigidBody.position.y < 1.3f)
            return false;
        
        //time = System.Math.Min(_jumpMaxTime, time);
        //time = System.Math.Max(_jumpMinTime, time);
        //float puissance = time / _jumpMaxTime;
        //print(puissance);
        _isJumping = true;
        _canStartANewJump = false;
        lastTimeRecordedJump = Time.time * 1000;
        startJumpTime = Time.time * 1000;
        //rigidBody.velocity = new Vector2(rigidBody.velocity.x, _jumpPuissanceMax );
        rigidBody.velocity = new Vector2(Mathf.Max(rigidBody.velocity.x,_martinSpeed), _jumpPuissanceMax * 1/3);
        SoundEffectsHelper.Instance.MakeJump1Sound();
        return true;
    }
    void JumpUpgrade(float time)
    {
        if (time - startJumpTime < _jumpMinTime)
            return;
        time = time - _jumpMinTime;
        print("JumpUpgrade");
        if ((time-startJumpTime) >= _jumpMaxTime)
            return;
        
        float delay = System.Math.Min(_jumpMaxTime, time - lastTimeRecordedJump);
        print("JumpUpgrade : " + delay);
        lastTimeRecordedJump = time;

         float puissance = delay / _jumpMaxTime;
        //puissance=puissance * 0.5f;
        print("JumpUpgrade : " + puissance);

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y+_jumpPuissanceMax * (puissance*2/3));
        
        //GetComponent<AudioSource>().Play();
    }

    public void JumpStop()
    {
        _isJumping = false;
        startJumpTime = 0;
        lastTimeRecordedJump = 0;
    }

    public void ResetJump()
    {
        _canStartANewJump = true;
        _isJumping = false;
        startJumpTime = 0;
        lastTimeRecordedJump = 0;
    }

    public void Grab(GameObject catchedObject)
    {
        GetComponent<Animator>().SetTrigger("Grab");
        rigidBody.velocity = new Vector2(0f, 0f);
        _isJumping = false;
        _isBalancing = true;
        rigidBody.constraints = RigidbodyConstraints2D.None;

        if (lastTrapeze == null)
        {
            lastTrapeze = catchedObject;
            rigidBody.position = new Vector2(lastTrapeze.transform.GetChild(0).transform.position.x + 0.1f,
                lastTrapeze.transform.GetChild(0).transform.position.y + 1.3f);
            //lastTrapeze.GetComponent<Collider2D>().enabled = false;
            joint = catchedObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = head.GetComponentInParent<Rigidbody2D>();
            SoundEffectsHelper.Instance.MakeGrabSound();
        }

        catchedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_trapezeSpeed, _trapezeSpeed);
        ResetJump();
    }

    void Release()
    {
        GetComponent<Animator>().SetTrigger("Ungrab");
        lastTrapeze.GetComponent<Collider2D>().enabled = false;
        joint.connectedBody = null;
        joint = null;
        lastTrapeze = null;
        //lastTrapeze.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
        //StartTheMovement();
    }

    public void HitTheGround()
    {
        ResetJump();
        StartTheMovement();
    }
}
