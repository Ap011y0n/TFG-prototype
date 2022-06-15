using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyButton : MonoBehaviour
{
    public TextMeshProUGUI price;
    public Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.RefreshUI();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyTroop()
    {
        if(PlayerManager.Instance.canBuy(int.Parse(price.text)) && PlayerManager.Instance.recruitedUnits.Count < PlayerManager.Instance.maxTroopSlots)
        {
            PlayerManager.Instance.addGold(-int.Parse(price.text));
            unit = new Unit();
            PlayerManager.Instance.addTroops(unit);
            PlayerManager.Instance.RefreshUI();
        }

    }
}
