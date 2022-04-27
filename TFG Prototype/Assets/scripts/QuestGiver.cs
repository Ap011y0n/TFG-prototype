using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public GameObject pickQuestText;
    private bool canPickQuest = false;
    private QuestManager questManager;
    // Start is called before the first frame update
    void Start()
    {
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickQuest)
        {
            canPickQuest = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickQuestText.SetActive(true);
            canPickQuest = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickQuestText.SetActive(false);
            canPickQuest = false;
        }
    }
}
