using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterInfoUI : MonoBehaviour
{
    public Character character;
    public TextMeshProUGUI description;
    public TextMeshProUGUI story;
    public int price;
    // Start is called before the first frame update
    public void SetDesAndBackstory()
    {
        description.text = character.description;
        for (int i = 0; i < character.story.addedEvents.Count; ++i)
        {
            story.text += "\n";
            story.text += character.story.addedEvents[i].cause;
            story.text += character.story.addedEvents[i].consequence;

        }

    }
    public void BuyCharacter()
    {
        if (PlayerManager.Instance.canBuy(price) && PlayerManager.Instance.recruitedCharacters.Count < PlayerManager.Instance.maxCharacterSlots)
        {

            if (!PlayerManager.Instance.recruitedCharacters.Contains(character))
            {
                PlayerManager.Instance.addGold(-price);

                PlayerManager.Instance.addCharacter(character);

                PlayerManager.Instance.RefreshUI();
            }
               
        }

    }
}
