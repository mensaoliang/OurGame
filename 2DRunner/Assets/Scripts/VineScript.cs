using UnityEngine;
using System.Collections;

public class VineScript : MonoBehaviour {
    //this script controls the movement of vines
    public float vineSpeed; //a positive float number, if verticalMoving is true, this is the vertical speed, or it is the horizontal speed
    public bool verticalMoving;    //if the vine is moving vertically, this is true, or it is false
    private bool goUp = true;   //if the vine is going up now, this is true; if it is going down, this is false
    private bool goLeft = true; //if the vine is going left now, this is true; if it is going right, this is false
    private Transform trans;
    private Transform parentTrans;  //transform of the scene
    private float upperBound;   //the highest position the vine can go
    private float botBound; //the lowest position the vine can go
    private float leftBound;    //the lowest position the vine can go
    private float rightBound;   //the right position the vine can go
	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        parentTrans = trans.parent.gameObject.transform;
        upperBound = parentTrans.position.y + 8f;
        botBound = parentTrans.position.y + 4f;
        leftBound = parentTrans.position.x - 0.5f;
        rightBound = parentTrans.position.x + 6f;
	}
	
	void FixedUpdate()
    {
        Vector2 pos = trans.position;
        float delta = Time.deltaTime * vineSpeed;
        if (verticalMoving) //vine is vertically moving
        {
            if (goUp)
            {

                trans.position = new Vector2(pos.x, pos.y + delta);
                if (trans.position.y > upperBound)  //turn head to go down
                    goUp = false;
            }
            else
            {
                trans.position = new Vector2(pos.x, pos.y - delta);
                if (trans.position.y < botBound)    //turn head to go up
                    goUp = true;
            }
        }
        else//vine is horizontally moving
        {
            if (goLeft)
            {
                trans.position = new Vector2(pos.x - delta, pos.y);
                if (trans.position.x < leftBound)   //turn head to go right
                    goLeft = false;
            }
            else
            {
                trans.position = new Vector2(pos.x + delta, pos.y);
                if (trans.position.x > rightBound)  //turn head to go left
                    goLeft = true;
            }
        }
    }
}
