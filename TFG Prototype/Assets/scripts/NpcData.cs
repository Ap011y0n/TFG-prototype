using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Mood
{
    // fear,
    //  anger,
    joy,
    sadness,
    disgust,
    surprise,
    maxMoods,
}

public class NpcData
{
    public string name;
    public Mood mood;
    public int stress = 0;
    public PoliticProfile profile;
    public System.Guid guid;
    public bool hasActiveQuest;
    public Family family;
    public Vector3 position;

}
