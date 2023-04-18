using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{

    int typeOfNPC;
    [SerializeField] List<int> disabledButtons;

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

    public List<int> getDisabledButtons(){
        return disabledButtons;
    }

    public void addButtonToDisable(int btn){
        disabledButtons.Add(btn);
    }
}
