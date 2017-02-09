using UnityEngine;
using System.Collections;

public class Currency : MonoBehaviour {

    public int value;

    float startTime = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        startTime -= Time.deltaTime;
        if (startTime < 0)
            Destroy(gameObject);

    }
}
