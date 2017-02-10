using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public enum E_AUDIO_STATE {
        STOPPED,
        PAUSED,
        PLAYING
    }

    public AudioSource backgroundMusic;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
            backgroundMusic.Pause();
        if (Input.GetKeyDown(KeyCode.W))
            backgroundMusic.Play();
    }


    public void SetBackgroundState(E_AUDIO_STATE _state) {
        if (_state == E_AUDIO_STATE.PLAYING)
            backgroundMusic.Play();
        if (_state == E_AUDIO_STATE.PAUSED)
            backgroundMusic.Pause();
        if (_state == E_AUDIO_STATE.STOPPED)
            backgroundMusic.Stop();

    }


}
