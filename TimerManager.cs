using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;

    private float timer;
    private bool timerRunning = true;
    public int coins = 0;

    void Update()
    {
        if (!timerRunning)
            return;

        timer += Time.deltaTime;

        timerText.text = timer.ToString("F2");
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log("Coins: " + coins);
    }
    public float GetTime()
    {
        return timer;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}
