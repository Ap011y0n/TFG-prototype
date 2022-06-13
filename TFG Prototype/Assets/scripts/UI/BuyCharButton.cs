using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BuyCharButton : MonoBehaviour
{
    public TextMeshProUGUI price;
    public Character character;
    public Image heroImage;
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.RefreshUI();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BuyCharacter()
    {
        if (PlayerManager.Instance.canBuy(int.Parse(price.text)))
        {
            PlayerManager.Instance.addGold(-int.Parse(price.text));
            PlayerManager.Instance.addCharacter(character);
            PlayerManager.Instance.RefreshUI();
        }

    }
}
