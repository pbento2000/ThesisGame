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

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Storage");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }else{
            filename = this.gameObject.GetComponent<InputHandler>().getFilename();
            entries = this.gameObject.GetComponent<InputHandler>().getEntries();
            if(entries.Count == 0){
                studyId = 0;
            }else{
                studyId = entries[entries.Count-1].studyId;
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        typeOfNPC = Random.Range(0, 4);
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
        currentEntry = null;
        FileHandler.SaveToJSON<List<InputEntry>>(entries, filename);
    }

    internal void saveScore(float scoreFloat)
    {
        currentEntry.score = (int) scoreFloat;
    }
}