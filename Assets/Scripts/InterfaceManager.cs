using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] GameObject finalScore;
    [SerializeField] TMPro.TextMeshProUGUI comboText;
    [SerializeField] Color[] colors;
    private int fontSize = 20;

    [SerializeField] GameObject[] interfaceButtons;
    [SerializeField] Sprite effectIconHolder;
    bool playerEffectActive;
    bool npcEffectActive;
    [SerializeField] Image effectIcon;
    [SerializeField] Image effectIconNPC;
    [SerializeField] Image[] effectIcons;
    [SerializeField] GameObject[] effectButtons;
    public bool isMenuOpen;
    int effectChosen = -1;
    [SerializeField] RectTransform effectCooldown;
    [SerializeField] RectTransform aoeCooldown;
    [SerializeField] RectTransform effectCooldownNPC;
    [SerializeField] RectTransform aoeCooldownNPC;
    float effectCooldownTime;
    float aoeCooldownTime;
    float effectCooldownNPCTime;
    float aoeCooldownNPCTime;
    [SerializeField] Color playerColor;
    [SerializeField] Color npcColor;
    [SerializeField] Color white;
    [SerializeField] Color startTimeColor;
    [SerializeField] Color finishTimeColor;

    float scoreFloat = 0f;
    private int comboMultiplier = 0;
    private float comboTimer = 1.5f;
    private float activeCombo = 0.0f;
    private bool isActiveCombo = false;

    [SerializeField] WaveManager waveManager;
    [SerializeField] Camera cameraScene;

    float timeSeconds = 300f;
    float minutes;
    float seconds;
    float second = 1f;
    int secondsInt = 0;
    [SerializeField] RectTransform time;
    float timeMaskSize = 1280f;
    [SerializeField] GameObject[] minuteFrames;
    int frameDeleted = 0;
    Vector3 colorDelta;
    [SerializeField] GameObject timeSprite;

    public float timeScale = 1f;

    void Start() {
        timeScale = 1f;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        timeSprite.gameObject.GetComponent<Image>().color = startTimeColor;
        colorDelta = new Vector3((startTimeColor.r - finishTimeColor.r)/timeSeconds,(startTimeColor.g - finishTimeColor.g)/timeSeconds,(startTimeColor.b - finishTimeColor.b)/timeSeconds);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Time passing code

        if(seconds < 0f)
        {
            DisplayTime(timeSeconds);
            timeSeconds -= 1f;
            seconds = 1f;
            if(secondsInt % 30 == 0 && waveManager != null)
            {
                waveManager.startWave();
            }
            if(secondsInt % 60 == 0 && secondsInt > 0){
                timeScale += 0.1f;
                Time.timeScale = timeScale;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                StartCoroutine(fadeMinuteFrame(frameDeleted));
                frameDeleted += 1;
            }
            secondsInt += 1;
        }
        else
        {
            seconds -= Time.fixedDeltaTime;
        }

        //Combo Code

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

        //Game end Code

        if(secondsInt == 300){
            finalScore.SetActive(true);
            finalScore.GetComponent<TextMeshProUGUI>().text = "Score: " + score.text;
            timeScale = 0f;
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        //Effect Menu Code

        if(isMenuOpen){
            if(Mathf.Abs(Input.GetAxis("MoveWeaponHorizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("MoveWeaponVertical")) > 0.1f){
                Vector2 joystickPosition = new Vector2(Input.GetAxis("MoveWeaponHorizontal"), Input.GetAxis("MoveWeaponVertical"));
                
                float angle = Vector2.Angle(joystickPosition, new Vector2(1f, 0f));

                if(Input.GetAxis("MoveWeaponVertical") > 0f){
                    if(angle > 45f && angle < 135f && effectChosen != 2){
                        effectChosen = 2;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().color = new Color (0.1f,0.1f,0.1f,1f);
                    }
                    if(angle < 45f && effectChosen != 0){
                        effectChosen = 0;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().color = new Color (0.1f,0.1f,0.1f,1f);
                    }
                    if(angle > 135f && effectChosen != 1){
                        effectChosen = 1;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().color = new Color (0.1f,0.1f,0.1f,1f);
                    }
                }else{
                    if(angle > 45f && angle < 135f && effectChosen != 3){
                        effectChosen = 3;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().color = new Color (0.1f,0.1f,0.1f,1f);
                    }
                    if(angle < 45f && effectChosen != 0){
                        effectChosen = 0;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().color = new Color (0.1f,0.1f,0.1f,1f);
                    }
                    if(angle > 135f && effectChosen != 1){
                        effectChosen = 1;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().color = new Color (0.1f,0.1f,0.1f,1f);
                    }
                }
            }

            for(int i = 0; i < effectButtons.Length; i++){
                if(effectButtons[i].GetComponent<RectTransform>().localScale.x > 1f && i != effectChosen){
                    effectButtons[i].GetComponent<RectTransform>().localScale -= new Vector3(0.01f,0.01f,0f);
                }
            }

            if(effectChosen != -1){
                if(effectButtons[effectChosen].GetComponent<RectTransform>().localScale.x < 1.1f){
                    effectButtons[effectChosen].GetComponent<RectTransform>().localScale += new Vector3(0.01f,0.01f,0f);
                }
            }
        }

        if(effectCooldown.localScale.y > 0f){
            effectCooldown.localScale -= new Vector3(0f, Time.fixedDeltaTime/effectCooldownTime, 0f);
        }else if(playerEffectActive){
            effectIcon.sprite = effectIconHolder;
            playerEffectActive = false;
            effectIcon.color = playerColor;
        }

        if(aoeCooldown.localScale.y > 0f){
            aoeCooldown.localScale -= new Vector3(0f, Time.fixedDeltaTime/aoeCooldownTime, 0f);
        }

        if(effectCooldownNPC.localScale.y > 0f){
            effectCooldownNPC.localScale -= new Vector3(0f, Time.fixedDeltaTime/effectCooldownNPCTime, 0f);
        }else if(npcEffectActive){
            effectIconNPC.sprite = effectIconHolder;
            npcEffectActive = false;
            effectIconNPC.color = npcColor;
        }

        if(aoeCooldownNPC.localScale.y > 0f){
            aoeCooldownNPC.localScale -= new Vector3(0f, Time.fixedDeltaTime/aoeCooldownNPCTime, 0f);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if(time != null)
            time.sizeDelta = new Vector2(timeMaskSize / 300f * timeSeconds, time.sizeDelta.y);
        if(timeSprite != null){
            Color tmp = timeSprite.gameObject.GetComponent<Image>().color;
            tmp.r -= colorDelta[0];
            tmp.g -= colorDelta[1];
            tmp.b -= colorDelta[2];
            timeSprite.gameObject.GetComponent<Image>().color = tmp;
        }
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

    internal void openMenu()
    {
        for(int i = 0; i < effectButtons.Length; i++){
            effectButtons[i].SetActive(true);
        }
        isMenuOpen = true;
    }

    internal void closeMenu()
    {
        if(effectChosen != -1){
            effectIcon.sprite = effectIcons[effectChosen].sprite;
            effectIcon.color = white;
        }
        clearEffectChoice();
        for(int i = 0; i < effectButtons.Length; i++){
            effectButtons[i].SetActive(false);
        }
        isMenuOpen = false;
        effectChosen = -1;
    }

    void clearEffectChoice(){
        for(int i = 0; i < effectButtons.Length; i++){
            effectButtons[i].GetComponent<Image>().color = new Color (0f,0f,0f,1f);
        }
    }

    internal int getEffectChosen()
    {
        return effectChosen;
    }

    public void returnToMenu(){
        //Add code to store info
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void setNPCEffectIcon(int effectChosen){
            effectIconNPC.sprite = effectIcons[effectChosen].sprite;
            effectIconNPC.color = white;
    }

    public void setEffectCooldown(float cooldown){
        effectCooldownTime = cooldown;
        effectCooldown.localScale = new Vector3(1f,1f,1f);
        playerEffectActive = true;
        StartCoroutine(makeButtonAnimation(0));
    }

    public void setAoeCooldown(float cooldown){
        aoeCooldownTime = cooldown;
        aoeCooldown.localScale = new Vector3(1f,1f,1f);
        StartCoroutine(makeButtonAnimation(1));
    }

    public void setEffectNPCCooldown(float cooldown){
        effectCooldownNPCTime = cooldown;
        effectCooldownNPC.localScale = new Vector3(1f,1f,1f);
        npcEffectActive = true;
        StartCoroutine(makeButtonAnimation(2));
    }

    public void setAoeNPCCooldown(float cooldown){
        aoeCooldownNPCTime = cooldown;
        aoeCooldownNPC.localScale = new Vector3(1f,1f,1f);
        StartCoroutine(makeButtonAnimation(3));
    }

    public float getTimeScale()
    {
        return timeScale;
    }

    IEnumerator fadeMinuteFrame(int index){
        Image frame = minuteFrames[index].GetComponent<Image>();
        Color tmp = frame.color;
        tmp.a /= 2;
        frame.color = tmp;
        yield return null;
    }

    IEnumerator makeButtonAnimation(int index)
    {
        RectTransform transformButton = interfaceButtons[index].GetComponent<RectTransform>();
        yield return new WaitForFixedUpdate();
        transformButton.localScale = new Vector3(1.25f, 1.25f, 1f);
        float x = 1.25f;
        yield return new WaitForFixedUpdate();
        float downFloat = 0.005f;
        yield return new WaitForFixedUpdate();
        while (x > 1f)
        {
            transformButton.localScale -= new Vector3(downFloat, downFloat, 0f);
            x -= downFloat;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

/*
    IEnumerator fadeMinuteFrame(int index){
        Image frame = minuteFrames[index].GetComponent<Image>();
        Color tmp = frame.color;
        while(minuteFrames[index].transform.localScale.x < 1f){
            minuteFrames[index].transform.localScale += new Vector3(0.005f, 0.005f, 0f);
            tmp.a -= 1f/50f;
            minuteFrames[index].GetComponent<Image>().color = tmp;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }*/
}