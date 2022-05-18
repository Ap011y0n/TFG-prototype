using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI goldUi;
    public TextMeshProUGUI troopsUi;
    public TextMeshProUGUI storeGold;
    public TextMeshProUGUI storeTroops;
    public GameObject inventory;
    public GameObject store;

    // Start is called before the first frame update
    void Start()
    {

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
        if(store && Input.GetKeyDown(KeyCode.P))
        {
            storeTroops.text = troopsUi.text;
            storeGold.text = goldUi.text;
            store.SetActive(!store.activeSelf);
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
}
