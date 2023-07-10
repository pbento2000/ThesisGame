using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Storage : MonoBehaviour
{

    int typeOfNPC;
    [SerializeField] List<string> disabledButtons;
    public bool alreadyChose;
    public int choiceInt;
    List<InputEntry> entries;
    int studyId;
    InputEntry currentEntry;
    string filename;
    FileLogManager fileManager = new FileLogManager();

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Storage");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        typeOfNPC = Random.Range(0, 4);
        filename = this.gameObject.GetComponent<InputHandler>().getFilename();
        entries = this.gameObject.GetComponent<InputHandler>().getEntries();
        if(entries.Count == 0){
            studyId = 0;
        }else{
            studyId = entries[entries.Count-1].studyId + 1;
        }
    }

    public void setTypeOfNPC(int type)
    {
        Debug.Log(type);
        typeOfNPC = type;

        currentEntry = new InputEntry(studyId);
    }

    public int getTypeOfNPC()
    {
        return typeOfNPC;
    }

    public List<string> getDisabledButtons(){
        return disabledButtons;
    }

    public void addButtonToDisable(string btn){
        disabledButtons.Add(btn);
    }

    public void cleanList(){
        disabledButtons.Clear();
    }

    internal void saveInfo()
    {
        entries.Add(currentEntry);
        FileHandler.SaveToJSON<InputEntry>(entries, filename);
        currentEntry = null;
    }

    internal void saveScore(float scoreFloat)
    {
        if(currentEntry != null){
            currentEntry.score = (int) scoreFloat;
            StartCoroutine(fileManager.WriteToLog("EventsData", "Events", EventManager.saveScore((int) scoreFloat, studyId, typeOfNPC), false));
        }
    }

    public void addBuffToPlayer(int second, int effect)
    {
        if(currentEntry != null){
            currentEntry.addBuffToPlayer(second, effect);
            StartCoroutine(fileManager.WriteToLog("EventsData", "Events", EventManager.addBuffToPlayer(second, effect, studyId, typeOfNPC), false));
        }
    }

    public void addBuffToNPC(int second, int effect)
    {
        if(currentEntry != null){
            currentEntry.addBuffToNPC(second, effect);
            StartCoroutine(fileManager.WriteToLog("EventsData", "Events", EventManager.addBuffToNPC(second, effect, studyId, typeOfNPC), false));
        }
    }

    public void addDeathToPlayer(int second)
    {
        if(currentEntry != null){
            currentEntry.addDeathToPlayer(second);
            StartCoroutine(fileManager.WriteToLog("EventsData", "Events", EventManager.addDeathToPlayer(second, studyId, typeOfNPC), false));
        }
    }

    public void addDeathToNPC(int second)
    {
        if(currentEntry != null){
            currentEntry.addDeathToNPC(second);
            StartCoroutine(fileManager.WriteToLog("EventsData", "Events", EventManager.addDeathToNPC(second, studyId, typeOfNPC), false));
        }
    }

    public void addSecondaryAttackToPlayer(int second)
    {
        if(currentEntry != null){
            currentEntry.addSecondaryAttackToPlayer(second);
            StartCoroutine(fileManager.WriteToLog("EventsData", "Events", EventManager.addSecondaryAttackToPlayer(second, studyId, typeOfNPC), false));
        }
    }

    public void addSecondaryAttackToNPC(int second)
    {
        if(currentEntry != null){
            currentEntry.addSecondaryAttackToNPC(second);
            StartCoroutine(fileManager.WriteToLog("EventsData", "Events", EventManager.addSecondaryAttackToNPC(second, studyId, typeOfNPC), false));
        }
    }
}