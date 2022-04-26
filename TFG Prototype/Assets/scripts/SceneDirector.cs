using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    struct npc
    {
        public string name;
        public Vector3 position;
    }
    struct sceneInfo
    {
        public string sceneName;
        public List<npc> sceneNpcs;
    }
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

    Dictionary<string, sceneInfo> LoadedScenes;

    private GameObject playerRef;
    private Vector3 playerPos;

    private static SceneDirector _instance;
    public static SceneDirector Instance { get { return _instance; } }

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

            if (scene.name != "WorldMap")
            {
                newScene.sceneNpcs = createNpcs();
            }
          

            LoadedScenes.Add(scene.name, newScene);
           
        }
        else
        {
            if (scene.name == "WorldMap")
            {
                playerRef.transform.position = playerPos;

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

    List<npc> createNpcs()
    {
        List<npc> npcList = new List<npc>();
        List<GameObject> spawnPositions = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        int maxNpc = Random.Range((int)(spawnPositions.Count / 3), spawnPositions.Count);
        for (int i = 0; i < maxNpc; ++i)
        {
            int randomPos = Random.Range(0, spawnPositions.Count);
            GameObject temp = GameObject.Instantiate(npcPrefab, spawnPositions[randomPos].transform.position, Quaternion.identity);
            spawnPositions.RemoveAt(randomPos);
            int genre = Random.Range(0, 2);
            temp.name = GenerateName(genre);
            
            npc newNpc;
            newNpc.name = temp.name;
            newNpc.position = temp.transform.position;
            npcList.Add(newNpc);
        }
        return npcList;
    }

    void spawnNpcs(sceneInfo info)
    {
        for(int i = 0; i < info.sceneNpcs.Count; i++)
        {
            GameObject temp = GameObject.Instantiate(npcPrefab, info.sceneNpcs[i].position, Quaternion.identity);
            temp.name = info.sceneNpcs[i].name;
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
    private void Update()
    {
        Debug.Log(playerPos);
    }
}
