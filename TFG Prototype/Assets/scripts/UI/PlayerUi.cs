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
    public GameObject characterSelectButton;
    public Vector3 charSelButtonPos;
    public GameObject characterBuyButton;
    public Vector3 charBuyButtonPos;

    private List<GameObject> RecruitedCharacters = new List<GameObject>();
    private List<GameObject> RecruitedTroops = new List<GameObject>();

    public Unit selectedUnit = null;
    public Character selectedCharacter = null;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void CreateBuyCharacters()
    {
        Character character;
        for (int x = 0; x < 2; ++x)
        {
            for (int y = 0; y < 2; ++y)
            {
                character = new Character();
                GameObject button = Instantiate(characterBuyButton, store.transform);
                Vector3 pos = charBuyButtonPos;
                pos.x += x * 130;
                pos.y -= y * 200;
                button.transform.localPosition = pos;
                button.GetComponent<BuyCharButton>().character = character;
                button.GetComponent<BuyCharButton>().heroImage.sprite = character.heroCard;
            }

        }
    }
    public void CreateSelectCharacters()
    {
        int characterCount = PlayerManager.Instance.recruitedCharacters.Count;
        Vector3 pos = charSelButtonPos;


        for (int x = 0; x < 2; ++x)
        {
            pos.y = charSelButtonPos.y;

            for (int y = 0; y < characterCount/2; ++y)
            {
                GameObject button = Instantiate(characterSelectButton, store.transform);
                button.transform.localPosition = pos;
                RecruitedCharacters.Add(button);
                Character character = PlayerManager.Instance.recruitedCharacters[x * characterCount / 2 + y];
                button.GetComponent<SelectCharButton>().character = character;
                button.GetComponent<SelectCharButton>().playerUi = this;
                button.GetComponent<SelectCharButton>().heroImage.sprite = character.heroCard;
                pos.y -= 200;


            }
            pos.x += 130;

        }
        if (characterCount%2 != 0)
        {
            GameObject button = Instantiate(characterSelectButton, store.transform);
            pos.x -= 130;

            button.transform.localPosition = pos;
            Character character = PlayerManager.Instance.recruitedCharacters[characterCount - 1];
            button.GetComponent<SelectCharButton>().character = character;
            button.GetComponent<SelectCharButton>().playerUi = this;
            button.GetComponent<SelectCharButton>().heroImage.sprite = character.heroCard;

            RecruitedCharacters.Add(button);
        }
    }

    public void CreateSelectTroops()
    {
        int troopCount = PlayerManager.Instance.recruitedUnits.Count;
        Vector3 pos = troopSelButtonPos;


        for (int x = 0; x < troopCount / 2; ++x)
        {
            pos.y = charSelButtonPos.y;

            for (int y = 0; y < 2; ++y)
            {
                GameObject button = Instantiate(troopSelectButton, store.transform);
                button.transform.localPosition = pos;
                Unit unit = PlayerManager.Instance.recruitedUnits[x + y * troopCount / 2];
                button.GetComponent<SelectTroopUi>().unit = unit;
                button.GetComponent<SelectTroopUi>().playerUi = this;
                if (unit.character != null)
                    button.GetComponent<SelectTroopUi>().heroImage.sprite = unit.character.heroCard;
                RecruitedTroops.Add(button);

                Character hero = button.GetComponent<SelectTroopUi>().unit.character;
                if (hero != null)
                {
                    troopSelectButton.transform.GetChild(1).GetComponent<Image>().sprite = hero.heroCard;

                    Debug.Log(unit.character.Name);

                }
                pos.y -= 200;


            }
            pos.x += 130;

        }
        if (troopCount % 2 != 0)
        {
            GameObject button = Instantiate(troopSelectButton, store.transform);
            pos.y += 200;

            button.transform.localPosition = pos;
            Unit unit = PlayerManager.Instance.recruitedUnits[troopCount-1];
            button.GetComponent<SelectTroopUi>().unit = unit;
            button.GetComponent<SelectTroopUi>().playerUi = this;
            if (unit.character != null)
            {
                button.GetComponent<SelectTroopUi>().heroImage.sprite = unit.character.heroCard;
                Debug.Log(unit.character.Name);
            }

            RecruitedTroops.Add(button);
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
            storeTroops.text = troopsUi.text;
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
        if (store)
        {
            storeTroops.text = troops.ToString();
        }
    }

    public void RefreshCharactersAndTroops()
    {
        if(store)
        {
            for (int i = 0; i < RecruitedCharacters.Count; ++i)
                Destroy(RecruitedCharacters[i]);
            RecruitedCharacters.Clear();
            CreateSelectCharacters();

            for(int i = 0; i < RecruitedTroops.Count; ++i)
                Destroy(RecruitedTroops[i]);
            RecruitedTroops.Clear();
            CreateSelectTroops();
        }

    }
}
