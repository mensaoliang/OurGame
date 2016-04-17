using UnityEngine;
using System.Collections;

public class SoundDuringGame : MonoBehaviour {

    public GameObject SoundOn;
    public GameObject SoundOff;
    AudioSource MusicPlayer;

	// Use this for initialization
	void Start () {

        MusicPlayer = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        Debug.Log(PlayerPrefs.GetInt("MusicOn"));
        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
            MusicPlayer.mute = false;
        }
        else{
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
            MusicPlayer.mute = true;
        }
    }
	
	// Update is called once per frame
	public void OnClickSoundOnButton () {
        SoundOn.SetActive(false);
        SoundOff.SetActive(true);
        PlayerPrefs.SetInt("MusicOn", 0);
        Debug.Log(PlayerPrefs.GetInt("MusicOn"));
        MusicPlayer.mute = true;
    }

    public void OnClickSoundOffButton()
    {
        SoundOn.SetActive(true);
        SoundOff.SetActive(false);
        PlayerPrefs.SetInt("MusicOn", 1);
        Debug.Log(PlayerPrefs.GetInt("MusicOn"));
        MusicPlayer.mute = false;
    }

}
