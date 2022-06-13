using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectCharButton : MonoBehaviour
{
    public TextMeshProUGUI name;
    private Character privChar;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
