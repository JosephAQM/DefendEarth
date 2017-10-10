using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {
    private Rigidbody rb;
    public GameObject explosion;
    public float defaultSpeed;
    public float bonusSpeed;
    public GameLogic gameLogic;

    void Start() {
        //RIGID DA BODAY
        rb = GetComponent<Rigidbody>();
        
        //Associate with game logic
        GameObject gameLogicObject = GameObject.FindWithTag("GameController");
        if (gameLogicObject != null) {
            gameLogic = gameLogicObject.GetComponent<GameLogic>();
        }


        bonusSpeed = gameLogic.getWave() * 2;

        //Face earth
        Vector3 vectorToTarget = GameObject.Find("earth").transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 9000);

        Move();
    }

    //Propels the projectile to its destination!
    void Move() {
        rb.AddForce(transform.up * (defaultSpeed + bonusSpeed));
    }

    //this.... OH, to prevent it from moving along z axis.... not sure if necessary anymore
    void Update() {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    //Triggers upon collision, explodes, kills stuff, increments score
    void OnTriggerEnter(Collider other) {

        if (other.tag == "powerup") {
            return;
        }
        else if (other.tag == "earth") {
            Destroy(gameObject);
            gameLogic.gameOver();
        }
        else if (other.tag == "Player") {
            gameLogic.gameOver();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        //Debug.Log("ded");
        gameLogic.AddScore();
        Instantiate(explosion, transform.position, transform.rotation);
        

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
