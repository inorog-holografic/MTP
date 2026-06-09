using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private void Start()
    {
        
    }

    public float delay = 0.001f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Player"))
        {
            
            Invoke(nameof(HidePlatform), delay);
        }
    }

    void HidePlatform()
    {
        gameObject.SetActive(false);
    }
}