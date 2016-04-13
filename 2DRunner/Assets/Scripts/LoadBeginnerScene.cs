using UnityEngine;
using System.Collections;

public class LoadBeginnerScene : MonoBehaviour {
    public void LoadNewScene(int sceneNum)
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("FirstTimePlay",1);
        PlayerPrefs.SetInt("FirstUseCloud", 1);
        PlayerPrefs.SetInt("FirstUseStars", 1);
        PlayerPrefs.Save();

        Application.LoadLevel(sceneNum);
    }
}
