using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool UIfocusedBool = false;

    private static PlayerController _instance;
    public static PlayerController Instance { get { return _instance; } }
    public GameDirector director;

    void Awake()
    {
            _instance = this;

        Debug.Log("Awake");
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UIfocusedBool) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                agent.destination = hit.point;
            }
        }

        float velocity = agent.velocity.magnitude;
        if (velocity > 0 && director)
        {
            director.AdvanceProgressBar();
        }
    }
        public void UIFocused()
    {
        UIfocusedBool = !UIfocusedBool;
    }
    public void goToPos(Vector3 pos)
    {
        agent.destination = pos;

    }
}
