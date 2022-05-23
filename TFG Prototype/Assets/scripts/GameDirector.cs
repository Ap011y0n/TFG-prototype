using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    private float timer = 0.0f;
    private int second = 1;
    int progressBar = 0;
    public GameObject uiPanel;
    public GameObject uiText;
    bool eventLaunched = false;
    public PlayerController player;
    QuestManager.Quest currentQuest;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

        if(timer > second)
        {
            timer = 0;
            progressBar++;
            int res = Random.Range(0, 20);
            if(res < progressBar)
            {
                progressBar = 0;
                LaunchEvent();
            }
        }
    }

    void LaunchEvent()
    {
        Debug.Log("Quest Added");
        uiPanel.SetActive(true);
        eventLaunched = true;
        player.UIfocused = true;

        currentQuest = QuestManager.Instance.GenerateQuest("world");
        Npc dummy = new Npc();

        QuestManager.Instance.AddQuest(currentQuest, dummy);

        SceneDirector.Instance.currentBattleMapInfo = SceneDirector.Instance.currentBattleMaps[currentQuest.guid];
    }

    public void AdvanceProgressBar()
    {
        if (!eventLaunched)
            timer += Time.deltaTime;
    }

    public void AdvanceProgressBar(float value)
    {
        timer += value;
    }

    public void LaunchMission()
    {
        
        SceneManager.LoadScene("BattleMap");
        SceneDirector.Instance.SetPlayerPos(transform.position);
    }
    public void EvadeMission()
    {
        QuestManager.Instance.AbortQuest(currentQuest);
        eventLaunched = false;
        player.UIfocused = false;

    }
}