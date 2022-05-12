using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct BattleMapInfo
{
    public HexUnit.unitType creature;
    public string mapName;
    public string place;
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
    public Vector3 position;
}
public struct sceneInfo
{
    public string sceneName;
    public List<npc> sceneNpcs;
    public npc QuestGiver;
}
public class SceneDirector : MonoBehaviour
{
  
    string[] maleNames =  new string[] 
    {
        "Rhuhmud Nohlar",
        "Fishun Padein",
        "Gronken Greenshot",
        "Stalvorn Skullcut",
        "Glidrol Sitsk",
        "Vif Kevin",
        "Blurbem Ironmaw",
        "Bror Bloodoak",
        "Butar-Zeoz Ronskupvot",
        "Tinis Fueltrekt",
        "Golmothet Stomolzebe",
        "Shislar Zenungi",
        "Jah Cein",
        "Fuing Wang",
        "Martascol Margargi",
        "Padrul Gunzurnas",
    };
    string[] femaleNames = new string[]
{
        "Cohmuroh Bhossod",
        "Nuseil Janno",
        "Relirru Flintblaze",
        "Josvieh Rumblestrider",
        "Sirles Grusk",
        "Ci Duzarsk",
        "Herhithro Truewound",
        "Kige Winterbrow",
        "Viresu Bekrizrud",
        "Rizreth Luzifk",
        "Eraldra Rugobyedye",
        "Urse Vanunze",
        "Xue Yung",
        "Pai Xia",
        "Quir Bodrese",
        "Dartm Pastuscen",
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

            if (scene.name == "WorldMap")
            {
                LoadedScenes.Add(scene.name, newScene);

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
        int maxNpc = Random.Range((int)(spawnPositions.Count / 3), spawnPositions.Count);
        for (int i = 0; i < maxNpc; ++i)
        {
            npc newNpc;
            int randomPos = Random.Range(0, spawnPositions.Count);
            int genre = Random.Range(0, 2);
            newNpc.name = GenerateName(genre);
            newNpc.position = spawnPositions[randomPos].transform.position;
            spawnPositions.RemoveAt(randomPos);
            info.sceneNpcs.Add(newNpc);
        }

        int questGiver = Random.Range(0, maxNpc);
        info.QuestGiver = info.sceneNpcs[questGiver];
        return info;
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
        for(int i = 0; i < info.sceneNpcs.Count; i++)
        {
            GameObject temp = GameObject.Instantiate(npcPrefab, info.sceneNpcs[i].position, Quaternion.identity);
            temp.name = info.sceneNpcs[i].name;
            temp.GetComponent<Npc>().setInitParams(info.sceneNpcs[i].name, info);

        }

    }

    public string GenerateName(int genre)
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
        return name;
    }

    public void SetPlayerPos(Vector3 pos)
    {
        playerPos = pos;
    }
    public void loadScene(GameObject sceneChanger)
    {
        if(sceneChanger.name != "WorldMap")
        playerPos = sceneChanger.transform.position;

        SceneManager.LoadScene(sceneChanger.name);

    }
    private void Update()
    {
     //   Debug.Log(playerPos);
    }
}
