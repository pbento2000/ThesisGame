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
    GameObject PlayButton;

    [SerializeField]
    GameObject[] npcButtons;
    Vector3[] npcButtonsPositions = new Vector3[4];

    [SerializeField]
    Storage storage;

    // Start is called before the first frame update
    void Start()
    {
        TutorialMenu.SetActive(false);

        for(int i = 0; i < 4; i++)
        {
            npcButtonsPositions[i] = npcButtons[i].transform.position;
        }

        int choice = UnityEngine.Random.Range(0, 4);

        for(int i = 0; i < 4; i++)
        {
            npcButtons[(i + choice) % 4].transform.position = npcButtonsPositions[i];
        }
    }

    public void setTypeOfNPC(int type)
    {
        storage.setTypeOfNPC(type);
    }

    public void Play(){
        PlayButton.SetActive(false);

        for(int i = 0; i < 4; i++)
        {
            npcButtons[i].SetActive(false);
        }
        TutorialMenu.SetActive(true);
    }

    public void Back(){
        PlayButton.SetActive(true);

        for(int i = 0; i < 4; i++)
        {
            npcButtons[i].SetActive(true);
        }

        TutorialMenu.SetActive(false);
    }

    public void Yes(){
        //Load Tutorial Scene
    }

    public void No(){
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}
