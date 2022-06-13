using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SelectCharButton : MonoBehaviour
{
    public TextMeshProUGUI name;
    private Character privChar;
    public PlayerUi playerUi;
    public Image heroImage;

    public Character character
    {
        set
        {
            privChar = value;
            name.text = privChar.Name;
        }
        get
        {
            return privChar;
        }
    }
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
    }
}
