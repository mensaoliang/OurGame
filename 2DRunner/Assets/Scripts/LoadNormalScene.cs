using UnityEngine;
using System.Collections;

public class LoadNormalScene : MonoBehaviour {
    public void LoadNewScene(int sceneNum)
    {

        if (PlayerPrefs.HasKey("FirstTimePlay"))
        {
            PlayerPrefs.SetInt("FirstTimePlay", 0);
        }
        else {
            PlayerPrefs.SetInt("FirstTimePlay", 1); 
        }


        if (!PlayerPrefs.HasKey("FirstUseCloud"))
        {
            PlayerPrefs.SetInt("FirstUseCloud", 1);
        }


        if (!PlayerPrefs.HasKey("FirstUseStars"))
        {
            PlayerPrefs.SetInt("FirstUseStars", 1);
        }

        PlayerPrefs.Save();
        Application.LoadLevel(sceneNum);
    }
}
