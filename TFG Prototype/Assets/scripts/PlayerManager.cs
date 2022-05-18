using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }

    private int troopCount;

    private int maxTroopSlots = 10;

    private int gold = 1000;

    public GameObject canvas;


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
        setTroops(4);
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

    public bool canBuy(int price)
    {
        if ((gold - price) > 0)
            return true;
        return false;
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
        if(!canvas)
        canvas = GameObject.Find("Canvas");

        InventoryUI inventory = canvas.GetComponent<InventoryUI>();
        if(inventory)
        {
           inventory.SetGold(gold.ToString());
           inventory.SetTroops(troopCount.ToString());

        }
    }
}
