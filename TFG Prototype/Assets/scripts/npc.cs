using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Npc : MonoBehaviour
{
    public string npcName;
    public string npcWork;

    public bool introducedHimself = false;
    public bool introducedCity = false;
    public bool HasActiveQuest = false;

    public Image image;
    public SceneInfo cityInfo;
    public TextMeshPro displayName;
    public GameObject canvas;
    public GameObject chatUI;
    public GameObject startChatting;
    public bool savePos = false;
    bool playerNear = false;
    public Mood npcMood;
    public PoliticProfile profile;
    public string job = "";
    public string jobName = "";
    public int age;
    public System.Guid NPCGuid;
    public Family family;
    public float stress;
    public wander wanderScript;
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
                PlayerController.Instance.goToPos(PlayerController.Instance.gameObject.transform.position);
                chatUI.SetActive(true);
                chatUI.GetComponentInChildren<Text>().text = npcName;
                ChatManager.Instance.focusedNPC = this;
                startChatting.SetActive(false);
                PlayerController.Instance.UIfocusedBool = true;
                wanderScript.Stop();
                chatUI.GetComponent<ChatButtons>().text.text = npcName 
                    + "\nJob: " + jobName
                    + "\nAge: " + age.ToString()
                    + "\n"  + "Family:";
                for(int i = 0; i < family.members.Count; ++i)
                {
                    if(family.members[i].name != name)
                    chatUI.GetComponent<ChatButtons>().text.text += "\n" + family.members[i].name;
                }
                chatUI.GetComponent<ChatButtons>().text.text += "\n\nMood:";
                switch(npcMood)
                {
                    case Mood.joy:
                        chatUI.GetComponent<ChatButtons>().text.text += " Joyful.";
                        break;
                    case Mood.sadness:
                        chatUI.GetComponent<ChatButtons>().text.text += " Sad.";
                        break;
                    case Mood.disgust:
                        chatUI.GetComponent<ChatButtons>().text.text += " Disgusted.";
                        break;
                    case Mood.surprise:
                        chatUI.GetComponent<ChatButtons>().text.text += " Astonished.";
                        break;

                }
                chatUI.GetComponent<ChatButtons>().text.text += "\n\nStress:";
                chatUI.GetComponent<ChatButtons>().text.text += " " + stress.ToString();

                //chatUI.GetComponent<ChatButtons>().text.text += "\n\nWork:";
                //chatUI.GetComponent<ChatButtons>().text.text += " " + jobName.ToString();
            }
            else
            {
                Debug.Log("closed by" + npcName);
                chatUI.GetComponent<ChatButtons>().ResetAnswers();
                chatUI.SetActive(false);
                startChatting.SetActive(true);
                PlayerController.Instance.UIfocusedBool = false;
                wanderScript.Move();

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

   public void setInitParams(SceneInfo info, NpcData newNpc)
    {
        npcName = newNpc.name;
        cityInfo = info;
        npcMood = newNpc.mood;
        profile = newNpc.profile;
        NPCGuid = newNpc.guid;
        family = newNpc.family;
        stress = newNpc.Stress;
        HasActiveQuest = newNpc.hasActiveQuest;
        jobName = newNpc.job;
        age = newNpc.age;
    }
}
