using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTroopUi : MonoBehaviour
{
    public Unit unit;
    public PlayerUi playerUi;
    public Image heroImage;
    public void SelectThisUnit()
    {
        if (playerUi.selectedCharacter != null)
        {
            unit.character = playerUi.selectedCharacter;
            for(int i = 0; i < PlayerManager.Instance.recruitedUnits.Count; ++i)
            {
                if (PlayerManager.Instance.recruitedUnits[i] == unit)
                PlayerManager.Instance.recruitedUnits[i].character = playerUi.selectedCharacter;
            }
            playerUi.selectedUnit = null;
            playerUi.selectedCharacter = null;
            playerUi.RefreshCharactersAndTroops();

        }
        playerUi.selectedUnit = unit;

    }
}
