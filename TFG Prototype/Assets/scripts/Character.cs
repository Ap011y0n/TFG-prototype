using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    BackStory story;
    public Stats stats;
    public string Name;
    public int gender;
    public int age;
    public string description;
    public Character()
    {
        gender = Random.Range(0, 2);
        if (gender == 0)
            Name = Names.maleNames[Random.Range(0, Names.maleNames.Length)];
        if (gender == 1)
            Name = Names.femaleNames[Random.Range(0, Names.femaleNames.Length)];
        Name += Names.surnames[Random.Range(0, Names.surnames.Length)];
        age = Random.Range(18, 30);
        stats = GenerateStats();
        stats.GenerateBaseStats(age);
        story = GenerateStory();
        
    }
    public BackStory GenerateStory()
    {
        return new BackStory(this);
    }
    public Stats GenerateStats()
    {
        return new Stats();
    }
}

public class BackStory
{
    string currentCity = "";
    Character character;
    public delegate StoryEvent Action(StoryEvent consequence);
    public struct StoryEvent
    {
        public string cause;
        public string consequence;
        public Action func;
    }
    List<StoryEvent> AllEvents = new List<StoryEvent>();

    List<StoryEvent> addedEvents = new List<StoryEvent>();
    int currentAge;
   

    StoryEvent GenerateEvent(List<StoryEvent> events, out int index)
    {
        index = Random.Range(0, events.Count);
        StoryEvent ret = events[index];
        ret = ret.func(ret);
        return ret;
    }
    StoryEvent AddAgeToEvent(StoryEvent storyEvent, int age)
    {
        storyEvent.cause = "At the age of " + currentAge.ToString() + ", " + character.Name + " " + storyEvent.cause;
        currentAge += age;
        return storyEvent;
    }

    void FillEventsList()
    {
        AllEvents.Add(new StoryEvent() { cause = "fled from " + currentCity + ".", consequence = (System.Convert.ToBoolean(character.gender) ? "She":"He") + " had to travel to newcity" , func = ChangeCity });
        AllEvents.Add(new StoryEvent() { cause = "studied in " + currentCity + ".", consequence = "With this knowledge, " +(System.Convert.ToBoolean(character.gender) ? "she" : "he") + " travelled to newcity in search of new opportunities", func = StudiedInCity });
        AllEvents.Add(new StoryEvent() { cause = "was attacked by monster.", consequence = "Since then,  " + (System.Convert.ToBoolean(character.gender) ? "she" : "he") + " hates monsters", func = MonsterAttackStrength });
        AllEvents.Add(new StoryEvent() { cause = "was attacked by monster.", consequence = "Since then,  " + (System.Convert.ToBoolean(character.gender) ? "she" : "he") + " has a great fear of monsters", func = MonsterAttackWakness });
        AllEvents.Add(new StoryEvent() { cause = "worked as a mercenary.", consequence = (System.Convert.ToBoolean(character.gender) ? "She" : "He") + " learned the way of the warrior", func = Mercenary });
        AllEvents.Add(new StoryEvent() { cause = "was a poacher.", consequence = (System.Convert.ToBoolean(character.gender) ? "She" : "He") + " hunted in the forests evading the authorities", func = Poacher });
        AllEvents.Add(new StoryEvent() { cause = "learnt to write and read", consequence = (System.Convert.ToBoolean(character.gender) ? "She" : "He") + " improved" + (System.Convert.ToBoolean(character.gender) ? "her" : "his") + " learning rate", func = Write });
        AllEvents.Add(new StoryEvent() { cause = "was a smith apprentice.", consequence = (System.Convert.ToBoolean(character.gender) ? "She" : "He") + " hardened " + (System.Convert.ToBoolean(character.gender) ? "her" : "his") + " body and mind", func = Smith });
        AllEvents.Add(new StoryEvent() { cause = "was a builder. ", consequence = "Erecting walls and castles improved " + (System.Convert.ToBoolean(character.gender) ? "her" : "his") + " strength", func = Builder });
        AllEvents.Add(new StoryEvent() { cause = "was a " +(System.Convert.ToBoolean(character.gender) ? "nun" : "monk"), consequence = "During " + (System.Convert.ToBoolean(character.gender) ? "her" : "his") + " time inside the monastery" + (System.Convert.ToBoolean(character.gender) ? "she" : "he") + " learnt to write and heal the wounded", func = Monk });
        AllEvents.Add(new StoryEvent() { cause = "was a drafted in the army.", consequence = "During " + (System.Convert.ToBoolean(character.gender) ? "her" : "his") + " service, " + (System.Convert.ToBoolean(character.gender) ? "she" : "he") + " learnt discipline and how to handle a spear", func = Army });
        AllEvents.Add(new StoryEvent() { cause = "went to a fencing school.", consequence = (System.Convert.ToBoolean(character.gender) ? "She" : "He") + "improved " + (System.Convert.ToBoolean(character.gender) ? "her" : "his") + " martial prowess", func = Fencing });
        AllEvents.Add(new StoryEvent() { cause = "explored the world.", consequence = "During this time, " + (System.Convert.ToBoolean(character.gender) ? "she" : "he") + " found incredible wonders, and learnt how to traverse harsh lands", func = Explorer });

    }


