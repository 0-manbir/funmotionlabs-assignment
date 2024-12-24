using TMPro;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    [SerializeField] TMP_Text stopwatchText;

    float time;
    string formattedTime;

    bool isRunning = false;

    void Start()
    {
        stopwatchText.text = "";
    }

    void Update()
    {
        if (isRunning)
        {
            time += Time.deltaTime;

            formattedTime = FormatTime(time);
            stopwatchText.text = formattedTime;
        }
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt(time * 1000f % 1000f);

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}