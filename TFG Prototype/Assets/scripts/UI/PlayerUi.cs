using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public TextMeshProUGUI goldUi;
    public TextMeshProUGUI troopsUi;
    public TextMeshProUGUI storeGold;
    public TextMeshProUGUI storeTroops;

    public GameObject inventory;
    public GameObject store;
    public GameObject stressInfo;
    public GameObject menu;
    public GameObject troopSelectButton;
    public Vector3 troopSelButtonPos;
    public Vector3 troopSelButtonPosInventory;

    public GameObject characterSelectButton;
    public Vector3 charSelButtonPos;
    public Vector3 charSelButtonPosInventory;

    public GameObject characterBuyButton;
    public Vector3 charBuyButtonPos;


    public int selectedUnit = -1;
    public int selectedCharacter = -1;

    private List<GameObject> RecruitedStoreCharacters = new List<GameObject>();
    private List<GameObject> RecruitedStoreTroops = new List<GameObject>();

    private List<GameObject> RecruitedInventoryCharacters = new List<GameObject>();
    private List<GameObject> RecruitedInventoryTroops = new List<GameObject>();

    public CharacterInfoUI ConfirmBuyUI = null;
    public CharacterInfoUI InfoUI = null;

    public bool isInStore = false;
    public bool isInInventory = false;
    // Start is called before the first frame update
    void Start()
    {
        selectedUnit = -1;
        selectedCharacter = -1;
        if (goldUi.text == "New Text" || troopsUi.text == "New Text")
            PlayerManager.Instance.RefreshUI();
    }

    public void CreateBuyCharacters()
    {
        PlayerManager.Instance.CreateBuyCharacters();
        for (int x = 0; x < 2; ++x)
        {
            for (int y = 0; y < 2; ++y)
            {
               // character = new Character();
                GameObject button = Instantiate(characterBuyButton, store.transform);
                Vector3 pos = charBuyButtonPos;
                pos.x += x * 130;
                pos.y -= y * 200;
                button.transform.localPosition = pos;
                button.GetComponent<BuyCharButton>().character = PlayerManager.Instance.availableCharacters[x*2+y];
                button.GetComponent<BuyCharButton>().price.text = PlayerManager.Instance.availableCharacters[x * 2 + y].price.ToString();
                button.GetComponent<BuyCharButton>().name.text = PlayerManager.Instance.availableCharacters[x * 2 + y].Name;
                button.GetComponent<BuyCharButton>().heroImage.sprite = PlayerManager.Instance.availableCharacters[x * 2 + y].heroCard;
                button.GetComponent<BuyCharButton>().playerUi = this;

            }

        }
    }
    public void CreateSelectCharacters()
    {
     
        Vector3 storePos = charSelButtonPos;
        Vector3 inventoryPos = charSelButtonPosInventory;

        for (int x = 0; x < 2; ++x)
        {
            storePos.y = charSelButtonPos.y;
            inventoryPos.y = charSelButtonPosInventory.y;
            for (int y = 0; y < PlayerManager.Instance.recruitedCharacters.Count/2; ++y)
            {
                if(store)
                {
                    GameObject storeButton = Instantiate(characterSelectButton, store.transform);
                    storeButton.transform.localPosition = storePos;
                    storeButton.GetComponent<SelectCharButton>().character = x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y;
                    storeButton.GetComponent<SelectCharButton>().playerUi = this;
                    storeButton.GetComponent<SelectCharButton>().name.text = PlayerManager.Instance.recruitedCharacters[x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y].Name;
                    storeButton.GetComponent<SelectCharButton>().heroImage.sprite = PlayerManager.Instance.recruitedCharacters[x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y].heroCard;
                    storeButton.GetComponent<SelectCharButton>().price = PlayerManager.Instance.recruitedCharacters[x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y].price;

                    RecruitedStoreCharacters.Add(storeButton);
                }
            
                if(inventory)
                {

                    GameObject inventoryButton = Instantiate(characterSelectButton, inventory.transform);
                    inventoryButton.transform.localPosition = inventoryPos;
                    inventoryButton.GetComponent<SelectCharButton>().character = x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y;
                    inventoryButton.GetComponent<SelectCharButton>().playerUi = this;
                    inventoryButton.GetComponent<SelectCharButton>().name.text = PlayerManager.Instance.recruitedCharacters[x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y].Name;
                    inventoryButton.GetComponent<SelectCharButton>().heroImage.sprite = PlayerManager.Instance.recruitedCharacters[x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y].heroCard;
                    RecruitedStoreCharacters.Add(inventoryButton);
                }
    
                storePos.y -= 180;
                inventoryPos.y -= 180;
            }
            storePos.x += 130;
            inventoryPos.x += 130;
        }
        if (PlayerManager.Instance.recruitedCharacters.Count % 2 != 0)
        {
            storePos.x -= 130;
            inventoryPos.x -= 130;
            if (store)
            {
                GameObject storeButton = Instantiate(characterSelectButton, store.transform);
                storeButton.transform.localPosition = storePos;
                storeButton.GetComponent<SelectCharButton>().character = PlayerManager.Instance.recruitedCharacters.Count - 1;
                storeButton.GetComponent<SelectCharButton>().playerUi = this;
                storeButton.GetComponent<SelectCharButton>().name.text = PlayerManager.Instance.recruitedCharacters[PlayerManager.Instance.recruitedCharacters.Count - 1].Name;
                storeButton.GetComponent<SelectCharButton>().heroImage.sprite = PlayerManager.Instance.recruitedCharacters[PlayerManager.Instance.recruitedCharacters.Count - 1].heroCard;
                storeButton.GetComponent<SelectCharButton>().price = PlayerManager.Instance.recruitedCharacters[PlayerManager.Instance.recruitedCharacters.Count - 1].price;
                RecruitedStoreCharacters.Add(storeButton);
            }
           
            if(inventory)
            {
                GameObject inventoryButton = Instantiate(characterSelectButton, inventory.transform);
                inventoryButton.transform.localPosition = inventoryPos;
                inventoryButton.GetComponent<SelectCharButton>().character = PlayerManager.Instance.recruitedCharacters.Count - 1;
                inventoryButton.GetComponent<SelectCharButton>().playerUi = this;
                inventoryButton.GetComponent<SelectCharButton>().name.text = PlayerManager.Instance.recruitedCharacters[PlayerManager.Instance.recruitedCharacters.Count - 1].Name;
                inventoryButton.GetComponent<SelectCharButton>().heroImage.sprite = PlayerManager.Instance.recruitedCharacters[PlayerManager.Instance.recruitedCharacters.Count - 1].heroCard;
                RecruitedInventoryCharacters.Add(inventoryButton);
            }
           
        }
    }

    public void CreateSelectTroops()
    {
        Vector3 StorePos = troopSelButtonPos;
        Vector3 InventoryPos = troopSelButtonPosInventory;


        for (int x = 0; x < PlayerManager.Instance.recruitedUnits.Count / 2; ++x)
        {
            StorePos.y = troopSelButtonPos.y;
            InventoryPos.y = troopSelButtonPosInventory.y;

            for (int y = 0; y < 2; ++y)
            {
                if(store)
                {
                    GameObject storebutton = Instantiate(troopSelectButton, store.transform);
                    storebutton.transform.localPosition = StorePos;
                    storebutton.GetComponent<SelectTroopUi>().unit = x + y * PlayerManager.Instance.recruitedUnits.Count / 2;

                    storebutton.GetComponent<SelectTroopUi>().playerUi = this;
                    if (PlayerManager.Instance.recruitedUnits[x + y * PlayerManager.Instance.recruitedUnits.Count / 2].character != null)
                    {
                        storebutton.GetComponent<SelectTroopUi>().heroImage.sprite = PlayerManager.Instance.recruitedUnits[x + y * PlayerManager.Instance.recruitedUnits.Count / 2].character.heroCard;
                    }
                    else
                    {
                        storebutton.GetComponent<SelectTroopUi>().heroImage.sprite = PlayerManager.Instance.defaultHeroSprite;
                    }
                    RecruitedStoreTroops.Add(storebutton);
                }

                if(inventory)
                {
                    GameObject inventorybutton = Instantiate(troopSelectButton, inventory.transform);
                    inventorybutton.transform.localPosition = InventoryPos;
                    inventorybutton.GetComponent<SelectTroopUi>().unit = x + y * PlayerManager.Instance.recruitedUnits.Count / 2;

                    inventorybutton.GetComponent<SelectTroopUi>().playerUi = this;
                    if (PlayerManager.Instance.recruitedUnits[x + y * PlayerManager.Instance.recruitedUnits.Count / 2].character != null)
                    {
                        inventorybutton.GetComponent<SelectTroopUi>().heroImage.sprite = PlayerManager.Instance.recruitedUnits[x + y * PlayerManager.Instance.recruitedUnits.Count / 2].character.heroCard;
                    }
                    else
                    {
                        inventorybutton.GetComponent<SelectTroopUi>().heroImage.sprite = PlayerManager.Instance.defaultHeroSprite;
                    }
                    RecruitedInventoryTroops.Add(inventorybutton);
                }
               
                StorePos.y -= 180;
                InventoryPos.y -= 180;

            }
            StorePos.x += 130;
            InventoryPos.x += 130;

        }
        if (PlayerManager.Instance.recruitedUnits.Count % 2 != 0)
        {
            StorePos.y += 180;
            InventoryPos.y += 180;
            if (store)
            {
                GameObject button = Instantiate(troopSelectButton, store.transform);
                button.transform.localPosition = StorePos;
                button.GetComponent<SelectTroopUi>().unit = PlayerManager.Instance.recruitedUnits.Count - 1;

                button.GetComponent<SelectTroopUi>().playerUi = this;
                if (PlayerManager.Instance.recruitedUnits[PlayerManager.Instance.recruitedUnits.Count - 1].character != null)
                {
                    button.GetComponent<SelectTroopUi>().heroImage.sprite = PlayerManager.Instance.recruitedUnits[PlayerManager.Instance.recruitedUnits.Count - 1].character.heroCard;
                }
                else
                {
                    button.GetComponent<SelectTroopUi>().heroImage.sprite = PlayerManager.Instance.defaultHeroSprite;
                }
                RecruitedStoreTroops.Add(button);
            }
            if (inventory)
            {
                GameObject button = Instantiate(troopSelectButton, inventory.transform);
                button.transform.localPosition = InventoryPos;
                button.GetComponent<SelectTroopUi>().unit = PlayerManager.Instance.recruitedUnits.Count - 1;

                button.GetComponent<SelectTroopUi>().playerUi = this;
                if (PlayerManager.Instance.recruitedUnits[PlayerManager.Instance.recruitedUnits.Count - 1].character != null)
                {
                    button.GetComponent<SelectTroopUi>().heroImage.sprite = PlayerManager.Instance.recruitedUnits[PlayerManager.Instance.recruitedUnits.Count - 1].character.heroCard;
                }
                else
                {
                    button.GetComponent<SelectTroopUi>().heroImage.sprite = PlayerManager.Instance.defaultHeroSprite;
                }
                RecruitedStoreTroops.Add(button);
            }
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isInStore)
        {
            inventory.SetActive(!inventory.activeSelf);
     
            PlayerController.Instance.UIfocusedBool = inventory.activeSelf;
            isInInventory = inventory.activeSelf;
        }
        if (store && Input.GetKeyDown(KeyCode.P) && !isInInventory)
        {
            storeGold.text = goldUi.text;
            store.SetActive(!store.activeSelf);
            PlayerController.Instance.UIfocusedBool = store.activeSelf;
            isInStore = store.activeSelf;
        }
        if (store && Input.GetKeyDown(KeyCode.S))
        {

            stressInfo.SetActive(!stressInfo.activeSelf);
            PlayerController.Instance.UIfocusedBool = stressInfo.activeSelf;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            menu.SetActive(!menu.activeSelf);
            PlayerController.Instance.UIfocusedBool = menu.activeSelf;

        }
    }

    public void SetGold(string gold)
    {
        goldUi.text = gold;
        if (store)
        {
            storeGold.text = gold;
        }
    }


    public void SetTroops(string troops)
    {
        troopsUi.text = troops + "/" + PlayerManager.Instance.maxTroopSlots.ToString();
        if (store)
        {
            storeTroops.text = troops + "/" + PlayerManager.Instance.maxTroopSlots.ToString();
        }
    }

    public void RefreshCharactersAndTroops()
    {
       
            for (int i = 0; i < RecruitedStoreCharacters.Count; ++i)
                Destroy(RecruitedStoreCharacters[i]);
            RecruitedStoreCharacters.Clear();
            for (int i = 0; i < RecruitedInventoryCharacters.Count; ++i)
                Destroy(RecruitedInventoryCharacters[i]);
            RecruitedInventoryCharacters.Clear();
            CreateSelectCharacters();

            for(int i = 0; i < RecruitedStoreTroops.Count; ++i)
                Destroy(RecruitedStoreTroops[i]);
            RecruitedStoreTroops.Clear();
            for (int i = 0; i < RecruitedInventoryTroops.Count; ++i)
                Destroy(RecruitedInventoryTroops[i]);
            RecruitedInventoryTroops.Clear();
            CreateSelectTroops();
        

    }
}
