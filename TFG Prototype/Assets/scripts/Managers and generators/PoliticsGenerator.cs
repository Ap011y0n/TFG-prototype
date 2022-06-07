using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Leadership
{
    Representation,
    Monarchy,
    Theocracy,
    Statocracy,
    Max,
}
public enum Religion
{
    Atheism,
    CollectiveFaith,
    OrganizedReligion,
    Zealotry,
    Max
}
public enum Foreign
{
    Isolationist,
    Interventionist,
    Internationalist,
    Imperialist,
    Max
}

public enum Economy
{
    Protectionism,
    Mercantilism,
    FreeTrade,
    Slavery,
    Max
}

public enum Military
{
    StandingArmy,
    Conscription,
    Militia,
    Pacifism,
    Max
}

public enum Cultural
{
    Aesthetics,
    Contemplation,
    Populism,
    Hegemony,
    Max
}
public enum Intellectual
{
    Antiquarian,
    Literary,
    Scholarly,
    Mechanical,
    Max
}

public enum Justice
{
    Frontier,
    Vigilantism,
    Penitentiary,
    Ordeal,
    Max
}

public struct PoliticProfile
{
    public Leadership leadership;
    public Religion religion;
    public Foreign foreign;
    public Economy economy;
    public Military military;
    public Cultural cultural;
    public Intellectual intellectual;
    public Justice justice;
}
public class PoliticsGenerator : MonoBehaviour
{
   public static PoliticProfile createProfile()
    {
        PoliticProfile profile = new PoliticProfile();
        profile.leadership = (Leadership)Random.Range(0, (int)Leadership.Max);
        profile.religion = (Religion)Random.Range(0, (int)Religion.Max);
        profile.foreign = (Foreign)Random.Range(0, (int)Foreign.Max);
        profile.economy = (Economy)Random.Range(0, (int)Economy.Max);
        profile.military = (Military)Random.Range(0, (int)Military.Max);
        profile.cultural = (Cultural)Random.Range(0, (int)Cultural.Max);
        profile.intellectual = (Intellectual)Random.Range(0, (int)Intellectual.Max);
        profile.justice = (Justice)Random.Range(0, (int)Justice.Max);

        return profile;
    }
    public static int checkLeadership(Leadership P1, Leadership P2)
    {
        return Mathf.Abs((int)P1 - (int)P2);
    }
    public static int checkReligion(Religion P1, Religion P2)
    {
        return Mathf.Abs((int)P1 - (int)P2);
    }
    public static int checkForeign(Foreign P1, Foreign P2)
    {
        return Mathf.Abs((int)P1 - (int)P2);
    }
    public static int checkEconomy(Economy P1, Economy P2)
    {
        return Mathf.Abs((int)P1 - (int)P2);
    }
    public static int checkMilitary(Military P1, Military P2)
    {
        return Mathf.Abs((int)P1 - (int)P2);
    }
    public static int checkCulture(Cultural P1, Cultural P2)
    {
        return Mathf.Abs((int)P1 - (int)P2);
    }
    public static int checkIntellectual(Intellectual P1, Intellectual P2)
    {
        return Mathf.Abs((int)P1 - (int)P2);
    }
    public static int checkJustice(Justice P1, Justice P2)
    {
        return Mathf.Abs((int)P1 - (int)P2);
    }

    public static string returnLeadershipView(Leadership leadership)
    {
        string ret = "";
        switch (leadership)
        {
            case Leadership.Representation:
                ret = "democratic representation";
                break;
            case Leadership.Monarchy:
                ret = "monarchical rule";
                break;
            case Leadership.Theocracy:
                ret = "religious representation in the government";
                break;
            case Leadership.Statocracy:
                ret = "military representation in the government";
                break;
        }
        return ret;
    }
    public static string returnReligiousView(Religion religion)
    {
        string ret = "";
        switch (religion)
        {
            case Religion.Atheism:
                ret = "a state atheism";
                break;
            case Religion.CollectiveFaith:
                ret = "a collective faith";
                break;
            case Religion.OrganizedReligion:
                ret = "an institutional religion";
                break;
            case Religion.Zealotry:
                ret = "zealotry";
                break;
        }
        return ret;
    }
    public static string returnForeignView(Foreign foreign)
    {
        string ret = "";
        switch (foreign)
        {
            case Foreign.Isolationist:
                ret = "isolationism";
                break;
            case Foreign.Interventionist:
                ret = "attention to foreign affairs";
                break;
            case Foreign.Internationalist:
                ret = "poilitical interference";
                break;
            case Foreign.Imperialist:
                ret = "military expansionism";
                break;
        }
        return ret;
    }
    public static string returnEconomyView(Economy economy)
    {
        string ret = "";
        switch (economy)
        {
            case Economy.Protectionism:
                ret = "economic interventionism";
                break;
            case Economy.Mercantilism:
                ret = "taxes to foreign merchants";
                break;
            case Economy.FreeTrade:
                ret = "market autoregulations";
                break;
            case Economy.Slavery:
                ret = "slave trading";
                break;
        }
        return ret;
    }
    public static string returnMilitaryView(Military military)
    {
        string ret = "";
        switch (military)
        {
            case Military.StandingArmy:
                ret = "nation's standing army";
                break;
            case Military.Conscription:
                ret = "conscription laws";
                break;
            case Military.Militia:
                ret = "militias";
                break;
            case Military.Pacifism:
                ret = "nation's disarment";
                break;
        }
        return ret;
    }
    public static string returnCulturalView(Cultural culture)
    {
        string ret = "";
        switch (culture)
        {
            case Cultural.Aesthetics:
                ret = "aesthetical values";
                break;
            case Cultural.Contemplation:
                ret = "philosophical contemplation";
                break;
            case Cultural.Populism:
                ret = "hero worshipping";
                break;
            case Cultural.Hegemony:
                ret = "cultural pride";
                break;
        }
        return ret;
    }

    public static string returnIntellectualView(Intellectual intellectual)
    {
        string ret = "";
        switch (intellectual)
        {
            case Intellectual.Antiquarian:
                ret = "historical events";
                break;
            case Intellectual.Literary:
                ret = "literacy";
                break;
            case Intellectual.Scholarly:
                ret = "natural laws";
                break;
            case Intellectual.Mechanical:
                ret = "engineering";
                break;
        }
        return ret;
    }
    public static string returnJudicialView(Justice justice)
    {
        string ret = "";
        switch (justice)
        {
            case Justice.Frontier:
                ret = "survival of the fittest";
                break;
            case Justice.Vigilantism:
                ret = "bounty hunters";
                break;
            case Justice.Penitentiary:
                ret = "penitentiary centers";
                break;
            case Justice.Ordeal:
                ret = "tortures and executions";
                break;
        }
        return ret;
    }
}
