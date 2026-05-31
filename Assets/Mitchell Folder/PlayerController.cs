using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameOverScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            gameOverScreen.SetActive(true);
        }
    }
}
