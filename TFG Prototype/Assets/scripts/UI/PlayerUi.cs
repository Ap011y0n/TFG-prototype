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
    public GameObject inventory;
    public GameObject store;
    public GameObject stressInfo;
    public GameObject menu;
    public GameObject troopSelectButton;
    public Vector3 troopSelButtonPos;
    public GameObject characterSelectButton;
    public Vector3 charSelButtonPos;
    public GameObject characterBuyButton;
    public Vector3 charBuyButtonPos;


    public int selectedUnit = -1;
    public int selectedCharacter = -1;

    private List<GameObject> RecruitedStoreCharacters = new List<GameObject>();
    private List<GameObject> RecruitedStoreTroops = new List<GameObject>();

    private List<GameObject> RecruitedInventoryCharacters = new List<GameObject>();
    private List<GameObject> RecruitedInventoryTroops = new List<GameObject>();

    public CharacterInfoUI ConfirmBuyUI = null;
    // Start is called before the first frame update
    void Start()
    {
        selectedUnit = -1;
        selectedCharacter = -1;
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
                button.GetComponent<BuyCharButton>().heroImage.sprite = PlayerManager.Instance.availableCharacters[x * 2 + y].heroCard;
                button.GetComponent<BuyCharButton>().playerUi = this;

            }

        }
    }
    public void CreateSelectCharacters()
    {
     
        Vector3 pos = charSelButtonPos;


        for (int x = 0; x < 2; ++x)
        {
            pos.y = charSelButtonPos.y;

            for (int y = 0; y < PlayerManager.Instance.recruitedCharacters.Count/2; ++y)
            {
                if(store)
                {
                    GameObject storeButton = Instantiate(characterSelectButton, store.transform);
                    storeButton.transform.localPosition = pos;
                    storeButton.GetComponent<SelectCharButton>().character = x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y;
                    storeButton.GetComponent<SelectCharButton>().playerUi = this;
                    storeButton.GetComponent<SelectCharButton>().name.text = PlayerManager.Instance.recruitedCharacters[x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y].Name;
                    storeButton.GetComponent<SelectCharButton>().heroImage.sprite = PlayerManager.Instance.recruitedCharacters[x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y].heroCard;
                    RecruitedStoreCharacters.Add(storeButton);
                }
            
                if(inventory)
                {
                    GameObject inventoryButton = Instantiate(characterSelectButton, inventory.transform);
                    inventoryButton.transform.localPosition = pos;
                    inventoryButton.GetComponent<SelectCharButton>().character = x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y;
                    inventoryButton.GetComponent<SelectCharButton>().playerUi = this;
                    inventoryButton.GetComponent<SelectCharButton>().name.text = PlayerManager.Instance.recruitedCharacters[x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y].Name;
                    inventoryButton.GetComponent<SelectCharButton>().heroImage.sprite = PlayerManager.Instance.recruitedCharacters[x * PlayerManager.Instance.recruitedCharacters.Count / 2 + y].heroCard;
                    RecruitedStoreCharacters.Add(inventoryButton);
                }
    
                pos.y -= 180;

            }
            pos.x += 130;

        }
        if (PlayerManager.Instance.recruitedCharacters.Count % 2 != 0)
        {
            pos.x -= 130;
            if(store)
            {
                GameObject storeButton = Instantiate(characterSelectButton, store.transform);
                storeButton.transform.localPosition = pos;
                storeButton.GetComponent<SelectCharButton>().character = PlayerManager.Instance.recruitedCharacters.Count - 1;
                storeButton.GetComponent<SelectCharButton>().playerUi = this;
                storeButton.GetComponent<SelectCharButton>().name.text = PlayerManager.Instance.recruitedCharacters[PlayerManager.Instance.recruitedCharacters.Count - 1].Name;
                storeButton.GetComponent<SelectCharButton>().heroImage.sprite = PlayerManager.Instance.recruitedCharacters[PlayerManager.Instance.recruitedCharacters.Count - 1].heroCard;
                RecruitedStoreCharacters.Add(storeButton);
            }
           
            if(inventory)
            {
                GameObject inventoryButton = Instantiate(characterSelectButton, inventory.transform);
                inventoryButton.transform.localPosition = pos;
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
        Vector3 pos = troopSelButtonPos;


        for (int x = 0; x < PlayerManager.Instance.recruitedUnits.Count / 2; ++x)
        {
            pos.y = troopSelButtonPos.y;

            for (int y = 0; y < 2; ++y)
            {
                if(store)
                {
                    GameObject storebutton = Instantiate(troopSelectButton, store.transform);
                    storebutton.transform.localPosition = pos;
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
                    inventorybutton.transform.localPosition = pos;
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
               
                pos.y -= 180;


            }
            pos.x += 130;

        }
        if (PlayerManager.Instance.recruitedUnits.Count % 2 != 0)
        {
            pos.y += 180;

            if(store)
            {
                GameObject button = Instantiate(troopSelectButton, store.transform);
                button.transform.localPosition = pos;
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
                GameObject button = Instantiate(troopSelectButton, store.transform);
                button.transform.localPosition = pos;
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);
            if (goldUi.text == "New Text" || troopsUi.text == "New Text")
                PlayerManager.Instance.RefreshUI();
        }
        if (store && Input.GetKeyDown(KeyCode.P))
        {
            storeGold.text = goldUi.text;
            store.SetActive(!store.activeSelf);
            PlayerController.Instance.UIfocused = store.activeSelf;

        }
        if (store && Input.GetKeyDown(KeyCode.S))
        {

            stressInfo.SetActive(!stressInfo.activeSelf);
            PlayerController.Instance.UIfocused = stressInfo.activeSelf;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            menu.SetActive(!menu.activeSelf);
            PlayerController.Instance.UIfocused = menu.activeSelf;

        }
    }

    public void SetGold(string gold)
    {
        goldUi.text = gold.ToString();
        if (store)
        {
            storeGold.text = gold.ToString();
        }
    }

    public void SetTroops(string troops)
    {
        troopsUi.text = troops;
      
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
