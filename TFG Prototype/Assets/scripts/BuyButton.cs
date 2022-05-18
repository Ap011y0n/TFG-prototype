using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyButton : MonoBehaviour
{
    public TextMeshProUGUI price;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyTroop()
    {
        if(PlayerManager.Instance.canBuy(int.Parse(price.text)))
        {
            PlayerManager.Instance.addGold(-int.Parse(price.text));
            PlayerManager.Instance.addTroops(1);
            PlayerManager.Instance.RefreshUI();
        }

    }
}
