using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

    [SerializeField] string filename;
    List<InputEntry> entries = new List<InputEntry> ();

    private void Start () {
        entries = FileHandler.ReadListFromJSON<InputEntry> (filename);
    }

    public void AddNameToList () {
        //entries.Add (new InputEntry ());

        FileHandler.SaveToJSON<InputEntry> (entries, filename);
    }

    public List<InputEntry> getEntries(){
        return entries;
    }

    public string getFilename(){
        return filename;
    }
}