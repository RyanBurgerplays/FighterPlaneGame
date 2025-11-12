using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public float verticalScreenSize = 5f; 
    public float horizontalScreenSize = 6f;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject coinPrefab;
    public int score;
    public int pointsToAdd;
    public GameObject playerPrefab;
    public GameObject cloudPrefab;
    public GameObject powerupPrefab;
    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject audioPlayer;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpText;
    public int cloudMove;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 3, 6);
        InvokeRepeating("CreateCoin", 5, 10);
        score = 0;
        AddScore(0);
        cloudMove = 1;
        gameOver = false;
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        powerUpText.text = "No Powers yet!";
        StartCoroutine(SpawnPowerup());
        //currentLives = 3;
    }
    IEnumerator SpawnPowerup() 
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }
    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-7.2f, 7.2f), Random.Range(0f, -3.2f), 0), Quaternion.identity);

    }

    public int currentLives;
    // Update is called once per frame
    void Update()
    {
       if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }
    void CreateEnemyTwo() { Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity); }
    void CreateCoin() { Instantiate(coinPrefab, new Vector3(Random.Range(-9f, 9f), Random.Range(0f, -3.2f), 0), Quaternion.identity) ; }

    public void AddScore(int pointsToAdd) 
    {
        score += pointsToAdd;
        scoreText.text = "Score:" + score;
    }
    void CreateSky()
    {
        for (int i =0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
            // ryan change this shit to the right numbers
        }
    }

    public void ManagePowerupText(int powerupType)
    {
        switch(powerupType)
        {
            case 1:
                powerUpText.text = "Speed!";
                break;
            case 2:
                powerUpText.text = "Double Weapon!";
                break;
            case 3:
                powerUpText.text = "Triple Weapon!";
                break;
            case 4:
                powerUpText.text = "Shield!";
                break;
            default:
                powerUpText.text = "No Powers yet!";
                break;
        }
    }
    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                this.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
                break;
            case 2:
                this.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;
        }
        
    }
    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives:" + currentLives;
    }
    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartText.SetActive(true);
        gameOver = true;
        CancelInvoke();
        cloudMove = 0;
    }
}
