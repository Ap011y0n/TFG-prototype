using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocations : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> questPlaces;

    private void Awake()
    {
        QuestManager.Instance.questPlaces = new List<GameObject>();
        QuestManager.Instance.questPlaces = questPlaces;

    }    
    
}
