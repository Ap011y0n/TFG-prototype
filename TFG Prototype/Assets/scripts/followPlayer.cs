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
        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        if (newPos.x > upperLimit.x)
            newPos.x = upperLimit.x;
        else if (newPos.x < lowerLimit.x)
            newPos.x = lowerLimit.x;

        if (newPos.z < upperLimit.y)
            newPos.z = upperLimit.y;
        else if (newPos.z > lowerLimit.y)
            newPos.z = lowerLimit.y;

        transform.position = newPos;
    }
}
