using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestLoader : MonoBehaviour
{
    public System.Guid guid;
    public GameObject EnterText;
    public bool savePos = true;
    private bool canEnter = false;
    private GameObject canvas;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        EnterText = canvas.transform.Find("Load Scene").gameObject;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canEnter)
        {
            canEnter = false;
            SceneDirector.Instance.currentBattleMapInfo = SceneDirector.Instance.currentBattleMaps[guid];
            SceneManager.LoadScene("BattleMap");
            SceneDirector.Instance.SetPlayerPos(transform.position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnterText.SetActive(true);
            canEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnterText.SetActive(false);
            canEnter = false;
        }
    }
}
