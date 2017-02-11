using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public enum E_AUDIO_STATE {
        STOPPED,
        PAUSED,
        PLAYING
    }

    public AudioSource backgroundMusic;
    [Space]
    public GameObject SlapSourcePrefab;
    [Space]
    public AudioClip sadBackgroundMusic;

    [HideInInspector]
    public bool soundsMuted = false;

    private List<GameObject> existingSlapSources = new List<GameObject>();
    private int maxSlapNum = 6;
    private Timer slapSpawnTimer;
    private bool sadMusicStarted = false;

    MouseWeapon mouseWeapon;
    Puppet puppet;

    // Use this for initialization
    void Start () {
        mouseWeapon = FindObjectOfType<MouseWeapon>();
        puppet = FindObjectOfType<Puppet>();

        slapSpawnTimer = new Timer(0.23f);
    }
	
	// Update is called once per frame
	void Update () {
        slapSpawnTimer.Tick(Time.deltaTime);
        RemoveFinishedSlapClips();
        HandleEndGameMusicChange();
    }


    //********************BACKGROUND MUSIC*************************
    public void SetBackgroundState(E_AUDIO_STATE _state) {
        if (_state == E_AUDIO_STATE.PLAYING)
            backgroundMusic.Play();
        if (_state == E_AUDIO_STATE.PAUSED)
            backgroundMusic.Pause();
        if (_state == E_AUDIO_STATE.STOPPED)
            backgroundMusic.Stop();
    }

    //********************SLAP SOUNDS*************************
    public void SpawnSlapSource() {
        if (existingSlapSources.Count > maxSlapNum || !slapSpawnTimer.IsFinished())
            return;

        float newPitch = (float)Random.Range(870,  1200) / 1000;
        SlapSourcePrefab.GetComponent<AudioSource>().pitch = newPitch;
        GameObject sourceObj = (GameObject)Instantiate(SlapSourcePrefab);
             
        existingSlapSources.Add(sourceObj);
        slapSpawnTimer.Reset();
    }

    public void RemoveFinishedSlapClips() {
        for (int i = existingSlapSources.Count - 1; i >= 0; --i)
            if (!existingSlapSources[i].GetComponent<AudioSource>().isPlaying) {
                Destroy(existingSlapSources[i]);
                existingSlapSources.RemoveAt(i);
            }
    }

    public void SetSlapVolumeState(bool _muted) {
        if (_muted)
            soundsMuted = true;
        else
            soundsMuted = false; 
        SlapSourcePrefab.GetComponent<AudioSource>().volume = (soundsMuted) ? 0 : 0.45f;
    }

    public void HandleEndGameMusicChange() {
        if (!puppet.moustacheShaven)
            return;

        if(mouseWeapon.cursorID == PrefabManager.E_WEAPON.CHAINSAW) { 
            if(!sadMusicStarted)
                backgroundMusic.volume -= Time.deltaTime * 0.3f;
            if(backgroundMusic.volume <= 0) {
                backgroundMusic.clip = sadBackgroundMusic;
                backgroundMusic.volume = 1;
                backgroundMusic.Play();
                sadMusicStarted = true;
            }
        }

    }



}
