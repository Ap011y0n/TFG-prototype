using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{

    private static ChatManager _instance;
    public static ChatManager Instance { get { return _instance; } }

    string[] personalInfo = new string[]
   {
        "I'm myname, nice to meet you",
        "Welcome adventurer, my name is myname, may I know yours?",
        "Greetings traveller, I'm myname",
};

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
        int randomPos = Random.Range(0, personalInfo.Length);
        string ret = personalInfo[randomPos];
        ret = ret.Replace("myname", focusedNPC.npcName);
        return ret;
    }
    public string generateCityInfo()
    {
        string ret = "This is city, I'm glad you visited us";
        ret = ret.Replace("city", focusedNPC.cityName);
        return ret;
    }
    public string generateRumours()
    {
        string ret = "I heard there's a monster in place";
        return ret;
    }
}
