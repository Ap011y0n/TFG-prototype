using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public struct BattleMapInfo
{
    public HexUnit.unitType creature;
    public string mapName;
    public string place;
    public int enemiesToSpawn;
    public System.Guid guid;
}

public enum enemyType
{
    DRAGON,
    PLEB,
}


public class SceneDirector : MonoBehaviour
{
  
    string[] maleNames =  new string[] 
    {
        "Rhuhmud",
        "Fishun",
        "Gronken",
        "Stalvorn",
        "Glidrol",
        "Vif",
        "Blurbem",
        "Bror",
        "Butar-Zeoz",
        "Tinis",
        "Golmothet",
        "Shislar",
        "Jah",
        "Fuing",
        "Martascol",
        "Padrul",
    };
    string[] femaleNames = new string[]
{
        "Cohmuroh",
        "Nuseil",
        "Relirru",
        "Josvieh",
        "Sirles",
        "Ci",
        "Herhithro",
        "Kige",
        "Viresu",
        "Rizreth",
        "Eraldra",
        "Urse",
        "Xue",
        "Pai",
        "Quir",
        "Dartm",
};
    string[] surnames = new string[]
{
        " Bhossod",
        " Janno",
        " Flintblaze",
        " Rumblestrider",
        " Grusk",
        " Duzarsk",
        " Truewound",
        " Winterbrow",
        " Bekrizrud",
        " Luzifk",
        " Rugobyedye",
        " Vanunze",
        " Yung",
        " Xia",
        " Bodrese",
        " Pastuscen",
        " Nohlar",
        " Padein",
        " Greenshot",
        " Skullcut",
        " Sitsk",
        " Kevin",
        " Ironmaw",
        " Bloodoak",
        " Ronskupvot",
        " Fueltrekt",
        " Stomolzebe",
        " Zenungi",
        " Cein",
        " Wang",
        " Margargi",
        " Gunzurnas",
};

    enum Cycle
    {
        DAY,
        MIDDAY,
        NIGHT,
    }
    float currentTime;
    public float TimeSpeed = 0.1f;
    public TextMeshProUGUI TimerText;
    public DayNightCycle light;
    int eventTrigger = 0;
     
    public GameObject npcPrefab;
    public QuestLoader questLoader;

    Dictionary<string, SceneInfo> LoadedScenes;

    private GameObject playerRef;
    private Vector3 playerPos;

    private static SceneDirector _instance;
    public static SceneDirector Instance { get { return _instance; } }

