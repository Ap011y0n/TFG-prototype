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
    public PlayerUi playerUi;
    public Vector3 buyUiPos;
    public GameObject BuyUIPrefab;
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
        //if (PlayerManager.Instance.canBuy(int.Parse(price.text)))
        //{
        //    PlayerManager.Instance.addGold(-int.Parse(price.text));
        //    PlayerManager.Instance.addCharacter(character);
        //    PlayerManager.Instance.RefreshUI();
        //}
        if (playerUi.ConfirmBuyUI != null)
            Destroy(playerUi.ConfirmBuyUI.gameObject);
        playerUi.ConfirmBuyUI = null;

        playerUi.ConfirmBuyUI = Instantiate(BuyUIPrefab, transform.parent.transform).GetComponent<CharacterInfoUI>();
        playerUi.ConfirmBuyUI.character = character;
        playerUi.ConfirmBuyUI.price = int.Parse(price.text);
        playerUi.ConfirmBuyUI.SetDesAndBackstory();

    }
}
