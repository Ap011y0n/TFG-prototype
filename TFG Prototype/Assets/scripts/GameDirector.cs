using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    private float timer = 0.0f;
    private int second = 1;
    int eventBar = 0;
    public GameObject uiPanel;
    public GameObject uiText;
    bool eventLaunched = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!eventLaunched)
        timer += Time.deltaTime;

        if(timer > second)
        {
            timer = 0;
            eventBar++;
            int res = Random.Range(0, 20);
            if(res < eventBar)
            {
                eventBar = 0;
                LaunchEvent();
            }
        }
    }

    void LaunchEvent()
    {
        Debug.Log("Quest Added");
        uiPanel.SetActive(true);
        eventLaunched = true;
    }

    public void LaunchMission()
    {
        QuestManager.Quest quest = QuestManager.Instance.GenerateQuest("world");
        Npc dummy = new Npc();

        QuestManager.Instance.AddQuest(quest, dummy);

        SceneDirector.Instance.currentBattleMapInfo = SceneDirector.Instance.currentBattleMaps[quest.guid];
        SceneManager.LoadScene("BattleMap");
        SceneDirector.Instance.SetPlayerPos(transform.position);
    }
    public void EvadeMission()
    {
        eventLaunched = false;
    }
}
