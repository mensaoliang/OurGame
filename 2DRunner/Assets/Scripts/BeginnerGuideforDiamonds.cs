using UnityEngine;
using System.Collections;

public class BeginnerGuideforDiamonds : MonoBehaviour {

    public GameObject panelForDiamondTutorial;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickSureDiamond()
    {
        panelForDiamondTutorial.SetActive(false);
        Time.timeScale = 1;
    }
}
