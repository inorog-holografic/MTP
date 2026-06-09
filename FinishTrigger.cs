using TMPro;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public TimerManager timerManager;
    public TMP_Text resultText;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
  

        if (collision.CompareTag("Player"))
        {
           
            timerManager.StopTimer();

            float time = timerManager.GetTime();

            string grade;

            if (time < 20)
                grade = "10";
            else if (time < 35)
                grade = "9";
            else if (time < 50)
                grade = "8";
            else
                grade = "7";

            resultText.gameObject.SetActive(true);

            resultText.text =
                "FINISH\n" +
                "TIME: " + time.ToString("F2") +
                "\nGRADE: " + grade +
                "\nCOINS: " + timerManager.coins;
        }
    }
}