    public BackStory(Character reference)
    {
        character = reference;
        currentAge = 9;
        currentCity = SceneDirector.Instance.Loadedcities[Random.Range(0, SceneDirector.Instance.Loadedcities.Count)];
        FillEventsList();
        List<StoryEvent> eventsCopy = new List<StoryEvent>(AllEvents);
        while(currentAge < reference.age && eventsCopy.Count > 0)
        {
            int index = 0;
            StoryEvent storyevent = GenerateEvent(eventsCopy, out index);
            addedEvents.Add(storyevent);
            eventsCopy.RemoveAt(index);
        }
        if (currentAge > reference.age)
        {
            currentAge = reference.age;
        }

    }

    StoryEvent StudiedInCity(StoryEvent EditEvent)
    {
        string oldCity = currentCity;
        while (oldCity == currentCity)
        {
            currentCity = SceneDirector.Instance.Loadedcities[Random.Range(0, SceneDirector.Instance.Loadedcities.Count)];
            character.description += "\n+1 intelligence, +1 medicine";

        }
        EditEvent.consequence = EditEvent.consequence.Replace("newcity", currentCity);
        EditEvent = AddAgeToEvent(EditEvent, 7);
        return EditEvent;
    }
    StoryEvent MonsterAttackStrength(StoryEvent EditEvent)
    {
        HexUnit.unitType rand = (HexUnit.unitType)Random.Range(1, 8);
        string monster = "";
        string monsters = "";

        switch (rand)
        {
            case HexUnit.unitType.Kelpie:
                {
                    character.description += "\nBonus when fighting on rivers";
                    monster = "a kelpie";
                    monsters = "kelpies";
                }
                break;
            case HexUnit.unitType.Golem:
                {
                    character.description += "\nArmor penetration +1";
                    monster = "a golem";
                    monsters = "golems";
                }
                break;
            case HexUnit.unitType.Dragon:
                {
                    character.description += "\nBonus when fighting flying enemies";
                    monster = "a dragon";
                    monsters = "dragons";
                }
                break;
            case HexUnit.unitType.Wolpertinger:
                {
                    character.description += "\nBonus when fighting on forests";
                    monster = "a wolpertinger";
                    monsters = "wolpertingers";
                }
                break;
            case HexUnit.unitType.Manticore:
                {
                    character.description += "\nBonus when fighting on mountains";
                    monster = "a manticore";
                    monsters = "manticores";
                }
                break;
            case HexUnit.unitType.Ghost:
                {
                    character.description += "\nBonus when fighting on plains";
                    monster = "a ghost";
                    monsters = "ghosts";
                }
                break;
            case HexUnit.unitType.Troll:
                {
                    character.description += "\nBonus when fighting on caves";
                    monster = "a troll";
                    monsters = "trolls";
                }
                break;
            case HexUnit.unitType.Giant:
                {
                    character.description += "\nBonus when fighting enemies large scale monsters";
                    monster = "a giant";
                    monsters = "giants";
                }
                break;
        }
        EditEvent.cause = EditEvent.cause.Replace("monster", monster);
        EditEvent.consequence = EditEvent.consequence.Replace("monsters", monsters);

        return EditEvent;
    }
    StoryEvent MonsterAttackWakness(StoryEvent EditEvent)
    {
        HexUnit.unitType rand = (HexUnit.unitType)Random.Range(1, 8);
        string monster = "";
        string monsters = "";

        switch (rand)
        {
            case HexUnit.unitType.Kelpie:
                {
                    character.description += "\nLess Morale when fighting against kelpies";
                    monster = "a kelpie";
                    monsters = "kelpies";
                }
                break;
            case HexUnit.unitType.Golem:
                {
                    character.description += "\nLess Morale when fighting against golems";
                    monster = "a golem";
                    monsters = "golems";
                }
                break;
            case HexUnit.unitType.Dragon:
                {
                    character.description += "\nLess Morale when fighting against dragons";
                    monster = "a dragon";
                    monsters = "dragons";
                }
                break;
            case HexUnit.unitType.Wolpertinger:
                {
                    character.description += "\nLess Morale when fighting against wolpertingers";
                    monster = "a wolpertinger";
                    monsters = "wolpertingers";
                }
                break;
            case HexUnit.unitType.Manticore:
                {
                    character.description += "\nLess Morale when fighting against manticores";
                    monster = "a manticore";
                    monsters = "manticores";
                }
                break;
            case HexUnit.unitType.Ghost:
                {
                    character.description += "\nLess Morale when fighting against ghosts";
                    monster = "a ghost";
                    monsters = "ghosts";
                }
                break;
            case HexUnit.unitType.Troll:
                {
                    character.description += "\nLess Morale when fighting against trolls";
                    monster = "a troll";
                    monsters = "trolls";
                }
                break;
            case HexUnit.unitType.Giant:
                {
                    character.description += "\nLess Morale when fighting against giants";
                    monster = "a giant";
                    monsters = "giants";
                }
                break;
        }
        EditEvent.cause = EditEvent.cause.Replace("monster", monster);
        EditEvent.consequence = EditEvent.consequence.Replace("monsters", monsters);

        return EditEvent;

    }