    [HideInInspector]
    public Dictionary<System.Guid, BattleMapInfo> currentBattleMaps;
    [HideInInspector]
    public BattleMapInfo currentBattleMapInfo;
    bool isInWorld = false;
    // called zero
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
        LoadedScenes = new Dictionary<string, SceneInfo>();
        currentBattleMaps = new Dictionary<System.Guid, BattleMapInfo>();
        CreateSceneInfo();
    }

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void CreateSceneInfo()
    {
        GameObject[] cities = GameObject.FindGameObjectsWithTag("city");
        for (int i = 0; i < cities.Length; ++i)
        {
            if (!LoadedScenes.ContainsKey(cities[i].name))
            {
                SceneInfo newScene = new SceneInfo();
                newScene.sceneName = cities[i].name;
                newScene.sceneNpcs = new List<NpcData>();
                newScene.profile = PoliticsGenerator.createProfile();
                newScene = createNpcs(newScene);
                LoadedScenes.Add(cities[i].name, newScene);
            }
        }
    }
    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        playerRef = GameObject.FindGameObjectWithTag("Player");
        if (!LoadedScenes.ContainsKey(scene.name))
        {
            SceneInfo newScene = new SceneInfo();
            newScene.sceneName = scene.name;
            newScene.sceneNpcs = new List<NpcData>();
            newScene.profile = PoliticsGenerator.createProfile();

            if (scene.name == "WorldMap")
            {
                LoadedScenes.Add(scene.name, newScene);
                PlayerManager.Instance.RefreshUI();
                playerRef.GetComponent<PlayerController>().UIFocused();
                isInWorld = true;
                TimerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();

                GameObject lightObject = GameObject.FindGameObjectWithTag("WorldLight");
                if (lightObject)
                    light = lightObject.GetComponent<DayNightCycle>();

            }
            else if (scene.name == "BattleMap")
            {
                CombatController controller = GameObject.Find("CombatController").GetComponent<CombatController>();
                controller.Load(currentBattleMapInfo);
                isInWorld = false;

            }
            else
            {
                newScene = createNpcs(newScene);
                spawnNpcs(newScene);
                LoadedScenes.Add(scene.name, newScene);
                isInWorld = false;

            }


        }
        else
        {
            if (scene.name == "WorldMap")
            {
                playerRef.transform.position = playerPos;
                PlayerManager.Instance.RefreshUI();
                GameObject.Find("Intro").SetActive(false);
                RefreshMapQuests();
                isInWorld = true;
                TimerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
            }
            else if (scene.name == "BattleMap")
            {
                isInWorld = false;

            }
            else
            {
                spawnNpcs(LoadedScenes[scene.name]);
                isInWorld = false;

            }
        }

    }

    // called third
    void Start()
    {
        Debug.Log("Start");
        SceneInfo.fillMorningEvents();
    }

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    SceneInfo createNpcs(SceneInfo info)
    {
        //List<GameObject> spawnPositions = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        //int maxNpc = spawnPositions.Count * 2;
        int maxNpc = 40;
        Family family = CreateFamily();
        info.families = new List<Family>();
        info.families.Add(family);
        int members = 0;
        for (int i = 0; i < maxNpc; ++i)
        {

            members++;
            if (members > 2 && Random.Range(members, 8) == 7)
            {
                family = CreateFamily();
                members = 1;
                info.families.Add(family);
            }
            NpcData newNpc = new NpcData();
            newNpc.family = family;
            int genre = Random.Range(0, 2);
            newNpc.name = GenerateName(genre, family.name);
            newNpc.mood = (Mood)Random.Range(0, (int)Mood.maxMoods);
            newNpc.profile = PoliticsGenerator.createProfile();
            newNpc.guid = System.Guid.NewGuid();
            newNpc.hasActiveQuest = false;
            newNpc.position = new Vector3(0, 0, 0);
            newNpc.stress = 0;
            info.sceneNpcs.Add(newNpc);
            family.members.Add(newNpc);

        }

        for(int i = 0; i < info.families.Count; i++)
        {
            for(int j = 0; j < info.families.Count; j++)
            {
                if (i != j)
                    info.families[i].relationships.Add(info.families[j], 0);
            }
        }

        int questGiver = Random.Range(0, maxNpc);
        info.QuestGiver = info.sceneNpcs[questGiver];
        return info;
    }

    Family CreateFamily()
    {
        Family family = new Family();
        family.members = new List<NpcData>();
        family.relationships = new Dictionary<Family, int>();
        family.name = surnames[Random.Range(0, surnames.Length)];

        return family;
    }
    public void RefreshMapQuests()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("mission");
        for(int i = objs.Length-1; i >= 0; i--)
        {
            Destroy(objs[i]);
        }
        foreach (KeyValuePair<System.Guid, BattleMapInfo> entry in currentBattleMaps)
        {
            for (int i = 0; i < QuestManager.Instance.questPlaces.Count; i++)
            {
                GameObject place = QuestManager.Instance.questPlaces[i];
                if (entry.Value.place == place.name)
                {
                    QuestLoader loader = Instantiate(questLoader, place.transform.position, Quaternion.identity);
                    loader.guid = entry.Key;
                }
            }
        }
    }
    void spawnNpcs(SceneInfo info)
    {
        List<GameObject> spawnPositions = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        List<NpcData> sceneNpcs = new List<NpcData> (info.sceneNpcs);
        int maxSpawns = Random.Range(spawnPositions.Count/2, spawnPositions.Count);
        bool questgiver = false;
        for (int i = 0; i < maxSpawns; i++)
        {
            int randomPos = Random.Range(0, spawnPositions.Count);
            Vector3 spawnPos = spawnPositions[randomPos].transform.position;
            GameObject temp = GameObject.Instantiate(npcPrefab, spawnPos, Quaternion.identity);
            spawnPositions.RemoveAt(randomPos);

            if (!questgiver)
            {
                NpcData NewNpc = info.QuestGiver;
                info.QuestGiver.position = spawnPos;
                sceneNpcs.Remove(info.QuestGiver);
                temp.name = NewNpc.name;
                temp.GetComponent<Npc>().setInitParams(info, NewNpc);
                questgiver = true;
            }
            else
            {
                int id = Random.Range(0, sceneNpcs.Count);
                NpcData NewNpc = sceneNpcs[id];
                sceneNpcs.RemoveAt(id);
                temp.name = NewNpc.name;
                temp.GetComponent<Npc>().setInitParams(info, NewNpc);
            }
       
        }

    }

    public string GenerateName(int genre, string surname)
    {
        string name = "No Name Found";
        switch(genre)
        {
            case 0:
                {
                    name = maleNames[Random.Range(0, maleNames.Length)];
                }
                break;
            case 1:
                {
                    name = femaleNames[Random.Range(0, femaleNames.Length)];
                }
                break;
        }
        name += surname;
        return name;
    }

    public void SetPlayerPos(Vector3 pos)
    {
        playerPos = pos;
    }
    public void loadScene(GameObject sceneChanger)
    {

        SceneManager.LoadScene(sceneChanger.name);

    }
    private void Update()
    {
        if(isInWorld)
        {
            currentTime += TimeSpeed * Time.deltaTime;
            if (currentTime > 1440)
            {
                currentTime = 0f; eventTrigger = 0;
            }

            int hours   = Mathf.FloorToInt(currentTime / 60);
            int minutes = Mathf.FloorToInt(currentTime % 60);
            string.Format("{0:00}:{1:00}", hours, minutes);

            TimerText.text = string.Format("{0:00}:{1:00}", hours, minutes);
            if(light)
            light.SetLight(currentTime);
            if(hours >= 5 &&  eventTrigger < 1)
            {
                eventTrigger++;
                foreach (KeyValuePair<string, SceneInfo> entry in LoadedScenes)
                {
                    if(entry.Value.sceneName != "WorldMap")
                    {
                        entry.Value.MorningEvent();
                        entry.Value.CheckTantrums();
                    }

                }
            }
            if (hours >= 15 && eventTrigger < 2)
            {
                eventTrigger++;
            }
            if (hours >= 22 && eventTrigger < 3)
            {
                eventTrigger++;
            }
        }
       
    }

  
}
