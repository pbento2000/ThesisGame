using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{

    int typeOfNPC;
    [SerializeField] List<string> disabledButtons;
    public bool alreadyChose;
    public int choiceInt;

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
    }

    public void setTypeOfNPC(int type)
    {
        Debug.Log(type);
        typeOfNPC = type;
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
}
