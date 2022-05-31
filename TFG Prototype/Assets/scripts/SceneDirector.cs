using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
public struct npc
{
    public string name;
    public Mood mood;
    public PoliticProfile profile;
    public System.Guid guid;
    public bool hasActiveQuest;
    public Family family;
    public Vector3 position;
}
public struct sceneInfo
{
    public string sceneName;
    public List<npc> sceneNpcs;
    public List<Family> families;
    public npc QuestGiver;
    public PoliticProfile profile;
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

    public GameObject npcPrefab;
    public QuestLoader questLoader;

    Dictionary<string, sceneInfo> LoadedScenes;

    private GameObject playerRef;
    private Vector3 playerPos;

    private static SceneDirector _instance;
    public static SceneDirector Instance { get { return _instance; } }

    [HideInInspector]
    public Dictionary<System.Guid, BattleMapInfo> currentBattleMaps;
    [HideInInspector]
    public BattleMapInfo currentBattleMapInfo;
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
        LoadedScenes = new Dictionary<string, sceneInfo>();
        currentBattleMaps = new Dictionary<System.Guid, BattleMapInfo>();
    }

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        playerRef = GameObject.FindGameObjectWithTag("Player");
        if (!LoadedScenes.ContainsKey(scene.name))
        {
            sceneInfo newScene = new sceneInfo();
            newScene.sceneName = scene.name;
            newScene.sceneNpcs = new List<npc>();
            newScene.profile = PoliticsGenerator.createProfile();

            if (scene.name == "WorldMap")
            {
                LoadedScenes.Add(scene.name, newScene);
                PlayerManager.Instance.RefreshUI();
                playerRef.GetComponent<PlayerController>().UIFocused();
            }
            else if (scene.name == "BattleMap")
            {
                CombatController controller = GameObject.Find("CombatController").GetComponent<CombatController>();
                controller.Load(currentBattleMapInfo);
            }
            else
            {
                newScene = createNpcs(newScene);
                spawnNpcs(newScene);
                LoadedScenes.Add(scene.name, newScene);

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
               

            }
            else if (scene.name == "BattleMap")
            {

            }
            else
            {
                spawnNpcs(LoadedScenes[scene.name]);

            }
        }

    }

    // called third
    void Start()
    {
        Debug.Log("Start");
    }

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    sceneInfo createNpcs(sceneInfo info)
    {
        List<GameObject> spawnPositions = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        int maxNpc = spawnPositions.Count * 2;
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
            npc newNpc;
            newNpc.family = family;
            int genre = Random.Range(0, 2);
            newNpc.name = GenerateName(genre, family.name);
            newNpc.mood = (Mood)Random.Range(0, (int)Mood.maxMoods);
            newNpc.profile = PoliticsGenerator.createProfile();
            newNpc.guid = System.Guid.NewGuid();
            newNpc.hasActiveQuest = false;
            newNpc.position = new Vector3(0, 0, 0);
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
        family.members = new List<npc>();
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
    void spawnNpcs(sceneInfo info)
    {
        List<GameObject> spawnPositions = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        List<npc> sceneNpcs = info.sceneNpcs;
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
                npc NewNpc = info.QuestGiver;
                info.QuestGiver.position = spawnPos;
                sceneNpcs.Remove(info.QuestGiver);
                temp.name = NewNpc.name;
                temp.GetComponent<Npc>().setInitParams(info, NewNpc);
                questgiver = true;
            }
            else
            {
                int id = Random.Range(0, sceneNpcs.Count);
                npc NewNpc = sceneNpcs[id];
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
     //   Debug.Log(playerPos);
    }

  
}
