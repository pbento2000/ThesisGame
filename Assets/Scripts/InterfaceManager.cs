using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TMPro.TextMeshProUGUI comboText;
    [SerializeField] Color[] colors;
    private int fontSize = 20;

    float scoreFloat = 0f;
    private int comboMultiplier = 0;
    private float comboTimer = 1.5f;
    private float activeCombo = 0.0f;
    private bool isActiveCombo = false;

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

        if (activeCombo > 0.0f)
        {
            activeCombo -= Time.fixedDeltaTime;
            comboText.color -= new Color(0f, 0f, 0f, Time.fixedDeltaTime / comboTimer);
            //comboText.rectTransform.rotation = Quaternion.Lerp(comboText.rectTransform.rotation, Quaternion.identity, Time.fixedDeltaTime);
        }
        if (activeCombo < 0.0f && isActiveCombo)
        {
            comboMultiplier = 0;
            comboText.text = "";
            isActiveCombo = false;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        time.sizeDelta = new Vector2(timeMaskSize / 300f * timeSeconds, time.sizeDelta.y);
    }

    public void changeScore(float score)
    {
        
        if(score > 0)
        {
            isActiveCombo = true;
            activeCombo = comboTimer;
            comboMultiplier += 1;
            scoreFloat += score * comboMultiplier;
        }
        else
        {
            isActiveCombo = false;
            activeCombo = 0f;
            comboMultiplier = 0;
            scoreFloat += score;
            comboText.color = new Color(0f, 0f, 0f, 0f);
        }

        this.comboText.fontSize = fontSize + comboMultiplier;
        this.score.text = Mathf.CeilToInt(scoreFloat).ToString();

        if (comboMultiplier > 1)
        {
            comboText.text = comboMultiplier.ToString() + "x Combo";
            comboText.fontSize = 24f + (comboMultiplier / 2f);
            comboText.color = colors[0];
            comboText.rectTransform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-5f, 5f));
        }
        //progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x + score, progressBar.sizeDelta.y);
    }
}
