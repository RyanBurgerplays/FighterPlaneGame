using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //how to define a variable
    //1. access modifier: public or private
    //2. data type: int, float, bool, string
    //3. variable name: camelCase
    //4. value: optional

    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;
    //private float verticalScreenLimit = 6.5f;

    public GameObject bulletPrefab;
    public int lives = 3;
    private int weaponType;
    private float speed;
    private GameManager gameManager;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;
    public bool hasShield;
    void Start()
    {
        playerSpeed = 6f;
        //This function is called at the start of the game
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        weaponType = 1;
        gameManager.ChangeLivesText(lives);
        hasShield = false;
    }

    void Update()
    {
        //This function is called every frame; 60 frames/second
        Movement();
        Shooting();

    }

    void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        //Read the input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);
        //Player leaves the screen horizontally
        if(transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        //Player leaves the screen vertically
        // if(transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        // {
        //     transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        // }
        if (transform.position.y > 0) { transform.position = new Vector3(transform.position.x, 0, 0); }
        if (transform.position.y < -3.2) {transform.position = new Vector3(transform.position.x, -3.2f, 0); }
    }
    public void LoseALife()
    {
        if (hasShield == false)
        {
            lives--;
        }
        else {
            shieldPrefab.SetActive(false);
            hasShield = false; }
            gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            gameManager.GameOver();

        }
    }
    IEnumerator SpeedPowerDown() {
        yield return new WaitForSeconds(3f);
        playerSpeed = 6f;
        thrusterPrefab.SetActive(false);

    }
    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
        //??? // what the fuck do i put here 
    }
    IEnumerator ShieldGot()
    {
        shieldPrefab.SetActive(true);
        hasShield = true;
        yield return new WaitForSeconds(3f);
        gameManager.ManagePowerupText(0);

    }
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.gameObject.tag =="Powerup") //remember to give the tag powerup to the poweupwhen and make it kinimatic
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 4);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    StartCoroutine(ShieldGot());
                    gameManager.ManagePowerupText(4);
                    break;
                case 2:
                    weaponType = 2;
                    StartCoroutine (WeaponPowerDown());
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    weaponType = 3;
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(3);
                    break;
                case 4:
                    playerSpeed = 10f;
                    StartCoroutine(SpeedPowerDown());
                    thrusterPrefab.SetActive(true);
                    gameManager.ManagePowerupText(1);
                    break;
            }
        }
        if (whatDidIHit.gameObject.tag == "Coin") {
            gameManager.AddScore(1);
            Destroy(whatDidIHit.gameObject);

        }
    }
}
