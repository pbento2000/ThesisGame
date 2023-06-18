using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class InputHandler : MonoBehaviour {

    [SerializeField] string filename;
    List<InputEntry> entries = new List<InputEntry> ();

    private void Awake () {
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