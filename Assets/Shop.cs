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
    Puppet puppet;

    // Use this for initialization
    void Start () {
        mouseWeapon = FindObjectOfType<MouseWeapon>();
        prefabManager = FindObjectOfType<PrefabManager>();
        gameActionManager = FindObjectOfType<GameActionManager>();
        puppet = FindObjectOfType<Puppet>();

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
            //automatically close shop after buying
            gameObject.active = false;
            mouseWeapon.SetCursorMode(false);
            puppet.PuppetVisibility(true);
        });

        Exit.onClick.AddListener(delegate {
            gameObject.active = false;
            mouseWeapon.SetCursorMode(false);
            puppet.PuppetVisibility(true);
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
            case PrefabManager.E_WEAPON.GLOVE   :   return 150;     break;
            case PrefabManager.E_WEAPON.BONE    :   return 1000;    break;
            case PrefabManager.E_WEAPON.BAT     :   return 4200;    break;
            case PrefabManager.E_WEAPON.HAMMER  :   return 12000;   break;
            case PrefabManager.E_WEAPON.KNIFE   :   return 40000;   break;
            case PrefabManager.E_WEAPON.AXE     :   return 150000;  break;
            case PrefabManager.E_WEAPON.CHAINSAW:   return 1000000; break;
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
        return gameActionManager.money >= GetWeaponPrice(mouseWeapon.cursorID + 1);
    }


}
