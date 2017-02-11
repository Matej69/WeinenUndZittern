using UnityEngine;
using System.Collections;

public class Puppet : MonoBehaviour {

    MouseWeapon mouseWeapon;
    float targetZRot;
    float rotForceMultiplier = 5;

    public GameObject Moustache;
    [HideInInspector]
    public Sprite startSprite;
    public Sprite moustacheSprite;
    public bool moustacheShaven = false;

    // Use this for initialization
    void Start () {
        mouseWeapon = FindObjectOfType<MouseWeapon>();

        startSprite = GetComponent<SpriteRenderer>().sprite;
        targetZRot = transform.eulerAngles.z;

    }
	
	// Update is called once per frame
	void Update () {
        if (mouseWeapon.DidMouseChangeSide())
            SetTargetZRot(mouseWeapon.GetMouseHitPower() * rotForceMultiplier);
        
        HandleRotation();
    }


    public void HandleRotation() {        
        float speed = Time.deltaTime * 5.5f;
        float powerMultiplier = 25;

        float currZRot = transform.eulerAngles.z;
        currZRot = (currZRot > 180) ? currZRot - 360 : currZRot;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(currZRot, targetZRot, speed));
    }

    public void SetTargetZRot(float _zRot) {
        targetZRot = -_zRot;
    }

    public void PuppetVisibility(bool _isVisible) {
        if (_isVisible) {
            GetComponent<SpriteRenderer>().sprite = startSprite;
            Moustache.GetComponent<SpriteRenderer>().sprite = moustacheSprite;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = null;
            Moustache.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    

}
