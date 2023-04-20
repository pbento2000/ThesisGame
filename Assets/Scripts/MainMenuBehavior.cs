using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    Storage storage;

    // Start is called before the first frame update
    void Start()
    {
        storage = GameObject.Find("Storage").GetComponent<Storage>();
        SettingsMenu.SetActive(false);

        for(int i = 1; i < 5; i++)
        {
            npcButtonsPositions[i-1] = npcButtons[i].transform.position;
        }

        int choice = UnityEngine.Random.Range(0, 4);

        for(int i = 1; i < 5; i++)
        {
            npcButtons[1 + (i + choice) % 4].transform.position = npcButtonsPositions[i-1];
        }

        TutorialMenu.SetActive(false);

        List<int> listOfDisabled = storage.getDisabledButtons();

        foreach(int n in listOfDisabled){
            npcButtons[n].GetComponent<Button>().interactable = false;
        }
    }

    public void setTypeOfNPC(int type)
    {
        storage.setTypeOfNPC(type);
        storage.addButtonToDisable(type+1);
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
        storage.addButtonToDisable(0);
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }
}