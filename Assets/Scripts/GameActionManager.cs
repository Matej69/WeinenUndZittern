using UnityEngine;
using System.Collections;

public class GameActionManager : MonoBehaviour {

    PrefabManager prefabManager;
    MouseWeapon mouseWeapon;

	// Use this for initialization
	void Start () {
        prefabManager = FindObjectOfType<PrefabManager>();
        mouseWeapon = FindObjectOfType<MouseWeapon>();

    }
	
	// Update is called once per frame
	void Update () {

        FingerSlap();

    }

    void FingerSlap() {
        if (!mouseWeapon.DidMouseChangeSide())
            return;

        float centerX = Camera.main.transform.position.x;
        float mouseX = mouseWeapon.transform.position.x;
        if (mouseWeapon.GetMouseHitPower() != 0)
            SpawnCurrency(mouseWeapon.GetPower(), mouseWeapon.GetMouseHitPower());

    }
    void SpawnCurrency(int _mousePower, int _getHitPower) {
        for (int i = 0; i < (int)PrefabManager.E_CURRENCY.SIZE - 1; ++i) {
            if (_mousePower == i)
                return;
            else {
                int numOfSpawnings = Random.Range(1, Mathf.Abs(_getHitPower));
                for(int j = 0; j < numOfSpawnings; ++j)
                    Instantiate(prefabManager.GetCurrencyPrefab((PrefabManager.E_CURRENCY)i), mouseWeapon.transform.position, Quaternion.identity);
            }
        }                
    }




}
