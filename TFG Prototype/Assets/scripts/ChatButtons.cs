using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatButtons : MonoBehaviour
{
    public GameObject answers;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void askPersonalInfo()
    {
        answers.GetComponent<Text>().text = ChatManager.Instance.generatePersonalInfo();
    }

    public void askCityInfo()
    {
        answers.GetComponent<Text>().text = ChatManager.Instance.generateCityInfo();
    }

    public void askRumours()
    {
        answers.GetComponent<Text>().text = ChatManager.Instance.generateRumours();
    }
}
