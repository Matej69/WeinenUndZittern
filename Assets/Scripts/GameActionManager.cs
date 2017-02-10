using UnityEngine;
using System.Collections;

public class GameActionManager : MonoBehaviour {
    [HideInInspector]
    public int money = 0;
    [HideInInspector]
    public Sprite UpgradeSprite;

    PrefabManager prefabManager;
    MouseWeapon mouseWeapon;

	// Use this for initialization
	void Start () {
        prefabManager = FindObjectOfType<PrefabManager>();
        mouseWeapon = FindObjectOfType<MouseWeapon>();

        UpgradeSprite = prefabManager.GetWeaponSprite(PrefabManager.E_WEAPON.GLOVE);
    }
	
	// Update is called once per frame
	void Update () {

       if (mouseWeapon.DidMouseChangeSide())
           FingerSlap();       

    }

    void FingerSlap() {
        if (mouseWeapon.GetMouseHitPower() != 0)
            SpawnCurrency(mouseWeapon.GetPower(), mouseWeapon.GetMouseHitPower());
    }
    void SpawnCurrency(int _mousePower, int _getHitPower) {
        for (int i = 0; i < (int)PrefabManager.E_CURRENCY.SIZE - 1; ++i) {
            if (_mousePower == i)
                return;
            else {
                int numOfSpawnings = Random.Range(1, Mathf.Abs(_getHitPower));
                for(int j = 0; j < numOfSpawnings; ++j) {
                    GameObject currencyPrefab = prefabManager.GetCurrencyPrefab((PrefabManager.E_CURRENCY)i);
                    Instantiate(currencyPrefab, Camera.main.transform.position/2, Quaternion.identity);
                    AddMoney(currencyPrefab.GetComponent<Currency>().value);
                }
            }
        }                
    }

    public void AddMoney(int _amount) {
        money += _amount;        
    }
    




}
