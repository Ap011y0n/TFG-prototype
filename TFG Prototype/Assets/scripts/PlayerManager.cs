using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }

    private int troopCount;

    private int maxTroopSlots = 10;

    private int gold = 1000;

    public GameObject canvas;
    private TextMeshProUGUI goldUi;
    private TextMeshProUGUI troopsUi;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            _instance = this;

        DontDestroyOnLoad(this.gameObject);

    }


    private void Start()
    {
        if (!canvas)
            canvas = GameObject.Find("Canvas");
        setGold(1000);
        setTroops(10);
        RefreshUI();
    }
    private void Update()
    {
       
    }
    public void setGold(int value)
    {
        gold = value;

    }
    public void addGold(int value)
    {
        gold += value;
    }

    public int getGold()
    {
        return gold;
    }
    public void setTroops(int value)
    {
        troopCount = value;
   
      
    }
    public void addTroops(int value)
    {
        troopCount += value;
     

        
    }

    public int getTroopCount()
    {
        return troopCount;
    }

    public void RefreshUI()
    {
        GameObject go = null;
        Transform trans = null;
            canvas = GameObject.Find("Canvas");

        if (!goldUi)
        {
            trans = canvas.transform.Find("GoldCount");
            if (trans)
                go = trans.gameObject;
            if (go)
                goldUi = go.GetComponent<TextMeshProUGUI>();
        }
        if (goldUi)
            goldUi.text = gold.ToString();

        if (!troopsUi)
        {
            trans = canvas.transform.Find("TroopsCount");
            if (trans)
                go = trans.gameObject;
            if (go)
                troopsUi = go.GetComponent<TextMeshProUGUI>();
        }
        if (troopsUi)
            troopsUi.text = troopCount.ToString();

    }
}