    StoryEvent ChangeCity(StoryEvent EditEvent)
    {
        string oldCity = currentCity;
        while (oldCity == currentCity)
        {
            currentCity = SceneDirector.Instance.Loadedcities[Random.Range(0, SceneDirector.Instance.Loadedcities.Count)];
            character.description += "\n+1 roguery";
            character.stats.AddStat(3, 1);

        }
        EditEvent.consequence = EditEvent.consequence.Replace("newcity", currentCity);
        EditEvent = AddAgeToEvent(EditEvent, 4);
        return EditEvent;
    }
    StoryEvent Mercenary(StoryEvent EditEvent)
    {
        
        character.description += "\n+1 strength, +1 speed";
        character.stats.AddStat(1, 1);
        character.stats.AddStat(0, 1);

        EditEvent = AddAgeToEvent(EditEvent, 7);
        return EditEvent;
    }
    StoryEvent Poacher(StoryEvent EditEvent)
    {

        character.description += "\n+1 speed";
        character.description += "\n+1 roguery";

        character.stats.AddStat(1, 1);
        character.stats.AddStat(3, 1);


        EditEvent = AddAgeToEvent(EditEvent, 8);
        return EditEvent;
    }
    StoryEvent Write(StoryEvent EditEvent)
    {

        character.description += "\n+1 intelligence";
        character.stats.AddStat(2, 1);

        EditEvent = AddAgeToEvent(EditEvent, 4);
        return EditEvent;
    }
    StoryEvent Smith(StoryEvent EditEvent)
    {

        character.description += "\n+1 strength";
        character.stats.AddStat(0, 1);

        EditEvent = AddAgeToEvent(EditEvent, 4);
        return EditEvent;
    }
    StoryEvent Builder(StoryEvent EditEvent)
    {

        character.description += "\n+1 strength";
        character.stats.AddStat(0, 1);

        EditEvent = AddAgeToEvent(EditEvent, 4);
        return EditEvent;
    }
    StoryEvent Monk(StoryEvent EditEvent)
    {

        character.description += "\n+1 medicine, + 1 intelligence";
        character.stats.AddStat(2, 1);

        EditEvent = AddAgeToEvent(EditEvent, 8);
        return EditEvent;
    }
    StoryEvent Army(StoryEvent EditEvent)
    {

        character.description += "\n+1 strength, +1 morale";
        character.stats.AddStat(0, 1);

        EditEvent = AddAgeToEvent(EditEvent, 8);
        return EditEvent;
    }
    StoryEvent Fencing(StoryEvent EditEvent)
    {

        character.description += "\n+1 speed";
        character.stats.AddStat(1, 1);

        EditEvent = AddAgeToEvent(EditEvent, 4);
        return EditEvent;
    }
    StoryEvent Explorer(StoryEvent EditEvent)
    {

        character.description += "\n+1 to commanded unit movement";
        character.stats.AddStat(4, 1);
        EditEvent = AddAgeToEvent(EditEvent, 8);
        return EditEvent;
    }
    
}

public class Stats
{

    public int Strength = 1;
    public int Speed = 1;
    public int Intelligence = 1;
    public int Roguery = 1;
    public int Movement = 0;

    public void GenerateBaseStats(int age)
    {
        for (int i = 0; i < age; i += 4)
        {
            int res = Random.Range(0, 4);
            switch (res)
            {
                case 0:
                    Strength += 1;
                    break;
                case 1:
                    Speed += 1;
                    break;
                case 2:
                    Intelligence += 1;
                    break;
                case 3:
                    Roguery += 1;
                    break;
            }
        }
    }

    public void AddStat(int stat, int value)
    {
        switch (stat)
        {
            case 0:
                Strength += value;
                break;
            case 1:
                Speed += value;
                break;
            case 2:
                Intelligence += value;
                break;
            case 3:
                Roguery += value;
                break;
            case 4:
                Movement += value;
                break;
        }
    }
}