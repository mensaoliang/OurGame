using UnityEngine;
using System.Collections;

public class VineScript : MonoBehaviour {
    //this script controls the movement of vines
    public float vineHorizontalSpeed; //a positive float number
    public float vineVerticalSpeed; //a positive float number
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
        upperBound = parentTrans.position.y + 8.5f;
        botBound = parentTrans.position.y + 4.5f;
        leftBound = parentTrans.position.x - 0.5f;
        rightBound = parentTrans.position.x + 6f;
	}
	
	void FixedUpdate()
    {
        Vector2 pos = trans.position;
        float HorizontalDelta = Time.fixedDeltaTime * vineHorizontalSpeed;
        float VerticalDelta = Time.fixedDeltaTime * vineVerticalSpeed;
        float VerticalFlag = goUp ? 1f : -1f;
        float HorizontalFlag = goLeft ? -1f : 1f;
        trans.position = new Vector2(pos.x + HorizontalFlag * HorizontalDelta, pos.y + VerticalFlag * VerticalDelta);
        if (goUp && trans.position.y > upperBound)   //vertically go up and reached the top
            goUp = false;
        else if (!goUp && trans.position.y < botBound)//vertically go down and reached the bottom
            goUp = true;
        if (goLeft && trans.position.x < leftBound)//vine is horizontally moving and reached the left
            goLeft = false;
        else if (!goLeft && trans.position.x > rightBound)  //vine is horizontally moving and reached the right
            goLeft = true;
    }
}
