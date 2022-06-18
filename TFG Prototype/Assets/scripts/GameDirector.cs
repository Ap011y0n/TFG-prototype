using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameDirector : MonoBehaviour
{
    private float timer = 0.0f;
    private int second = 1;
    float progressBar = 0f;
    public GameObject combatEventPanel;
    public GameObject goldEventPanel;

    public TextMeshProUGUI uiText;
    bool eventLaunched = false;
    public PlayerController player;
    QuestManager.Quest currentQuest;
    public string questText;
    public int eventProbability = 60;
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
            
            int res = Random.Range(0, eventProbability);
            if(res < progressBar*SceneDirector.Instance.CombatProbModifier)
            {
                progressBar = 0;
                LaunchCombatEvent();
            }
            int res2 = Random.Range(0, eventProbability);
            if (res < progressBar * SceneDirector.Instance.GoldProbModifier)
            {
                progressBar = 0;
                LaunchgoldEvent();
            }
        }
    }

    void LaunchCombatEvent()
    {
        Debug.Log("Quest Added");
        combatEventPanel.SetActive(true);
        eventLaunched = true;
        player.UIfocusedBool = true;
        player.goToPos(player.transform.position);
        currentQuest = QuestManager.Instance.GenerateQuest("world");
        uiText.text = questText;
        uiText.text = uiText.text.Replace("monster", currentQuest.questName.Replace("Hunt a", ""));
        Npc dummy = new Npc();

        QuestManager.Instance.AddQuest(currentQuest, dummy);

        SceneDirector.Instance.currentBattleMapInfo = SceneDirector.Instance.currentBattleMaps[currentQuest.guid];
    }
    
    void LaunchgoldEvent()
    {
        goldEventPanel.SetActive(true);
        eventLaunched = true;
        player.UIfocusedBool = true;
        player.goToPos(player.transform.position);

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
        SceneDirector.Instance.SetPlayerPos(transform.position);

        SceneManager.LoadScene("BattleMap");
    }
    public void EvadeMission()
    {
        PlayerManager.Instance.addGold(-50);
        PlayerManager.Instance.RefreshUI();
        QuestManager.Instance.AbortQuest(currentQuest);
        SceneDirector.Instance.GoldProbModifier += 0.1f;
        SceneDirector.Instance.CombatProbModifier -= 0.1f;

        Debug.Log(SceneDirector.Instance.GoldProbModifier);
        eventLaunched = false;
        player.UIfocusedBool = false;

    }

    public void TakeGold()
    {
        SceneDirector.Instance.GoldProbModifier -= 0.1f;
        Debug.Log(SceneDirector.Instance.GoldProbModifier);
        PlayerManager.Instance.addGold(100);
        PlayerManager.Instance.RefreshUI();
        player.UIfocusedBool = false;
        eventLaunched = false;

    }
}
