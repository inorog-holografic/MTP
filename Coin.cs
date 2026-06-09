using UnityEngine;

public class Coin : MonoBehaviour
{
    public TimerManager timerManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            timerManager.AddCoin();

            Debug.Log("Coin collected!");
            Destroy(gameObject);
        }
    }
}