using UnityEngine;
using System.Collections;

public class PrefabManager : MonoBehaviour {

    [HideInInspector]
    public enum E_CURRENCY {
        COIN_RED,
        COIN_GREEN,
        COIN_ORANGE,
        COIN_GOLD,
        MONEY,
        INGOT,
        DIAMOND_GREEN,
        DIAMOND_RED,
        DIAMOND_YELLOW,
        SIZE
    }
    public GameObject[] currency;

    [HideInInspector]
    public enum E_WEAPON {
        FINGER,
        GLOVE,
        BONE,
        BAT,
        HAMMER,
        KNIFE,
        AXE,
        CHAINSAW,
        SIZE
    }
    public Sprite[] weapons;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject GetCurrencyPrefab(E_CURRENCY _type) {     
        foreach (GameObject curr in currency)
            if (curr.GetComponent<CurrencyEnumType>().type == _type)
                return curr;
        Debug.Log("ERROR : Cant get any currency objects");
        return null;
    }

    public Sprite GetWeaponSprite(E_WEAPON _weapon) {
        for (int i = 0; i < weapons.Length; ++i)
            if (i == (int)_weapon)
                return weapons[i];
        Debug.Log("ERROR : Cant get any weapon objects");
        return null;
    }




}
