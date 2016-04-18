using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadBeginnerScene : MonoBehaviour {
    public void LoadNewScene(int sceneNum)
    {
        PlayerPrefs.DeleteKey("FirstTimePlay");
        PlayerPrefs.DeleteKey("FirstUseCloud");
        PlayerPrefs.DeleteKey("FirstUseStars");
        PlayerPrefs.DeleteKey("FirstCollectDiamond");

        PlayerPrefs.SetInt("FirstTimePlay",1);
        PlayerPrefs.SetInt("FirstUseCloud", 1);
        PlayerPrefs.SetInt("FirstUseStars", 1);
        PlayerPrefs.SetInt("FirstCollectDiamond", 1);

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
