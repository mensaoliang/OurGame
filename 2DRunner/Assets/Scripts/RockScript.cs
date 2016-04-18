using UnityEngine;
using System.Collections;

public class RockScript : MonoBehaviour {
	
    void FixedUpdate()
    {
        transform.Rotate (new Vector3(0f, 0f, 180f) * Time.fixedDeltaTime);
    }
}
