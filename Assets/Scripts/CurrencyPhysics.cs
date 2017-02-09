using UnityEngine;
using System.Collections;

public class CurrencyPhysics : MonoBehaviour {
    
    Vector3 rotVelocity;
    float rotSpeed = 2;

    Vector2 velocity;
    float grav = 0.45f;

	// Use this for initialization
	void Start () {
        velocity = new Vector2((float)Random.Range(40, 150) / 10, (float)Random.Range(40, 150) / 10);
        rotVelocity = new Vector3(0, 0, (float)Random.Range(700, 2000) / 10);
        ChangeDirectionX((Random.Range(0,2) == 0)?1:-1);
        ChangeRotationZ((Random.Range(0, 2) == 0) ? 1 : -1);
    }
	
	// Update is called once per frame
	void Update () {
        
        ApplyRotation();        

        ApplyGravity();
        ApplyMovement();
    }
    

    //*********************MOVEMENT**************************
    void ApplyGravity() {
        velocity = new Vector2(velocity.x, velocity.y - grav);
    }
    void ApplyMovement() {
        Vector2 pos = transform.position;
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }
    void ChangeDirectionX(int _dir) {
        velocity.x = velocity.x * _dir;
    }

    //*********************MOVEMENT**************************
    void ApplyRotation() {
        Vector3 rot = transform.rotation.eulerAngles;
        transform.Rotate(rotVelocity * Time.deltaTime);
    }
    void ChangeRotationZ(int _dir) {
        rotVelocity.z = rotVelocity.z * _dir;
    }



}
