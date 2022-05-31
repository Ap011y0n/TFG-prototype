using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Mood
{
   // fear,
  //  anger,
    joy,
    sadness, 
    disgust,
    surprise,
    maxMoods,
}

public struct Family
{
    public string name;
    public List<npc> members;
    public Dictionary<Family, int> relationships;
}
public class Npc : MonoBehaviour
{
    public string npcName;
    public string npcWork;

    public bool introducedHimself = false;
    public bool introducedCity = false;
    public bool HasActiveQuest = false;

    public sceneInfo cityInfo;
    public TextMeshPro displayName;
    public GameObject canvas;
    public GameObject chatUI;
    public GameObject startChatting;
    public bool savePos = false;
    bool playerNear = false;
    public Mood npcMood;
    public PoliticProfile profile;
    public string job = "";
    public System.Guid NPCGuid;
    public Family family;
    string[] jobs = new string[]
    {
        "smith",
        "merchant",
        "book seller"
    };
    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        chatUI = canvas.transform.Find("ChatUI").gameObject;
        startChatting = canvas.transform.Find("StartChatting").gameObject;

    }
    void Start()
    {
        displayName.text = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerNear)
        {
            if (startChatting.activeSelf)
            {
                Debug.Log("Open by " + npcName);
                PlayerController.Instance.goToPos(transform.position);
                chatUI.SetActive(true);
                chatUI.GetComponentInChildren<Text>().text = npcName;
                ChatManager.Instance.focusedNPC = this;
                startChatting.SetActive(false);
                PlayerController.Instance.UIfocused = true;
            }
            else
            {
                Debug.Log("closed by" + npcName);
                chatUI.GetComponent<ChatButtons>().ResetAnswers();
                chatUI.SetActive(false);
                startChatting.SetActive(true);
                PlayerController.Instance.UIfocused = false;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            startChatting.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            startChatting.SetActive(false);
        }
    }

   public void setInitParams(sceneInfo info, npc newNpc)
    {
        npcName = newNpc.name;
        cityInfo = info;
        npcMood = newNpc.mood;
        profile = newNpc.profile;
        NPCGuid = newNpc.guid;
        family = newNpc.family;
        HasActiveQuest = newNpc.hasActiveQuest;
    }
}
