using UnityEngine;
using System.Collections;

public class MouseWeapon : MonoBehaviour {

    public enum E_DIR {
        LEFT = -1,
        RIGHT = 1,
        NOT_MOVING = 0
    };

    [HideInInspector]
    private E_DIR lastFrameSide;
    private E_DIR currSide;

    private int power = 1;

    private Vector2 lastFrameMousePos;
    public bool isOnGUI = false;

    public Sprite GUICursorSprite;
    [HideInInspector]
    public Sprite gameCursorSprite;
    

    [HideInInspector]
    public PrefabManager.E_WEAPON cursorID;

    PrefabManager manager;

    // Use this for initialization
    void Start () {
        manager = FindObjectOfType<PrefabManager>();
        gameCursorSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        cursorID = PrefabManager.E_WEAPON.FINGER;
        Cursor.visible = false;
        lastFrameMousePos = Input.mousePosition;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isOnGUI)
            SetLastFrameMouseSide();

        HandleMouseFollowing();
        HandleRotation();

        lastFrameMousePos = Input.mousePosition;
        if (!isOnGUI)
            SetCurrentMouseSide();
    }

    //***********************SET FUNCTIONS************************* 
    public void SetPower(int _power) {
        power = _power;
    }
    public int GetPower() {
        return power;
    }
    
    public void SetLastFrameMouseSide() {
        float centerX = Camera.main.transform.position.x;
        float mouseX = transform.position.x;
        lastFrameSide = (mouseX > centerX) ? E_DIR.RIGHT : E_DIR.LEFT;
    }
    public void SetCurrentMouseSide() {
        float centerX = Camera.main.transform.position.x;
        float mouseX = transform.position.x;
        currSide = (mouseX > centerX) ? E_DIR.RIGHT : E_DIR.LEFT;
    }

    public void SetSprite(PrefabManager.E_WEAPON _weapon) {
        gameCursorSprite = manager.GetWeaponSprite(_weapon);         
    }

    //***********************GET FUNCTIONS*************************
    public Vector2 GetDeltaMouse() {
        return (Vector2)Input.mousePosition - lastFrameMousePos;
    } 

    public int GetMouseHitPower() {
        Vector2 mouseDelta = GetDeltaMouse();
        int x = (int)mouseDelta.x;
        int sign = (int)GetMovementDir();
        if      ((x > 0  && x < 15)      || (x < 0    && x > -15))  return 1 * sign;
        else if ((x >= 15 && x < 40)    || (x < -15  && x > -40))   return 2 * sign;
        else if ((x >= 40  && x < 90)   || (x < -40  && x > -90))   return 3 * sign;
        else if ((x >= 90  && x < 160)  || (x < -90  && x > -160))  return 4 * sign;
        else if ((x >= 160 && x < 300)  || (x < -160 && x > -300))  return 5 * sign;
        else                                                        return 0 * sign;
    }    

    public E_DIR GetMovementDir() {
        float deltaX = GetDeltaMouse().x;
        return (deltaX > 0) ? E_DIR.RIGHT : ((deltaX < 0) ? E_DIR.LEFT : E_DIR.NOT_MOVING);
    }

    public bool DidMouseChangeSide() {
        return (lastFrameSide != currSide);
    }

    //***********************HANDLE FUNCTIONS*************************
    void HandleMouseFollowing() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        gameObject.transform.position = new Vector2(mousePos.x, mousePos.y);
    }

    public void HandleRotation() {        
        float speed = Time.deltaTime * 5.5f;
        float powerMultiplier = 25;

        float currZRot = transform.eulerAngles.z;
        currZRot = (currZRot > 180) ? currZRot - 360 : currZRot;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(currZRot, GetMouseHitPower() * powerMultiplier, speed));
    }


    //**************************CURSOR SPRITE*****************************
    public void SetCursorMode(bool _isOnGUI) {
        if (_isOnGUI) {
            gameObject.GetComponent<SpriteRenderer>().sprite = GUICursorSprite;
            isOnGUI = true;
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().sprite = gameCursorSprite;
            isOnGUI = false;
        }
    }



    



}
