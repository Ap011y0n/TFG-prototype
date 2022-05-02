using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{

    private static ChatManager _instance;
    public static ChatManager Instance { get { return _instance; } }

    [HideInInspector]
    public Npc focusedNPC;
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
        
    }

    void Update()
    {
        
    }
    public string generatePersonalInfo()
    {
        string ret = "I'm name, I work at job";
        return ret;
    }
    public string generateCityInfo()
    {
        string ret = "This is city, I'm glad you visited us";
        return ret;
    }
    public string generateRumours()
    {
        string ret = "I heard there's a monster in place";
        return ret;
    }
}
