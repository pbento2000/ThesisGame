using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TMPro.TextMeshProUGUI comboText;
    [SerializeField] Color[] colors;
    private int fontSize = 20;

    [SerializeField] GameObject[] effectButtons;
    bool isMenuOpen;
    int effectChosen = -1;
    [SerializeField] Sprite[] backgroundImages;
    [SerializeField] Sprite backgroundImgActive;
    [SerializeField] RectTransform effectCooldown;
    [SerializeField] RectTransform aoeCooldown;
    [SerializeField] RectTransform effectCooldownNPC;
    [SerializeField] RectTransform aoeCooldownNPC;
    float effectCooldownTime;
    float aoeCooldownTime;
    float effectCooldownNPCTime;
    float aoeCooldownNPCTime;

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

        if(isMenuOpen){
            if(Mathf.Abs(Input.GetAxis("MoveWeaponHorizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("MoveWeaponVertical")) > 0.1f){
                Vector2 joystickPosition = new Vector2(Input.GetAxis("MoveWeaponHorizontal"), Input.GetAxis("MoveWeaponVertical"));
                
                float angle = Vector2.Angle(joystickPosition, new Vector2(1f, 0f));

                if(Input.GetAxis("MoveWeaponVertical") > 0f){
                    if(angle > 45f && angle < 135f && effectChosen != 2){
                        effectChosen = 2;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().sprite = backgroundImgActive;
                    }
                    if(angle < 45f && effectChosen != 0){
                        effectChosen = 0;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().sprite = backgroundImgActive;
                    }
                    if(angle > 135f && effectChosen != 1){
                        effectChosen = 1;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().sprite = backgroundImgActive;
                    }
                }else{
                    if(angle > 45f && angle < 135f && effectChosen != 3){
                        effectChosen = 3;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().sprite = backgroundImgActive;
                    }
                    if(angle < 45f && effectChosen != 0){
                        effectChosen = 0;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().sprite = backgroundImgActive;
                    }
                    if(angle > 135f && effectChosen != 1){
                        effectChosen = 1;
                        clearEffectChoice();
                        effectButtons[effectChosen].GetComponent<Image>().sprite = backgroundImgActive;
                    }
                }
            }
        }

        if(effectCooldown.localScale.y > 0f){
            effectCooldown.localScale -= new Vector3(0f, Time.fixedDeltaTime/effectCooldownTime, 0f);
        }

        if(aoeCooldown.localScale.y > 0f){
            aoeCooldown.localScale -= new Vector3(0f, Time.fixedDeltaTime/aoeCooldownTime, 0f);
        }

        if(effectCooldownNPC.localScale.y > 0f){
            effectCooldownNPC.localScale -= new Vector3(0f, Time.fixedDeltaTime/effectCooldownNPCTime, 0f);
        }

        if(aoeCooldownNPC.localScale.y > 0f){
            aoeCooldownNPC.localScale -= new Vector3(0f, Time.fixedDeltaTime/aoeCooldownNPCTime, 0f);
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

    internal void openMenu()
    {
        for(int i = 0; i < effectButtons.Length; i++){
            effectButtons[i].SetActive(true);
        }
        isMenuOpen = true;
    }

    internal void closeMenu()
    {
        clearEffectChoice();
        for(int i = 0; i < effectButtons.Length; i++){
            effectButtons[i].SetActive(false);
        }
        isMenuOpen = false;
        effectChosen = -1;
    }

    internal int getEffectChosen()
    {
        return effectChosen;
    }

    void clearEffectChoice(){
        for(int i = 0; i < effectButtons.Length; i++){
            effectButtons[i].GetComponent<Image>().sprite = backgroundImages[i];
        }
    }

    public void setEffectCooldown(float cooldown){
        effectCooldownTime = cooldown;
        effectCooldown.localScale = new Vector3(1f,1f,1f);
    }

    public void setAoeCooldown(float cooldown){
        aoeCooldownTime = cooldown;
        aoeCooldown.localScale = new Vector3(1f,1f,1f);
    }

    public void setEffectNPCCooldown(float cooldown){
        effectCooldownNPCTime = cooldown;
        effectCooldownNPC.localScale = new Vector3(1f,1f,1f);
    }

    public void setAoeNPCCooldown(float cooldown){
        aoeCooldownNPCTime = cooldown;
        aoeCooldownNPC.localScale = new Vector3(1f,1f,1f);
    }
}