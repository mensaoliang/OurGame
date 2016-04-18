using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using UnityEngine.Audio;

public class PlayerScript : MonoBehaviour {

    public float upForce;       //upward force of the "jump"
    public float forwardSpeed;  //forward movement speed
    public bool isDead = false; //has the player collided with a obstacle?
    public GameObject sceneScroller;    //use this to get access to ScrollSceneScript which is attached to it
    public Text scoreText;
    public float teleSpeed;     //moving speed of using binary star
    /////////////////////
    public GameObject panelForDiamondTutorial;
    ///////////////////
    public GameObject explosion;    //when player touches bomb, the bomb will explode
	public AudioClip[] stings;

    private Rigidbody2D rbody;
    private int score = 0;
    private bool tele = false;      //is the player using binary star? 
    private Animator anim;              //reference to the animator component
    private bool jump = false;          //has the player triggered a "jump"?
    private bool canJump = true;       //can the player jump at this moment?
    private bool run = false;           //is the player running?
    private Vector2 startPos, endPos;   //the start and end position of tele
    private Transform trans;
    private float gravScale; //the default gravScale of the player rigidbody
    private Vector3 decScale = new Vector3(0.3f, 0.3f, 0f); //by which player scale will get smaller while using star
    private AudioSource source;


    // Use this for initialization
    public static int IntParseFast(string value)
	{
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			char letter = value[i];
			result = 10 * result + (letter - 48);
		}
		return result;
	}
	void Start () {
        //get reference to the animator component
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();  //get the transform of player, this variable is mainly used for changing the scale of player
                                            //set the character moving forward 
        source = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(forwardSpeed, 0);
        scoreText.text = "Score: " + score.ToString();
        gravScale = rbody.gravityScale;
    }
	
	// Update is called once per frame
	void Update () {
        //don't allow control if the character has died
        if (isDead)
            return;
        //look for input to trigger a "jump"

        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump && Time.timeScale > 0.1)
        {
            jump = true;
            run = false;
        }
	}
    void FixedUpdate()
    {
        //if a "jump" is triggered
        if (jump)
        {
            jump = false;
            canJump = false;//cant do another jump while jumping
            anim.SetBool("jump", true);
            anim.SetBool("idle", false);
            anim.SetBool("run", false);
            rbody.velocity = new Vector2(forwardSpeed, 0);
            //giving the character some upward force
            rbody.AddForce(new Vector2(0, upForce));
			PlaySound (2);
        }
        else if (run)
           rbody.velocity = new Vector2(forwardSpeed, rbody.velocity.y);
        else if (tele)
        {
            /*
            counter += 0.1f / teleSpeed;
            float x = Mathf.Lerp(0, dist, counter);
            rbody.position = x * Vector3.Normalize(endPos - startPos) + startPos;
            */
            float step = teleSpeed * Time.fixedDeltaTime;
            trans.position = Vector2.MoveTowards(trans.position, endPos, step);
            //print("tele: " + rbody.position.x.ToString() + " => " + endPos.x.ToString() + tele);
            //print("tele Y: " + rbody.position.y.ToString() + " => " + endPos.y.ToString() + tele);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")//land on the ground
        {
            if (tele)   //if using star at this moment
            {
                tele = false;
                trans.localScale += decScale;
                rbody.gravityScale = gravScale;
            }
            run = true;
            canJump = true;
            jump = false;
            anim.SetBool("idle", false);
            anim.SetBool("jump", false);
            anim.SetBool("run", true);
        }
        else if (other.collider.tag == "dead")
        {
            isDead = true;
            GameControl.current.Died();
            print("dead\n");
        }
       //else run = false;
    }
	public void PlaySound(int clip)
	{
        if (PlayerPrefs.GetInt("EffectOn") == 0)    //if effect sound is muted, then don't play sound
            return;
        source.clip = stings [clip];
		source.Play ();
	}
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "coin") 
		{
			other.gameObject.SetActive (false);
			score++;
			scoreText.text = "Score: " + score.ToString ();
            if (source.isPlaying == false)  //don't play duplicate coin sound
                PlaySound(4);
		} 
		else if (other.tag == "power") 
		{
			other.gameObject.SetActive (false);
            sceneScroller.GetComponent<ScrollSceneScript>().IncDiamondCount();  //add diamond count by 1
			PlaySound (1);
            //////////////////////ADD BY ZHENG///////////////////////
            /////////////////////////////////////////////////////////
            if (PlayerPrefs.GetInt("FirstCollectDiamond") == 1)
            {
                panelForDiamondTutorial.SetActive(true);
                PlayerPrefs.SetInt("FirstCollectDiamond", 0);
                Time.timeScale = 0;
            }
            else
            {
                panelForDiamondTutorial.SetActive(false);
            }
            /////////////////////////////////////////////////////////
            ///////////////////////END OF ADD////////////////////////
        }
        else if (other.tag == "jumpCloud")
        {
            //can't do another jump while using jump cloud
            if (tele)   //if using star at this moment
            {
                tele = false;
                trans.localScale += decScale;
                rbody.gravityScale = gravScale;
            }
            canJump = false;
            other.gameObject.SetActive(false);
            anim.SetBool("jump", true);
            anim.SetBool("idle", false);
            anim.SetBool("run", false);
            rbody.velocity = new Vector2(forwardSpeed, 0);
            //giving the character some upward force
            rbody.AddForce(new Vector2(0, upForce));
			PlaySound (2);
        }
        else if (other.tag == "starMain")   //touches the main star
        {
            if (!tele)  //if player is not using star before
                trans.localScale -= decScale;    //the player scale becomes smaller
            startPos = other.transform.position;
            trans.position = startPos;
            other.gameObject.SetActive(false);
            endPos = other.transform.parent.GetComponent<Transform>().GetChild(1).transform.position;
            rbody.gravityScale = 0.0f;  //while using star, player will not be affected by gravity
            rbody.velocity = new Vector2(0f, 0f);
            canJump = false;
            tele = true;
            run = false;
            anim.SetBool("idle", true);     
        }
        else if (other.tag == "starCom" && tele && Vector2.Distance(endPos, other.transform.position) < 0.1f)    //reaches the destination star com while teling
        {
            tele = false;
            trans.localScale += decScale;
            rbody.gravityScale = gravScale;
			PlaySound (3);
            other.transform.parent.GetComponent<LineRenderer>().SetWidth(0f, 0f);
            other.transform.parent.GetComponent<Transform>().GetChild(1).gameObject.SetActive(false);
            rbody.velocity = new Vector2(forwardSpeed, 0);
        }
        else if (other.tag == "bomb" || other.tag == "Vine" || other.tag == "rock")
        {
            if (other.tag == "bomb")    //bomb explode
            {
                Destroy(other.gameObject);
                GameObject expEffect = Instantiate(explosion, other.transform.position, Quaternion.identity) as GameObject;
                Destroy(expEffect, 1);  //display the explosion effect at the same positon for 1s
                PlaySound(0);
            }
            else if (other.tag == "rock")   //play effect sound
                PlaySound(0);
            isDead = true;
            GameControl.current.Died();
        }
    }
}
