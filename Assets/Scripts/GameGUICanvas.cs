using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameGUICanvas : MonoBehaviour {

    public Button Shop;
    public Button Music;
    public Button Sound;
    public Button Exit;   
    public Text MoneyAmount;

    [Space]
    public Sprite muteMusicSprite;
    public Sprite notMuteMusicSprite;
    [Space]
    public Sprite muteSoundSprite;
    public Sprite notMuteSoundSprite;

    public GameObject ShopWindow;

    AudioManager audioManager;
    GameActionManager gameActionManager;
    Puppet puppet;

	// Use this for initialization
	void Start () {
        audioManager = FindObjectOfType<AudioManager>();
        gameActionManager = FindObjectOfType<GameActionManager>();
        puppet = FindObjectOfType<Puppet>();

        SetListeners();
	

	}
	
	// Update is called once per frame
	void Update () {
        UpdateMoneyText();


    }


    void SetListeners() {
        Shop.onClick.AddListener(delegate {
            ShopWindow.active = true;
            puppet.PuppetVisibility(false);
        });

        Music.onClick.AddListener(delegate {
            Image musicSprite = Music.gameObject.GetComponent<Image>();
            if (audioManager.backgroundMusic.isPlaying) { 
                audioManager.SetBackgroundState(AudioManager.E_AUDIO_STATE.PAUSED);
                musicSprite.sprite = muteMusicSprite;
                }
            else { 
                audioManager.SetBackgroundState(AudioManager.E_AUDIO_STATE.PLAYING);
                musicSprite.sprite = notMuteMusicSprite;
            }
        });

        Sound.onClick.AddListener(delegate {
            Image soundSprite = Sound.gameObject.GetComponent<Image>();
            if (audioManager.soundsMuted)
            {
                audioManager.SetSlapVolumeState(false);
                soundSprite.sprite = notMuteSoundSprite;
            }
            else {
                audioManager.SetSlapVolumeState(true);
                soundSprite.sprite = muteSoundSprite;
            }
        });

        Exit.onClick.AddListener(delegate {
            Application.Quit();
        });
    }

    void UpdateMoneyText() {
        string amountString = gameActionManager.money.ToString();
        MoneyAmount.text = "Money : " + amountString;
    }
    



}
