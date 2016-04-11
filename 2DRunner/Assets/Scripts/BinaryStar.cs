using UnityEngine;
using System.Collections;

public class BinaryStar : MonoBehaviour {
    private LineRenderer lineRend;
    public Transform startPos;
    public Transform endPos;
    public float textureShiftSpeed;
    private float textureOffset = 0f;
	// Use this for initialization
	void Start ()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.sortingLayerName = "Background";	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //update line positions
        lineRend.SetPosition(0, startPos.position);
        float dist = Vector3.Distance(startPos.position, endPos.position);
        float validDist = dist - 0.45f;
        if (validDist < 0) validDist = 0;
        Vector3 pointEnd = validDist * Vector3.Normalize(endPos.position - startPos.position) + startPos.position;
        lineRend.SetPosition(1, pointEnd);

        textureOffset -= Time.deltaTime * textureShiftSpeed;
        if (textureOffset < -10f)
            textureOffset += 10f;
        lineRend.sharedMaterials[0].SetTextureOffset("_MainTex", new Vector2(textureOffset, 0f));
	}
}
