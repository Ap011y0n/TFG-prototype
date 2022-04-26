using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public Vector2 upperLimit;
    public Vector2 lowerLimit;

    void Start()
    {
        transform.position = target.position + offset;
    }

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        Vector3 pos;
        if (upperLimit.x < transform.position.x)
            transform.position 

    }
}
