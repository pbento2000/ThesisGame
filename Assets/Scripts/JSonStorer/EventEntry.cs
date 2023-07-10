using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static string[] effects = {"BUFF", "NERF", "SEC_ATTACK", "DEATH", "FINAL"};
    public static string[] agents = {"PLAYER", "COMPANION", "PLAYER_ENEMY", "COMPANION_ENEMY"};

    public static Dictionary<string,string> addBuffToPlayer(int second, int effect, int studyId, int typeOfNPC){
        Dictionary<string,string> entry = new Dictionary<string, string>();
        entry = fillGenericInfo(entry, studyId, typeOfNPC);
        if(effect%2 == 0){
            entry["event_Type"] = effects[0];
        }else{
            entry["event_Type"] = effects[1];
        }
        entry["event_Actuator"] = agents[0];
        if(effect < 2){
            entry["event_Receiver"] = agents[3];
        }else{
            entry["event_Receiver"] = agents[2];
        }
        entry["time_Seconds"] = second.ToString();
        entry["score"] = "-";
        return entry;
    }

    public static Dictionary<string,string> addBuffToNPC(int second, int effect, int studyId, int typeOfNPC){
        Dictionary<string,string> entry = new Dictionary<string, string>();
        entry = fillGenericInfo(entry, studyId, typeOfNPC);
        if(effect%2 == 0){
            entry["event_Type"] = effects[0];
        }else{
            entry["event_Type"] = effects[1];
        }
        entry["event_Actuator"] = agents[1];
        if(effect < 2){
            entry["event_Receiver"] = agents[3];
        }else{
            entry["event_Receiver"] = agents[2];
        }
        entry["time_Seconds"] = second.ToString();
        entry["score"] = "-";
        return entry;
    }

    public static Dictionary<string,string> addDeathToPlayer(int second, int studyId, int typeOfNPC){
        Dictionary<string,string> entry = new Dictionary<string, string>();
        entry = fillGenericInfo(entry, studyId, typeOfNPC);
        entry["event_Type"] = effects[3];
        entry["event_Actuator"] = agents[2];
        entry["event_Receiver"] = agents[0];
        entry["time_Seconds"] = second.ToString();
        entry["score"] = "-";
        return entry;
    }

    public static Dictionary<string,string> addDeathToNPC(int second, int studyId, int typeOfNPC){
        Dictionary<string,string> entry = new Dictionary<string, string>();
        entry = fillGenericInfo(entry, studyId, typeOfNPC);
        entry["event_Type"] = effects[3];
        entry["event_Actuator"] = agents[3];
        entry["event_Receiver"] = agents[1];
        entry["time_Seconds"] = second.ToString();
        entry["score"] = "-";
        return entry;
    }

    public static Dictionary<string,string> addSecondaryAttackToPlayer(int second, int studyId, int typeOfNPC){
        Dictionary<string,string> entry = new Dictionary<string, string>();
        entry = fillGenericInfo(entry, studyId, typeOfNPC);
        entry["event_Type"] = effects[2];
        entry["event_Actuator"] = agents[0];
        entry["event_Receiver"] = agents[2];
        entry["time_Seconds"] = second.ToString();
        entry["score"] = "-";
        return entry;
    }

    public static Dictionary<string,string> addSecondaryAttackToNPC(int second, int studyId, int typeOfNPC){
        Dictionary<string,string> entry = new Dictionary<string, string>();
        entry = fillGenericInfo(entry, studyId, typeOfNPC);
        entry["event_Type"] = effects[2];
        entry["event_Actuator"] = agents[1];
        entry["event_Receiver"] = agents[3];
        entry["time_Seconds"] = second.ToString();
        entry["score"] = "-";
        return entry;
    }

    public static Dictionary<string,string> saveScore(int score, int studyId, int typeOfNPC){
        Dictionary<string,string> entry = new Dictionary<string, string>();
        entry = fillGenericInfo(entry, studyId, typeOfNPC);
        entry["event_Type"] = effects[4];
        entry["event_Actuator"] = "-";
        entry["event_Receiver"] = "-";
        entry["time_Seconds"] = "180";
        entry["score"] = score.ToString();
        return entry;
    }

    private static Dictionary<string, string> fillGenericInfo(Dictionary<string, string> entry, int studyId, int typeOfNPC)
    {
        entry["study_ID"] = studyId.ToString();
        entry["companion_Type"] = typeOfNPC.ToString();
        return entry;
    }
}
