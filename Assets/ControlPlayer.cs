using UnityEngine;
using System.Collections;

public class ControlPlayer : MonoBehaviour {

    private Rigidbody rb;
    private float agility = 0f;
    public float defaultAgility = 2.0f;
    public float angle = 0.0f;
    public GameObject Projectile;
    public Transform ShotSpawn;
    private float fireRate;
    private float defaultFireRate = 0.25f;
    public float bonusAttackSpeed;
    private float nextFire;
    public GameLogic gameLogic;


    void Start() {
        rb = GetComponent<Rigidbody>();

        //Associate with game logic
        GameObject gameLogicObject = GameObject.FindWithTag("GameController");
        if (gameLogicObject != null) {
            gameLogic = gameLogicObject.GetComponent<GameLogic>();
        }
    }

    void Update() {
        bonusAttackSpeed = gameLogic.getBonusAttackSpeed();
        fireRate = defaultFireRate - bonusAttackSpeed;
        agility = defaultAgility + gameLogic.getBonusMoveSpeed();

        if (Input.GetButton("Fire1") && Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Instantiate(Projectile, ShotSpawn.position, ShotSpawn.rotation);
        }
        
    }

    void FixedUpdate() {
        pointAtMouse();
        move();
    }

    //Makes the ship point at the user's cursor position
    void pointAtMouse() {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    //Responds to users arrow key inputs by moving in the inputted direction
    void move() {
        float movementX = 0.0f;
        float movementY = 0.0f;

        if (Input.GetAxis("Horizontal") != 0) {
            movementX = Input.GetAxis("Horizontal");
        }

        if (Input.GetAxis("Vertical") != 0) {
            movementY = Input.GetAxis("Vertical");
        }

        Vector3 movementVector = new Vector3(movementX, movementY, 0);
        rb.AddForce(movementVector * agility);

    }

    //void restrictPosition() {
    //    float minX, maxX, minY, maxY;

    //    rb.position = new Vector3(Mathf.Clamp(rb.position.x, minX, maxX), 0, 0f, Mathf.Clamp(rb.position.z, minY, maxY));

    //}
}

