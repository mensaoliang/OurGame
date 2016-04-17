using UnityEngine;
using System.Collections;

public class MusicBackSwitch : MonoBehaviour {

    public GameObject MusicCheckMark;

    // Use this for initialization
    void Start()
    {
        //Debug.Log(PlayerPrefs.GetInt("MusicOn"));
        if (PlayerPrefs.HasKey("MusicOn"))
        {
            if (PlayerPrefs.GetInt("MusicOn") == 1)
            {
                MusicCheckMark.SetActive(true);
            }
            else
            {
                MusicCheckMark.SetActive(false);
            }
        }

    }

    public void OnSwitchMusic()
    {
        if (!PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.SetInt("MusicOn", 1);
            MusicCheckMark.SetActive(true);
            //Debug.Log(PlayerPrefs.GetInt("MusicOn"));
        }
        else
        {
            if (PlayerPrefs.GetInt("MusicOn") == 1)
            {
                PlayerPrefs.SetInt("MusicOn", 0);
                MusicCheckMark.SetActive(false);
                Debug.Log(PlayerPrefs.GetInt("MusicOn"));
            }
            else
            {
                PlayerPrefs.SetInt("MusicOn", 1);
                MusicCheckMark.SetActive(true);
                //Debug.Log(PlayerPrefs.GetInt("MusicOn"));
            }
        }

    }

}
