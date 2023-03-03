using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI time;
    [SerializeField] WaveManager waveManager;
    float timeSeconds = 300f;
    float minutes;
    float seconds;
    float second = 1f;
    int secondsInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(seconds < 0f)
        {
            DisplayTime(timeSeconds);
            timeSeconds -= 1f;
            seconds = 1f;
            if(secondsInt % 30 == 0)
            {
                waveManager.startWave();
            }
            secondsInt += 1;
        }
        else
        {
            seconds -= Time.fixedDeltaTime;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
