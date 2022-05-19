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
}
