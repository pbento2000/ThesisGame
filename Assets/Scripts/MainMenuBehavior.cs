using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{

    [SerializeField]
    Tuple<int, string> info;

    [SerializeField]
    GameObject TutorialMenu;
    [SerializeField]
    GameObject SettingsMenu;

    [SerializeField]
    GameObject PlayButton;
    [SerializeField]
    GameObject SettingsButton;

    [SerializeField]
    GameObject[] npcButtons;
    Vector3[] npcButtonsPositions = new Vector3[4];

    [SerializeField]
    Storage storage;

    // Start is called before the first frame update
    void Start()
    {
        SettingsMenu.SetActive(false);

        for(int i = 0; i < 4; i++)
        {
            npcButtonsPositions[i] = npcButtons[i].transform.position;
        }

        int choice = UnityEngine.Random.Range(0, 4);

        for(int i = 0; i < 4; i++)
        {
            npcButtons[(i + choice) % 4].transform.position = npcButtonsPositions[i];
        }

        TutorialMenu.SetActive(false);
    }

    public void setTypeOfNPC(int type)
    {
        storage.setTypeOfNPC(type);
        Debug.Log(type);
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    public void Play(){
        PlayButton.SetActive(false);
        TutorialMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        SettingsButton.SetActive(false);
    }

    public void Back(){
        PlayButton.SetActive(true);
        SettingsButton.SetActive(true);
        TutorialMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }

    public void Settings(){
        PlayButton.SetActive(false);
        SettingsButton.SetActive(false);
        TutorialMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void LoadTutorial(){
        //Load Tutorial Scene
    }
}