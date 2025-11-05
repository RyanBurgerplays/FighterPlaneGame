using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float verticalScreenSize = 5f; 
    public float horizontalScreenSize = 6f;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public int score;
    public int pointsToAdd;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 3, 6);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }
    void CreateEnemyTwo() { Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity); }

    public void AddScore(int pointsToAdd) 
    {
        score += pointsToAdd;
    }
}
