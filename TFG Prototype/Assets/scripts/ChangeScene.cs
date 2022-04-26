using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject EnterText;
    public bool savePos = false;
    private bool canEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canEnter)
        {
            canEnter = false;
            SceneManager.LoadScene(gameObject.name);
            if(savePos)
            SceneDirector.Instance.SetPlayerPos(transform.position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnterText.SetActive(true);
            canEnter = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnterText.SetActive(false);
            canEnter = false;
        }
    }
}
