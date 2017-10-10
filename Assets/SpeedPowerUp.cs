using UnityEngine;
using System.Collections;

public class SpeedPowerUp : MonoBehaviour {

    private Rigidbody rb;
    public GameLogic gameLogic;

    // Use this for initialization
    void Start () {
        //Associate with game logic
        GameObject gameLogicObject = GameObject.FindWithTag("GameController");
        if (gameLogicObject != null) {
            gameLogic = gameLogicObject.GetComponent<GameLogic>();
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.tag == "shot" || other.tag == "enemy") {
            return;
        }
        Debug.Log("SUPER SAIYANNN");
        gameLogic.addProjectileSpeed(25);
        gameLogic.addAttackSpeed(0.013f);
        Destroy(gameObject);

    }
}
