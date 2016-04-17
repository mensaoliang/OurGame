using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameControl : MonoBehaviour {
    public static GameControl current;  //a reference to our game control
    public Button jumpCloudBtn;         //a reference to our jump cloud button
    public Button binaryStarBtn;        //a reference to our binary star button
    public GameObject gameOverText;
	public AudioMixerSnapshot audio_pause;
	public AudioMixerSnapshot audio_running;
	public AudioMixerSnapshot audio_died;
	public float bpm = 128;

    private bool isGameOver = false;    //is the game over?
    private bool paused = false;        //is the game paused?
	private float m_TransitionPause;
	private float m_TransitionRestart;
	private float m_QuarterNote;
    
    void Awake()
    {
        //if we don't currently have a game control...
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);
    }
	// Use this for initialization
	void Start () {
        jumpCloudBtn.gameObject.SetActive(false);
        binaryStarBtn.gameObject.SetActive(false);
		m_QuarterNote = 60 / bpm;
		m_TransitionPause = m_QuarterNote*2;
		m_TransitionRestart = m_QuarterNote * 4;

	}
	
	// Update is called once per frame
	void Update () {
        if (isGameOver && Input.anyKey)
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
			audio_running.TransitionTo (m_TransitionRestart);

        }
	    if (Input.GetKeyDown(KeyCode.Space))//press space to pause or not pause
        {
            if (paused)             //make all tool buttons invisible
            {
                Time.timeScale = 1;
                paused = false;
                jumpCloudBtn.gameObject.SetActive(false);
                binaryStarBtn.gameObject.SetActive(false);
				audio_running.TransitionTo (m_TransitionRestart);
            }
            else                    //show all tool buttons here
            {
                Time.timeScale = 0;
                paused = true;
                jumpCloudBtn.gameObject.SetActive(true);
                binaryStarBtn.gameObject.SetActive(true);
				audio_pause.TransitionTo (m_TransitionPause);
            }
        }
    }
    public void Died()
    {
        gameOverText.SetActive(true);
        isGameOver = true;
        Time.timeScale = 0;
		audio_died.TransitionTo (m_TransitionPause);

    }
}
