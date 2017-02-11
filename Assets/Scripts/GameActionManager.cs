using UnityEngine;
using System.Collections;

public class GameActionManager : MonoBehaviour {
    [HideInInspector]
    public int money = 0;
    [HideInInspector]
    public Sprite UpgradeSprite;

    public Transform SlapPoint;
    [Space]
    public GameObject CreditsPrefab;
    private bool creditsSpawned = false;

    PrefabManager prefabManager;
    MouseWeapon mouseWeapon;
    AudioManager audioManager;
    Puppet puppet;
    GameGUICanvas gameGUI;
    Shop shop;

    public Animator PuppetAnimator;
    public Animator CameraAnimator;
    private bool zoomingAnimEnded = false;

    Timer spawnCreditsTimer;

    // Use this for initialization
    void Start () {
        prefabManager = FindObjectOfType<PrefabManager>();
        mouseWeapon = FindObjectOfType<MouseWeapon>();
        audioManager = FindObjectOfType<AudioManager>();
        puppet = FindObjectOfType<Puppet>();
        gameGUI = FindObjectOfType<GameGUICanvas>();
        shop = FindObjectOfType<Shop>();

        spawnCreditsTimer = new Timer(4f);
        UpgradeSprite = prefabManager.GetWeaponSprite(PrefabManager.E_WEAPON.GLOVE);
    }
	
	// Update is called once per frame
	void Update () {

       if (mouseWeapon.DidMouseChangeSide() && !puppet.moustacheShaven)
           FingerSlap();
       
        if (mouseWeapon.DidMouseChangeSide() && !puppet.moustacheShaven && mouseWeapon.cursorID == PrefabManager.E_WEAPON.CHAINSAW) {
            puppet.moustacheShaven = true;
            gameGUI.gameObject.SetActive(false);
        }
        
        HandleEndGameAnimations();
    }

    void FingerSlap() {
        if (mouseWeapon.GetMouseHitPower() != 0)
            SpawnCurrency(mouseWeapon.GetPower(), mouseWeapon.GetMouseHitPower());
    }
    void SpawnCurrency(int _mousePower, int _getHitPower) {
        if (FindObjectOfType<Shop>() != null)
            return;

        audioManager.SpawnSlapSource();

        for (int i = 0; i < (int)PrefabManager.E_CURRENCY.SIZE - 1; ++i) {
            if (_mousePower == i)
                break;
            else {
                int numOfSpawnings = Random.Range(1, Mathf.Abs(_getHitPower));
                for(int j = 0; j < numOfSpawnings; ++j) {
                    GameObject currencyPrefab = prefabManager.GetCurrencyPrefab((PrefabManager.E_CURRENCY)i);
                    float prefabPosZ = currencyPrefab.transform.position.z;
                    Instantiate(currencyPrefab, new Vector3(SlapPoint.position.x, SlapPoint.position.y, prefabPosZ), Quaternion.identity);
                    AddMoney(currencyPrefab.GetComponent<Currency>().value);
                }
            }
        }

        //if its axe, spawn red and yellow gem as well
        if (mouseWeapon.cursorID == PrefabManager.E_WEAPON.AXE) {
            Debug.Log("spawning diamonds");
            GameObject extraDiamondPrefab = prefabManager.GetCurrencyPrefab(Random.Range(0, 2) == 0 ? PrefabManager.E_CURRENCY.DIAMOND_RED : PrefabManager.E_CURRENCY.DIAMOND_YELLOW);
            float prefabPosZ = extraDiamondPrefab.transform.position.z;
            Instantiate(extraDiamondPrefab, new Vector3(SlapPoint.position.x, SlapPoint.position.y, prefabPosZ), Quaternion.identity);
            AddMoney(extraDiamondPrefab.GetComponent<Currency>().value);
        }
    }

    public void AddMoney(int _amount) {
        money += _amount;        
    }

    public void HandleEndGameAnimations() {
        if (!puppet.moustacheShaven)
            return;
        //start zooming camera to puppet
        if(mouseWeapon.cursorID == PrefabManager.E_WEAPON.CHAINSAW) {            
            puppet.SetTargetZRot(0f);
            CameraAnimator.SetBool("GameEnded", true);
            PuppetAnimator.SetBool("MustacheShaved", true);
            //when camera zoomed enough, start crying
            if (Camera.main.orthographicSize < 2.5f) {
                mouseWeapon.GetComponent<SpriteRenderer>().sprite = null;
                PuppetAnimator.SetBool("GameEnded", true);
                spawnCreditsTimer.Tick(Time.deltaTime);
                //after crying for X seconds, spawn credits
                if (spawnCreditsTimer.IsFinished() && !creditsSpawned) { 
                    Instantiate(CreditsPrefab);
                    creditsSpawned = true;
                }
            }                
            
        }               
    }
    




}
