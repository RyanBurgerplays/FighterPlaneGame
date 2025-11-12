using UnityEngine;
using UnityEngine.UIElements;

public class EnemyTwo : MonoBehaviour
{
    public bool HitWall;
    public GameObject explodeNow;
    private CircleCollider2D myCollider;
    public GameObject explosionPrefab;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        HitWall = (Random.Range(0, 2) == 0);
        //Debug.Log(""+HitWall); //testing to make sure the randomize works
        myCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HitWall == true) { transform.Translate(new Vector3(5, -0.75f, 0) * Time.deltaTime * 3f); }
        if (HitWall == false) { transform.Translate(new Vector3(-5, -0.75f, 0) * Time.deltaTime * 3f); }
        if (transform.position.x > 8f) { HitWall = false; }
        if (transform.position.x < -8f) { HitWall = true; }
        
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
            explodeNow = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            // Destroy(this.gameObject);
            myCollider.enabled = false;
            Invoke("DieTime", 0.1f);
        }
        else if (whatDidIHit.gameObject.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            explodeNow = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //Destroy(this.gameObject);
            myCollider.enabled = false;
            gameManager.AddScore(2);
            Invoke("DieTime", 0.2f);
        }
    }
    private void DieTime()
    {
        Destroy(explodeNow);
        Destroy(this.gameObject);
    }
}
