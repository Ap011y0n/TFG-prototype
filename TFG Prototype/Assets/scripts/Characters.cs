using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters 
{
    BackStory story;
    public string Name;
    public int gender;
    public int age;
    public Characters()
    {
        gender = Random.Range(0, 2);
        if (gender == 0)
            Name = Names.maleNames[Random.Range(0, Names.maleNames.Length)];
        if (gender == 1)
            Name = Names.femaleNames[Random.Range(0, Names.femaleNames.Length)];
        Name += Names.surnames[Random.Range(0, Names.surnames.Length)];
        age = Random.Range(18, 30);
        story = GenerateStory();
    }
    public BackStory GenerateStory()
    {
        return new BackStory(this);
    }

}

public class BackStory
{
    string currentCity = "";
    Characters character;
    public delegate StoryEvent Action(StoryEvent consequence);
    public struct StoryEvent
    {
        public string cause;
        public string consequence;
        public Action func;
    }
    static List<StoryEvent> AllEvents = new List<StoryEvent>();

    List<StoryEvent> addedEvents = new List<StoryEvent>();
    int currentAge;
   

    StoryEvent GenerateEvent()
    {
        StoryEvent ret = AllEvents[Random.Range(0, AllEvents.Count)];
        ret = ret.func(ret);
        return ret;
    }
    StoryEvent AddAgeToEvent(StoryEvent storyEvent)
    {
        storyEvent.cause += "At the age of" + currentAge.ToString() + ", " + character.Name + storyEvent.cause;

        return storyEvent;
    }

    void FillEventsList()
    {
        AllEvents.Add(new StoryEvent() { cause = "Fled from " + currentCity + ".", consequence = (System.Convert.ToBoolean(character.gender) ? "She":"He") + " had to travel to newcity" , func = ChangeCity });
        AllEvents.Add(new StoryEvent() { cause = "Was attacked by monster.", consequence = "That is why " + (System.Convert.ToBoolean(character.gender) ? "she" : "he" ) + " hates monster", func = MonsterAttack });

    }
    StoryEvent ChangeCity(StoryEvent EditEvent)
    {
        string oldCity = currentCity;
        while(oldCity == currentCity)
        {
            currentCity = SceneDirector.Instance.Loadedcities[Random.Range(0, SceneDirector.Instance.Loadedcities.Count)];
        }
        EditEvent.consequence = EditEvent.consequence.Replace("newcity", currentCity);
        return EditEvent;
    }
    StoryEvent MonsterAttack(StoryEvent EditEvent)
    {
        string monster = "trolls";
        EditEvent.cause = EditEvent.cause.Replace("monster", monster);
        EditEvent.consequence = EditEvent.consequence.Replace("monster", monster);

        return EditEvent;

    }

    public BackStory(Characters reference)
    {
        character = reference;
        currentAge = 5;
        currentCity = SceneDirector.Instance.Loadedcities[Random.Range(0, SceneDirector.Instance.Loadedcities.Count)];
        FillEventsList();
        addedEvents.Add(GenerateEvent());
    }
}