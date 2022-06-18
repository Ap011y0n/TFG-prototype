using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wander : MonoBehaviour
{
    private Vector3 worldTarget;
    public NavMeshAgent agent;
    public float stopDistance = 2f;
    private Rigidbody m_Rigidbody;
    bool stopped = false;
    float timer = 0.0f;
    public float walkTime = 10.0f;
    public float restTime = 15.0f;
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        worldTarget = m_Rigidbody.transform.position;
        agent.autoBraking = true;
        agent.stoppingDistance = stopDistance;
    }

    void Start()
    {
        timer = Random.Range(0, 10);
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = Vector3.Distance(m_Rigidbody.transform.position, agent.destination);
        //if (showdistance)
        //{
        //    Debug.Log(distance);
        //    Debug.Log(m_Rigidbody.transform.position);
        //    Debug.Log(agent.destination);

        //}
        timer += Time.deltaTime;

        if (distance <= 2 && !stopped)
            Wander();
    }
   

    private void Wander()
    {
        Vector3 offset = new Vector3(0, 0, 3);
       
        if(timer < walkTime)
        {
            float radius = 2f;
            Vector3 localTarget = new Vector3(
                Random.Range(-1.0f, 1.0f), 0,
                Random.Range(-1.0f, 1.0f));
            localTarget.Normalize();
            localTarget *= radius;
            localTarget += offset;
            worldTarget =
                transform.TransformPoint(localTarget);
            worldTarget.y = 0f;

            NavMeshPath path = new NavMeshPath();
            if (!agent.CalculatePath(worldTarget, path))
            {
                int rand = Random.Range(1, 2);
                switch (rand)
                {
                    case 1:
                        offset.Set(5, 0, -2);
                        break;
                    case 2:
                        offset.Set(-5, 0, -2);
                        break;

                }

                localTarget.Set(
                    Random.Range(-1.0f, 1.0f), 0,
                    Random.Range(-1.0f, 1.0f));
                localTarget.Normalize();
                localTarget *= radius;
                localTarget += offset;
                worldTarget =
                    transform.TransformPoint(localTarget);
                worldTarget.y = 0f;
            }

            Seek(worldTarget);
        }
        else if (timer < restTime)
        {
            Seek(transform.position);

        }
        else
            timer = 0.0f;




    }
    public void Stop()
    {
        stopped = true;
        Seek(transform.position);
    }
    public void Move()
    {
        stopped = false;
    }
    private void Seek(Vector3 target)
    {
        agent.destination = target;
    }
}
