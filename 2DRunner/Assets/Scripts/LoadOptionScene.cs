using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOptionScene : MonoBehaviour {

    public void LoadNewScene(int sceneNum)
    {

        //Debug.Log(PlayerPrefs.GetInt("MusicOn"));

        //Debug.Log(PlayerPrefs.HasKey("MusicOn"));

        if (!PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        if (!PlayerPrefs.HasKey("EffectOn"))
        {
            PlayerPrefs.SetInt("EffectOn", 1);
        }
        //Debug.Log(PlayerPrefs.GetInt("MusicOn"));
        SceneManager.LoadScene(sceneNum);
    }
}
