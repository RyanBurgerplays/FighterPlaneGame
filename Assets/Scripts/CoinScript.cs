using UnityEngine;

public class CoinScript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("TooLong", 3f);
    }
    private void TooLong() 
    {
        Destroy(this.gameObject);
    }
}
