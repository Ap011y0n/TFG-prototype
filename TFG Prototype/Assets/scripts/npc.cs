using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc : MonoBehaviour
{
    public string npcName;
    public TextMesh displayName;
    // Start is called before the first frame update
    void Start()
    {
        displayName = displayName.GetComponent<TextMesh>();
        displayName.text = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
