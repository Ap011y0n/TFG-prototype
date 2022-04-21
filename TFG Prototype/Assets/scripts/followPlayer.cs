using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
      

    }
}
