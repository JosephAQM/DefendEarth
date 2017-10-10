using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    private Rigidbody rb;
    public float defaultSpeed;
    private float speed;
    public float angle;
    public GameLogic gameLogic;

    void Start() {
        rb = GetComponent<Rigidbody>();
        pointAtMouse();
        defaultSpeed = 120;
        GameObject gameLogicObject = GameObject.FindWithTag("GameController");
        if (gameLogicObject != null) {
            gameLogic = gameLogicObject.GetComponent<GameLogic>();
        }

        speed = gameLogic.getBonusProjectileSpeed() + defaultSpeed;
        rb.AddForce(transform.up * speed);
    }

    void Update() {       
        Clean();
    }

    void pointAtMouse() {
          transform.rotation = GameObject.Find("goodShip").transform.rotation;
    }

    void Clean() {
        if (transform.position.x > 10 || transform.position.y > 15 || transform.position.x < -25 || transform.position.y < -15) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.tag == "earth") {
            Destroy(gameObject);
        }
    }
}
