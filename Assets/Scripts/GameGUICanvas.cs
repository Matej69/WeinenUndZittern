using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameGUICanvas : MonoBehaviour {

    public Button Shop;
    public Button Sound;
    public Button Exit;
    public Text MoneyAmount;
    [Space]
    public Sprite muteSprite;
    public Sprite notmuteSprite;

    public GameObject ShopWindow;

    AudioManager audioManager;
    GameActionManager gameActionManager;

	// Use this for initialization
	void Start () {
        audioManager = FindObjectOfType<AudioManager>();
        gameActionManager = FindObjectOfType<GameActionManager>();

        SetListeners();
	

	}
	
	// Update is called once per frame
	void Update () {
        UpdateMoneyText();


    }


    void SetListeners() {
        Shop.onClick.AddListener(delegate {
            ShopWindow.active = true;
        });

        Sound.onClick.AddListener(delegate {
            Image soundSprite = Sound.gameObject.GetComponent<Image>();
            if (audioManager.backgroundMusic.isPlaying) { 
                audioManager.SetBackgroundState(AudioManager.E_AUDIO_STATE.PAUSED);
                soundSprite.sprite = muteSprite;
                }
            else { 
                audioManager.SetBackgroundState(AudioManager.E_AUDIO_STATE.PLAYING);
                soundSprite.sprite = notmuteSprite;
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
