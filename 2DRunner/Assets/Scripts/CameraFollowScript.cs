using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

    public GameObject target;        //target for the camera to follow
    public float xOffset;           //how much x-axis space should be between the camera and target
    private Rigidbody2D rbody;
    void Start()
    {
        rbody = target.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //follow the target on the x-axis only
        transform.position = new Vector3(rbody.position.x + xOffset, transform.position.y, transform.position.z);
        //rbody.velocity = new Vector2(5f, 0);
    }
}
