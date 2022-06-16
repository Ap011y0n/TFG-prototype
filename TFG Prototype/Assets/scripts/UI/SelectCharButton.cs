using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SelectCharButton : MonoBehaviour
{
    public TextMeshProUGUI name;
    public int character;
    public PlayerUi playerUi;
    public Image heroImage;
    public GameObject characterInfoUI;
    public GameObject characterInfoUIInventory;
    public void SelectThisChar()
    {
        //if (playerUi.selectedUnit != null)
        //{
        //    playerUi.selectedUnit.character = character;
        //    for (int i = 0; i < PlayerManager.Instance.recruitedUnits.Count; ++i)
        //    {
        //        if (PlayerManager.Instance.recruitedUnits[i] == playerUi.selectedUnit)
        //            PlayerManager.Instance.recruitedUnits[i].character = character;
        //    }
        //    playerUi.selectedUnit = null;
        //    playerUi.selectedCharacter = null;
        //    playerUi.RefreshCharactersAndTroops();
        //}
        //else

        playerUi.selectedCharacter = character;

        if (playerUi.ConfirmBuyUI != null)
            Destroy(playerUi.ConfirmBuyUI.gameObject);
        if (playerUi.InfoUI != null)
            Destroy(playerUi.InfoUI.gameObject);
        if(playerUi.isInStore)
        playerUi.InfoUI = Instantiate(characterInfoUI, transform.parent.transform).GetComponent<CharacterInfoUI>();
        else
            playerUi.InfoUI = Instantiate(characterInfoUIInventory, transform.parent.transform).GetComponent<CharacterInfoUI>();

        playerUi.InfoUI.character = PlayerManager.Instance.recruitedCharacters[character];
      //  playerUi.InfoUI.price = int.Parse(price.text);
        playerUi.InfoUI.SetDesAndBackstory();
    }
}
