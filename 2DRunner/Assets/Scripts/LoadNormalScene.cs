using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

        if (!PlayerPrefs.HasKey("FirstCollectDiamond"))
        {
            PlayerPrefs.SetInt("FirstCollectDiamond", 1);
        }

        if (!PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }

        if (!PlayerPrefs.HasKey("EffectOn"))
        {
            PlayerPrefs.SetInt("EffectOn", 1);
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneNum);
    }
}
