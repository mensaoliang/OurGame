using UnityEngine;
using System.Collections;

public class DragStarCom : MonoBehaviour {
    public float maxStretch;        //the longest distance the binary star can be streched
    public Transform startPos;
    private bool clickedOn = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (clickedOn)
            Dragging();
	}
    void OnMouseDown()
    {
        clickedOn = true;
    }
    void OnMouseUp()
    {
        clickedOn = false;
    }
    void Dragging()     
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPoint.z = 0f;
        float dist = Vector3.Distance(startPos.position, mouseWorldPoint);
        Vector3 endPoint = mouseWorldPoint;
        if (dist > maxStretch)
            endPoint = maxStretch * Vector3.Normalize(mouseWorldPoint - startPos.position) + startPos.position;
        transform.position = endPoint;
    }
}
