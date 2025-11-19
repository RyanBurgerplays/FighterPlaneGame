using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameManager gameManager;
    public GameObject explodeNow;
    private CircleCollider2D myCollider;
    private MeshRenderer myMesh;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myCollider = GetComponent<CircleCollider2D>();
        myMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 3f);
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.gameObject.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
             explodeNow= Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
            // Destroy(this.gameObject);
            myCollider.enabled = false;
            Invoke("DieTime", 0.1f);
        }
        else if (whatDidIHit.gameObject.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
             explodeNow= Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //Destroy(this.gameObject);
            myCollider.enabled=false;
            gameManager.AddScore(1);
            myMesh.enabled = false;
            Invoke("DieTime", 0.2f);
        }
    }
    private void DieTime() 
    {
        Destroy(explodeNow);
        Destroy(this.gameObject);
    }
}
