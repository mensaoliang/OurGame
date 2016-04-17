using UnityEngine;
using System.Collections;

public class MusicEffectSwitch : MonoBehaviour {

    public GameObject EffectCheckMark;

    // Use this for initialization
    void Start () {

        if (PlayerPrefs.HasKey("EffectOn"))
        {
            if (PlayerPrefs.GetInt("EffectOn") == 1)
            {
                EffectCheckMark.SetActive(true);
            }
            else
            {
                EffectCheckMark.SetActive(false);
            }
        }
    }
	
    public void OnSwitchEffect()
    {
        if (!PlayerPrefs.HasKey("EffectOn"))
        {
            PlayerPrefs.SetInt("EffectOn", 1);
            EffectCheckMark.SetActive(true);
        }
        else
        {
            if (PlayerPrefs.GetInt("EffectOn") == 1)
            {
                PlayerPrefs.SetInt("EffectOn", 0);
                EffectCheckMark.SetActive(false);
            }
            else
            {
                PlayerPrefs.SetInt("EffectOn", 1);
                EffectCheckMark.SetActive(true);
            }
        }
    }

}
