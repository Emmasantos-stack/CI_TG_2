using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private TMP_Text timerText;
    private float currentTimer;
    private bool isCounting;

    void Start()
    {
        timerText = GetComponent<TMP_Text>();
        currentTimer = 0;
        isCounting = true;
    }

    void Update()
    {
        if (!isCounting)
        {
            return;
        }

        currentTimer += Time.deltaTime;

        float seconds = Mathf.FloorToInt(currentTimer % 60);

        timerText.text = $"{seconds:00}";
    }

    public int GetTimerAndStop()
    {
        isCounting = false;
        return (int)currentTimer;
    }
}