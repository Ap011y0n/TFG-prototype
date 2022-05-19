using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public class Npc : MonoBehaviour
{
    public string npcName;
    public string npcWork;

    public bool introducedHimself = false;
    public bool introducedCity = false;
    public sceneInfo cityInfo;
    public TextMesh displayName;
    public GameObject canvas;
    public GameObject chatUI;
    public GameObject startChatting;
    public bool savePos = false;
    bool playerNear = false;
    public Mood npcMood;
    public PoliticProfile profile;

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
        displayName = displayName.GetComponent<TextMesh>();
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

   public void setInitParams(string name, sceneInfo city, Mood mood, PoliticProfile politics)
    {
        npcName = name;
        cityInfo = city;
        npcMood = mood;
        profile = politics;
    }
}
