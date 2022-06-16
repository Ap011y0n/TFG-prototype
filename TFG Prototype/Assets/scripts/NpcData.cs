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
    private int stress;
    public int imageId;
    public int gender;
    public int Stress
    {
        get { return stress;  }
        set { stress = Mathf.Clamp(value, -50, 50); }
    }
    public PoliticProfile profile;
    public System.Guid guid;
    public bool hasActiveQuest;
    public Family family;
    public Vector3 position;
    public string history = "";
    public int returnConflictivePolitics(PoliticProfile cityProfile)
    {
        int complicity =  PoliticsGenerator.checkLeadership(profile.leadership, cityProfile.leadership);
        if (complicity >= 2)
            return 1;
        complicity = PoliticsGenerator.checkForeign(profile.foreign, cityProfile.foreign);
        if (complicity >= 2)
            return 2;
        complicity = PoliticsGenerator.checkReligion(profile.religion, cityProfile.religion);
        if (complicity >= 2)
            return 3;
        complicity = PoliticsGenerator.checkJustice(profile.justice, cityProfile.justice);
        if (complicity >= 2)
            return 4;
        complicity = PoliticsGenerator.checkMilitary(profile.military, cityProfile.military);
        if (complicity >= 2)
            return 5;
        complicity = PoliticsGenerator.checkEconomy(profile.economy, cityProfile.economy);
        if (complicity >= 2)
            return 6;
        complicity = PoliticsGenerator.checkCulture(profile.cultural, cityProfile.cultural);
        if (complicity >= 2)
            return 7;
        complicity = PoliticsGenerator.checkIntellectual(profile.intellectual, cityProfile.intellectual);
        if (complicity >= 2)
            return 8;

        return -1;
    }
}