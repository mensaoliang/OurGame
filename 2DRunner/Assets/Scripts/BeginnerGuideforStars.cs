using UnityEngine;
using System.Collections;

public class BeginnerGuideforStars : MonoBehaviour {


    public GameObject panel2; // Assign in inspector
    public GameObject button; // Assign in inspector

    public void StarsOnClick()
    {
        if (PlayerPrefs.GetInt("FirstUseStars") == 1)
        {
            panel2.SetActive(true);
            PlayerPrefs.SetInt("FirstUseStars", 0);
        }
        else
        {
            panel2.SetActive(false);
        }
    }

    public void onClickStarsSureButton()
    {
        panel2.SetActive(false);
    }

}
