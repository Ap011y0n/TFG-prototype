using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters 
{
    BackStory story;
    public string Name;
    public Characters()
    {
        //Name = 
    }
    public BackStory GenerateStory()
    {
        return new BackStory(this);
    }

}

public class BackStory
{
    struct StoryEvent
    {
        public string cause;
        public string consequence;
    }
    Characters character;
    List<StoryEvent> events;
    int currentAge;
    public BackStory(Characters reference)
    {
        character = reference;
    }

    StoryEvent AddAgeToEvent(StoryEvent storyEvent)
    {
        storyEvent.cause += "At the age of" + currentAge.ToString() + ", " + character.Name + storyEvent.cause;

        return storyEvent;
    }
}