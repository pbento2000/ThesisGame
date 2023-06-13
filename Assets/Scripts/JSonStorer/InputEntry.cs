using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class InputEntry {

    public int studyId;
    public int score;
    public List<Vector2> buffsUsedByPlayer;
    public List<Vector2> buffsUsedByNPC;
    public List<int> deathsByPlayer;
    public List<int> deathsByNPC;

    public InputEntry (int id) {
        buffsUsedByPlayer = new List<Vector2>();
        buffsUsedByNPC = new List<Vector2>();
        deathsByPlayer = new List<int>();
        deathsByNPC = new List<int>();
        studyId = id;
    }

    public void writestuff(InputEntry entry){
        JsonConvert.SerializeObject(entry);
    }
}