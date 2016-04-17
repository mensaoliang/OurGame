using UnityEngine;
using System.Collections;

public class BeginnerGuideforCloud : MonoBehaviour {

    public GameObject panel1; // Assign in inspector
    public GameObject button; // Assign in inspector
  
    public void CloudOnClick()
    {
        if (PlayerPrefs.GetInt("FirstUseCloud") == 1)
        {
            panel1.SetActive(true);
            PlayerPrefs.SetInt("FirstUseCloud", 0);
        }
        else
        {
            panel1.SetActive(false);
        }
    }

    public void onClickCloudSureButton() {
            panel1.SetActive(false);
    }


}
