using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTroopUi : MonoBehaviour
{
    public int unit;
    public PlayerUi playerUi;
    public Image heroImage;
    public void SelectThisUnit()
    {
        if (playerUi.selectedCharacter != -1)
        {
            PlayerManager.Instance.SetCommander(unit, playerUi.selectedCharacter);
            
        
            playerUi.selectedUnit = -1;
            playerUi.selectedCharacter = -1;
            playerUi.RefreshCharactersAndTroops();

        }
        playerUi.selectedUnit = unit;

    }
}
