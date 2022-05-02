using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    enum Tag
    {
        FOREST, 
        CAVE, 
        PLAIN,
        MOUNTAINS,
        RIVER,
        Count
    }
    private int questTypes = 1;
    public enum QuestType
    {
        HUNT,
    }
    public struct Quest
    {
        public string questGiver;
        public string questName;
        public string questDescription;
        public QuestType type;
    }

    struct QuestText
    {
        public List<Tag> tags;
        public string text;

    }
    struct CreatureName
    {
        public List<Tag> tags;
        public string name;
    }
    struct PlaceName
    {
        public Tag tag;
        public string name;
    }

    public List<GameObject> questPlaces;
    private List<Quest> activeQuests;
    private List<Quest> completedQuests;
    private List<QuestText> questTexts;
    private List<CreatureName> creatureNames;
    private List<PlaceName> placeNames;


    private static QuestManager _instance;
    public static QuestManager Instance { get { return _instance; } }

   
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            _instance = this;

        Debug.Log("Awake");
        DontDestroyOnLoad(this.gameObject);

        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
        questTexts = new List<QuestText>();
        creatureNames = new List<CreatureName>();
        placeNames = new List<PlaceName>();

        AddQuestTexts();
        AddCreatureNames();
        AddPlaces();
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F5))
        {
            Quest newQuest =GenerateQuest("GOD");
            Debug.Log(newQuest.questName);
            Debug.Log(newQuest.questGiver);
            Debug.Log(newQuest.questDescription);

        }
    }

    public Quest GenerateQuest(string giver)
    {
        Quest newquest = new Quest();
        newquest.questGiver = giver;
        newquest.type = (QuestType)Random.Range(0, questTypes);
      
        PlaceName place = placeNames[Random.Range(0, placeNames.Count)];

        List<CreatureName> creatures = returnCreaturesWithTag(place.tag);
        CreatureName creature = creatures[Random.Range(0, creatures.Count)];
        List<QuestText> texts = returnQuestTextsWithTag(place.tag);
        QuestText text = texts[Random.Range(0, texts.Count)];

        text.text = text.text.Replace("place", place.name);
        newquest.questDescription = text.text.Replace("monster", creature.name);

        switch (newquest.type)
        {
            default:
                newquest.questName = "No quest type";
                break;
            case QuestType.HUNT:
                newquest.questName = "Hunt the " + creature.name;
                break;
        }

        return newquest;
    }
    public void AddQuest(Quest newquest)
    {
        activeQuests.Add(newquest);
    }
    public void EndQuest(Quest endedQuest)
    {
        activeQuests.Remove(endedQuest);
        completedQuests.Add(endedQuest);
    }
    public void AddQuestTexts()
    {
        QuestText newText = new QuestText();
        newText.tags = new List<Tag>();

        for (int i = 0; i <= (int)Tag.Count; i ++)
        {
            newText.tags.Add((Tag)i);
        }
        newText.text = "Kill the monster at the place";
        questTexts.Add(newText);
    }

    public void AddCreatureNames()
    {
        CreatureName newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.RIVER);
        newCreature.tags.Add(Tag.FOREST);
        newCreature.name = "Kelpie";
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.CAVE);
        newCreature.name = "Golem";
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.MOUNTAINS);
        newCreature.tags.Add(Tag.CAVE);
        newCreature.tags.Add(Tag.PLAIN);
        newCreature.name = "Dragon";
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.FOREST);
        newCreature.name = "Wolpertinger";
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.MOUNTAINS);
        newCreature.tags.Add(Tag.PLAIN);
        newCreature.name = "Manticore";
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.PLAIN);
        newCreature.tags.Add(Tag.FOREST);
        newCreature.name = "Ghost";
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.CAVE);
        newCreature.tags.Add(Tag.RIVER);
        newCreature.name = "Troll";
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.MOUNTAINS);
        newCreature.tags.Add(Tag.FOREST);
        newCreature.name = "Giant";
        creatureNames.Add(newCreature);
    }
    public void AddPlaces()
    {
        for (int i = 0; i < questPlaces.Count; ++i)
        {
            PlaceName newPlace = new PlaceName();

            switch(questPlaces[i].tag)
            {
                case "forest":
                    {
                        newPlace.tag = Tag.FOREST;
                        newPlace.name = questPlaces[i].name;
                    }
                    break;
                case "cave":
                    {
                        newPlace.tag = Tag.CAVE;
                        newPlace.name = questPlaces[i].name;
                    }
                    break;
                case "plain":
                    {
                        newPlace.tag = Tag.PLAIN;
                        newPlace.name = questPlaces[i].name;
                    }
                    break;
                case "mountain":
                    {
                        newPlace.tag = Tag.MOUNTAINS;
                        newPlace.name = questPlaces[i].name;
                    }
                    break;
                case "river":
                    {
                        newPlace.tag = Tag.RIVER;
                        newPlace.name = questPlaces[i].name;
                    }
                    break;
            }

            placeNames.Add(newPlace);
        }
    }

    List<CreatureName> returnCreaturesWithTag(Tag tag)
    {
        List<CreatureName> list = new List<CreatureName>();

        for (int i = 0; i < creatureNames.Count; i++)
        {
            if (creatureNames[i].tags.Contains(tag))
                list.Add(creatureNames[i]);

        }
        return list;
    }

    List<QuestText> returnQuestTextsWithTag(Tag tag)
    {
        List<QuestText> list = new List<QuestText>();

        for(int i = 0; i < questTexts.Count; i++)
        {
            if (questTexts[i].tags.Contains(tag))
                list.Add(questTexts[i]);

        }
        

        return list;
    }
}