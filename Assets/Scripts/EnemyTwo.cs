using UnityEngine;
using UnityEngine.UIElements;

public class EnemyTwo : MonoBehaviour
{
    public bool HitWall;
    
    // Start is called before the first frame update
    void Start()
    {
        
        HitWall = (Random.Range(0, 2) == 0);
        //Debug.Log(""+HitWall); //testing to make sure the randomize works
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
}
