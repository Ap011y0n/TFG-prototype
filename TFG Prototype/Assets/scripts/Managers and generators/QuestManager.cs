using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public System.Guid guid;
        public int reward;
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
        public int number;
    }
    struct PlaceName
    {
        public Tag tag;
        public string name;
    }

    [HideInInspector]
    public List<GameObject> questPlaces;
   // private List<Quest> activeQuests;
    private List<QuestText> questTexts;
    private List<CreatureName> creatureNames;
    private List<PlaceName> placeNames;

    public Dictionary<Quest, System.Guid> activeQuests;
    public Dictionary<Quest, System.Guid> completedQuests;
    public Dictionary<Quest, System.Guid> failedQuests;

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

       
    }

    void Start()
    {
        activeQuests = new Dictionary<Quest, System.Guid>();
        completedQuests = new Dictionary<Quest, System.Guid>();
        failedQuests = new Dictionary<Quest, System.Guid>();

        questTexts = new List<QuestText>();
        creatureNames = new List<CreatureName>();
        placeNames = new List<PlaceName>();

        AddQuestTexts();
        AddCreatureNames();
        AddPlaces();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F5))
        {
            Quest newQuest = GenerateQuest("GOD");
            Debug.Log(newQuest.questName);
            Debug.Log(newQuest.questGiver);
            Debug.Log(newQuest.questDescription);
           

        }
    }

    public Quest GenerateQuest(string giver)
    {
        Quest newquest = new Quest();
        BattleMapInfo info = new BattleMapInfo();

        newquest.questGiver = giver;
        newquest.type = (QuestType)Random.Range(0, questTypes);
      
        PlaceName place = placeNames[Random.Range(0, placeNames.Count)];

        List<CreatureName> creatures = returnCreaturesWithTag(place.tag);
        CreatureName creature = creatures[Random.Range(0, creatures.Count)];
        List<QuestText> texts = returnQuestTextsWithTag(place.tag);
        QuestText text = texts[Random.Range(0, texts.Count)];

        text.text = text.text.Replace("place", place.name);
        newquest.questDescription = text.text.Replace("monster", creature.name);

        switch (creature.name)
        {
            case "Kelpies":
                info.creature = HexUnit.unitType.Kelpie;
                break;
            case "Golem":
                info.creature = HexUnit.unitType.Golem;
                break;
            case "Dragon":
                info.creature = HexUnit.unitType.Dragon;
                break;
            case "Wolpertingers":
                info.creature = HexUnit.unitType.Wolpertinger;
                break;
            case "Manticore":
                info.creature = HexUnit.unitType.Manticore;
                break;
            case "Ghosts":
                info.creature = HexUnit.unitType.Ghost;
                break;
            case "Trolls":
                info.creature = HexUnit.unitType.Troll;
                break;
            case "Giant":
                info.creature = HexUnit.unitType.Giant;
                break;
        }

        info.place = place.name;
        info.enemiesToSpawn = creature.number;

        switch (newquest.type)
        {
            default:
                newquest.questName = "No quest type";
                break;
            case QuestType.HUNT:
                if(creature.number > 1)
                {
                    newquest.questName = "Hunt a group of " + creature.name;
                    info.mapName = "MultiEntityMap";

                }
                else
                {
                    newquest.questName = "Hunt a " + creature.name;
                    info.mapName = "MultiEntityMap";

                }
                break;
        }

        System.Guid guid = System.Guid.NewGuid();
        newquest.guid = guid;
        info.guid = guid;

        SceneDirector.Instance.currentBattleMaps.Add(guid, info);
      

        newquest.reward = 500;
        return newquest;
    }
    public void AddQuest(Quest newquest, Npc giver)
    {
        activeQuests.Add(newquest, giver.NPCGuid);
    }
    public void EndQuest(Quest endedQuest, bool victory)
    {
      
        SceneDirector.Instance.currentBattleMaps.Remove(endedQuest.guid);
        if(victory)
        {
            PlayerManager.Instance.addGold(endedQuest.reward);
            completedQuests.Add(endedQuest, activeQuests[endedQuest]);
        }
        else
        {
            failedQuests.Add(endedQuest, activeQuests[endedQuest]);
        }
     
        activeQuests.Remove(endedQuest);
        SceneDirector.Instance.changeQuestGiver(endedQuest.questGiver);
    }
    public void AbortQuest(Quest endedQuest)
    {
        activeQuests.Remove(endedQuest);
        SceneDirector.Instance.currentBattleMaps.Remove(endedQuest.guid);
    }
    public Quest GetActiveQuest(System.Guid guid)
    {
        foreach (KeyValuePair<Quest, System.Guid> entry in activeQuests)
        {
            if(entry.Key.guid == guid)
            {
                return entry.Key;
            }
        }

        Debug.LogError("No quest with guid " + guid.ToString());
        return new Quest();
    }
    public void AddQuestTexts()
    {
        QuestText newText = new QuestText();
        newText.tags = new List<Tag>();

        for (int i = 0; i <= (int)Tag.Count; i ++)
        {
            newText.tags.Add((Tag)i);
        }
        newText.text = "Kill the monster at place";
        questTexts.Add(newText);
    }

    public void AddCreatureNames()
    {
        CreatureName newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.RIVER);
        newCreature.tags.Add(Tag.FOREST);
        newCreature.number = 3;
        newCreature.name = "Kelpies";
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.CAVE);
        newCreature.name = "Golem";
        newCreature.number = 2;
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.MOUNTAINS);
        newCreature.tags.Add(Tag.CAVE);
        newCreature.tags.Add(Tag.PLAIN);
        newCreature.number = 1;
        newCreature.name = "Dragon";
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.FOREST);
        newCreature.name = "Wolpertingers";
        newCreature.number = 10;
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.MOUNTAINS);
        newCreature.tags.Add(Tag.PLAIN);
        newCreature.name = "Manticore";
        newCreature.number = 1;
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.PLAIN);
        newCreature.tags.Add(Tag.FOREST);
        newCreature.name = "Ghosts";
        newCreature.number = 7;
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.CAVE);
        newCreature.tags.Add(Tag.RIVER);
        newCreature.name = "Trolls";
        newCreature.number = 4;
        creatureNames.Add(newCreature);

        newCreature = new CreatureName();
        newCreature.tags = new List<Tag>();
        newCreature.tags.Add(Tag.MOUNTAINS);
        newCreature.tags.Add(Tag.FOREST);
        newCreature.name = "Giant";
        newCreature.number = 1;
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
