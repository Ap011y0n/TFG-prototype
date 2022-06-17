using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showUi : MonoBehaviour
{
    public GameObject UI;
    GameObject playerRef;
    public PlayerUi canvas;
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

    }
    public void showWindow()
    {
        UI.SetActive(!UI.activeSelf);
        playerRef.GetComponent<PlayerController>().UIfocusedBool = UI.activeSelf;
        playerRef.GetComponent<PlayerController>().agent.destination = playerRef.transform.position;

    }
    public void showWindowShop()
    {
        UI.SetActive(!UI.activeSelf);
        playerRef.GetComponent<PlayerController>().UIfocusedBool = UI.activeSelf;
        playerRef.GetComponent<PlayerController>().agent.destination = playerRef.transform.position;
        canvas.isInStore = UI.activeSelf;

    }
    public void showWindowInventory()
    {
        UI.SetActive(!UI.activeSelf);
        playerRef.GetComponent<PlayerController>().UIfocusedBool = UI.activeSelf;
        playerRef.GetComponent<PlayerController>().agent.destination = playerRef.transform.position;
        canvas.isInInventory = UI.activeSelf;

    }
}
