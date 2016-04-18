using UnityEngine;
using System.Collections;

public class FallingStonesScript : MonoBehaviour {
    public float leftwardSpeed; //leftward speed of the falling stone
    public GameObject rock; //this is the falling stone
    private float lastFired;   //last time to generate a falling stone
    private Transform startPos; //the position to generate a falling stone
    // Use this for initialization
	void Start () {
        startPos = transform.FindChild("StoneStartPos");    //get the position
        lastFired = Time.time - 2f;
	}
	
    void FixedUpdate ()
    {
        if (Time.time > lastFired + 2f)
        {
            GameObject newRock = Instantiate(rock, startPos.position, Quaternion.identity) as GameObject;
            Rigidbody2D rbody = newRock.GetComponent<Rigidbody2D>();
            rbody.gravityScale = 0.8f;    //stone is affacted by gravity
            rbody.velocity = new Vector2(-leftwardSpeed, 0f);   //flies to left
            Destroy(newRock, 3f);
            lastFired = Time.time;
        }
    }
}
