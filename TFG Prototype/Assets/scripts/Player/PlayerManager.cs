using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
 
    private int maxTroopSlots = 10;

    private int troopNum;
    private int troopCount
    {
        set
        {
            troopNum = value;
            if (troopCount > maxTroopSlots)
                troopNum = maxTroopSlots;

        }
        get
        {
            return troopNum;
        }
    }

    public List<Character> recruitedCharacters = new List<Character>();
    public List<Unit> recruitedUnits = new List<Unit>();

    private int gold = 1000;

    public GameObject canvas;

    public Sprite[] MaleHeroesSprites;
    public Sprite[] FemaleHeroesSprites;
    public Sprite defaultHeroSprite;
    enum troopType
    {
        SPEARMEN,
    }
    struct Troop
    {
        Character UnitLeader;
        System.Guid guid;
        troopType type;
    }

    Dictionary<System.Guid, Troop> troops;
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
        setGold(2000);
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
        if ((gold - price) >= 0)
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
        for(int i = 0; i < value; ++i)
        {
            Unit unit = new Unit();
            unit.character = new Character(defaultHeroSprite);
            recruitedUnits.Add(unit);
        }
    }
    public void addTroops(Unit unit)
    {
        troopCount ++;
        unit.character = null;
        recruitedUnits.Add(unit);

    }

    public void addCharacter(Character newCharacter)
    {
        recruitedCharacters.Add(newCharacter);
    }
    public int getTroopCount()
    {
        return troopCount;
    }

    public void RefreshUI()
    {
        if(!canvas)
        canvas = GameObject.Find("Canvas");

        PlayerUi inventory = canvas.GetComponent<PlayerUi>();
        if(inventory)
        {
           inventory.SetGold(gold.ToString());
         //  inventory.SetTroops(troopCount.ToString());
           inventory.RefreshCharactersAndTroops();
        }

    }
}
