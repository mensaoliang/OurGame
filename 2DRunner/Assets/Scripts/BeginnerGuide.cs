using UnityEngine;
using System.Collections;

public class BeginnerGuide : MonoBehaviour
{
    public GameObject panel; // Assign in inspector
    public GameObject panel1; // Assign in inspector
    public GameObject panel2; // Assign in inspector
    public GameObject panel3; // Assign in inspector
    public GameObject button; // Assign in inspector
    //private bool isShowing;
    //private float currentTime = 0.0f, startTime = 0.0f, timeToWait = 2.0f;

    // Use this for initialization
    void Start()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        if (PlayerPrefs.GetInt("FirstTimePlay") == 1)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
            PlayerPrefs.SetInt("FirstTimePlay", 0);
        }
        else {
            panel.SetActive(false);
        }
    }

    // Update is called once per frame
    public void clickOnUnderstandButton()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}
