using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public Sprite upgradeActiveSprite;
    public Sprite upgradeInactiveSprite;

    public Button Upgrade;
    public Button Exit;
    public Image CurrentWeapon;
    public Image NextWeapon;
    public Text AmountForUpgrade;

    MouseWeapon mouseWeapon;
    PrefabManager prefabManager;
    GameActionManager gameActionManager;

    // Use this for initialization
    void Start () {
        mouseWeapon = FindObjectOfType<MouseWeapon>();
        prefabManager = FindObjectOfType<PrefabManager>();
        gameActionManager = FindObjectOfType<GameActionManager>();

        SetListeners();
    }
	
	// Update is called once per frame
	void Update () {
        if (mouseWeapon.cursorID != PrefabManager.E_WEAPON.CHAINSAW)
            UpdateShop();


    }


    void SetListeners() {
        Upgrade.onClick.AddListener(delegate {
            if (!HasEnoughMoney() || mouseWeapon.cursorID == PrefabManager.E_WEAPON.CHAINSAW)
                return;
            gameActionManager.money -= GetWeaponPrice(mouseWeapon.cursorID + 1);
            mouseWeapon.SetSprite(++mouseWeapon.cursorID);
            mouseWeapon.SetPower(mouseWeapon.GetPower() + 1);
                 
        });

        Exit.onClick.AddListener(delegate {
            gameObject.active = false;
            mouseWeapon.SetCursorMode(false);
        });

    }

    void SetUpgradeButtonSprite(bool _canUpgrade) {
        if (_canUpgrade)
            Upgrade.gameObject.GetComponent<Image>().sprite = upgradeActiveSprite;
        else
            Upgrade.gameObject.GetComponent<Image>().sprite = upgradeInactiveSprite;
    }

    public int GetWeaponPrice(PrefabManager.E_WEAPON _weapon) {
        switch (_weapon) {
            case PrefabManager.E_WEAPON.GLOVE   :   return 70;      break;
            case PrefabManager.E_WEAPON.BONE    :   return 500;     break;
            case PrefabManager.E_WEAPON.BAT     :   return 2200;    break;
            case PrefabManager.E_WEAPON.HAMMER  :   return 9000;    break;
            case PrefabManager.E_WEAPON.KNIFE   :   return 50000;   break;
            case PrefabManager.E_WEAPON.AXE     :   return 200000;   break;
            case PrefabManager.E_WEAPON.CHAINSAW:   return 2000000; break;
            default                             :   return 0;
        }
    }

    public void UpdateShop() {
        CurrentWeapon.sprite = mouseWeapon.gameCursorSprite;
        NextWeapon.sprite = prefabManager.GetWeaponSprite(mouseWeapon.cursorID + 1);
        Upgrade.gameObject.GetComponent<Image>().sprite = (HasEnoughMoney()) ? upgradeActiveSprite : upgradeInactiveSprite;
        AmountForUpgrade.text = GetWeaponPrice(mouseWeapon.cursorID + 1).ToString();
    }

    public bool HasEnoughMoney() {
        return gameActionManager.money > GetWeaponPrice(mouseWeapon.cursorID + 1);
    }


}
