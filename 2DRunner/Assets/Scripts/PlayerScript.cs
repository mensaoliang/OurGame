using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float upForce;       //upward force of the "jump"
    public float forwardSpeed;  //forward movement speed
    public bool isDead = false; //has the player collided with a obstacle?
	public Text diamondCount;
    public Text scoreText;
    public float teleSpeed;     //moving speed of using binary star

    private float counter, dist;    //counter and dist are used for binary star
    private Rigidbody2D rbody;
    private int score = 0;
	private int diamond = 0;
    private bool tele = false;      //is the player using binary star? 
    private Animator anim;              //reference to the animator component
    private bool jump = false;          //has the player triggered a "jump"?
    private bool canJump = true;       //can the player jump at this moment?
    private bool run = false;           //is the player running?
    private Vector3 startPos, endPos;   //the start and end position of tele
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
        //set the character moving forward
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(forwardSpeed, 0);
        scoreText.text = "Score: " + score.ToString();
		diamondCount.text = diamondCount.text = diamond.ToString ();
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
        }
        else if (run)
           rbody.velocity = new Vector2(forwardSpeed, rbody.velocity.y);
        else if (tele)
        {
            counter += 0.1f / teleSpeed;
            float x = Mathf.Lerp(0, dist, counter);
            rbody.position = x * Vector3.Normalize(endPos - startPos) + startPos;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")//land on the ground
        {
            run = true;
            canJump = true;
            jump = false;
            tele = false;
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
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "coin") 
		{
			other.gameObject.SetActive (false);
			score++;
			scoreText.text = "Score: " + score.ToString ();
		} 
		else if (other.tag == "power") 
		{
			other.gameObject.SetActive (false);
			diamond = IntParseFast (diamondCount.text);
			diamond++;
			diamondCount.text = diamond.ToString ();
		}
        else if (other.tag == "jumpCloud")
        {
            //can't do another jump while using jump cloud
            canJump = false;
            tele = false;
            other.gameObject.SetActive(false);
            anim.SetBool("jump", true);
            anim.SetBool("idle", false);
            anim.SetBool("run", false);
            rbody.velocity = new Vector2(forwardSpeed, 0);
            //giving the character some upward force
            rbody.AddForce(new Vector2(0, upForce));
        }
        else if (other.tag == "starMain")
        {
            canJump = false;
            tele = true;
            run = false;
            anim.SetBool("idle", true);
            startPos = other.transform.position;
            other.gameObject.SetActive(false);
            endPos = other.transform.parent.GetComponent<Transform>().GetChild(1).transform.position;
            dist = Vector3.Distance(startPos, endPos);
            counter = 0;
        }
        else if (other.tag == "starCom" && tele)    //reaches the star com while teling
        {
            tele = false;
            other.transform.parent.GetComponent<LineRenderer>().SetWidth(0f, 0f);
            other.transform.parent.GetComponent<Transform>().GetChild(1).gameObject.SetActive(false);
            rbody.velocity = new Vector2(forwardSpeed, 0);
        }
        else if (other.tag == "bomb" || other.tag == "Vine")
        {
            isDead = true;
            GameControl.current.Died();
        }
    }
}
