using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

    public GameObject enemies;
    public GameObject MoveBoost;
    public GameObject AttackBoost;
    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;
    public int enemyCount;

    public Text scoreText;
    public int score;
    private int wave;

    public float bonusProjectileSpeed;
    public float bonusAttackSpeed;
    public float bonusMoveSpeed;

    public bool gameOverState;

    void Start() {
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        bonusProjectileSpeed = 0;
        score = 0;
        wave = 1;
        enemyCount = 10;
        UpdateScore();
        gameOverState = false;
        StartCoroutine(SpawnWaves()); 
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);

        Quaternion spawnRotation = new Quaternion();

        //10 Waves
        for (int j = 0; j < 19; j++){
            if (wave > 1) {
                Instantiate(MoveBoost, new Vector3(3.5f, Random.Range(3, -3), 0), spawnRotation);
                Instantiate(AttackBoost, new Vector3(-3.5f, Random.Range(3, -3), 0), spawnRotation);
            }
            //While there's still enemies
            while (enemyCount > 0) {
                //Spawn on each side, all at once cause we're evil
                for (int i = 0; i < j+1; i++) {
                    Vector3 spawnPositionRight = new Vector3(-1.5f, Random.Range(10, -10), 0);
                    Vector3 spawnPositionLeft = new Vector3(-21f, Random.Range(10, -10), 0);
                    
                    Instantiate(enemies, spawnPositionRight, spawnRotation);
                    Instantiate(enemies, spawnPositionLeft, spawnRotation);
                    enemyCount -= 2;
                }
                yield return new WaitForSeconds(spawnWait);
            }

            enemyCount = 10 + (int)Mathf.Pow(2f, (j + 1));
            spawnWait -= 0.15f;
            wave++;
            UpdateScore();
        }
    }

    void Update() {
        if (gameOverState) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene("Main");
            }
        }
    }

    void UpdateScore() {
        
        if (gameOverState) {
            scoreText.text = "Score: " + score + "\nWave: " + wave + "/20\n\n\nGame Over!\nPress 'R' to restart the game.";
        }

        else {
            scoreText.text = "Score: " + score + "\nWave: " + wave + "/20";
        }
    }

    public void AddScore() {
        score++;
        UpdateScore();
    }

    public void addProjectileSpeed(float addAmnt) {
        bonusProjectileSpeed += addAmnt;
    }

    public float getBonusProjectileSpeed() {
        return bonusProjectileSpeed;
    }

    public void addAttackSpeed(float addAmnt) {
        bonusAttackSpeed += addAmnt;
    }

    public float getBonusAttackSpeed() {
        return bonusAttackSpeed;
    }

    public void addMoveSpeed(float addAmnt) {
        bonusMoveSpeed += addAmnt;
    }

    public float getBonusMoveSpeed() {
        return bonusMoveSpeed;
    }

    public int getWave() {
        return wave;
    }

    public void gameOver() {
        StopCoroutine("SpawnWaves");
        gameOverState = true;
    }

    //SceneManager.LoadScene("Scene_Name");

}
