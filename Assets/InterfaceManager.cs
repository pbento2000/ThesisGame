using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;
    float scoreFloat = 0f;

    [SerializeField] WaveManager waveManager;

    float timeSeconds = 300f;
    float minutes;
    float seconds;
    float second = 1f;
    int secondsInt = 0;
    [SerializeField] RectTransform time;
    float timeMaskSize = 1650f;

    // Start is called before the first frame update
    void Start()
    {
        //progressBar.sizeDelta = new Vector2(0, progressBar.sizeDelta.y);
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
        time.sizeDelta = new Vector2(timeMaskSize / 300f * timeSeconds, time.sizeDelta.y);
    }

    public void changeScore(float score)
    {
        scoreFloat += score;
        this.score.text = Mathf.CeilToInt(scoreFloat).ToString();
        //progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x + score, progressBar.sizeDelta.y);
    }
}
