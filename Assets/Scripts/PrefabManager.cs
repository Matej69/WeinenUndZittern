using UnityEngine;
using System.Collections;

public class PrefabManager : MonoBehaviour {

    [HideInInspector]
    public enum E_CURRENCY {
        COIN_GOLD,
        COIN_GREEN,
        COIN_ORANGE,
        COIN_RED,
        MONEY,
        INGOT,
        DIAMOND_GREEN,
        DIAMOND_RED,
        DIAMOND_YELLOW,
        SIZE
    }
    public GameObject[] currency;
    

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




}
