using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCameraMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public Vector2 upperLimit;
    public Vector2 lowerLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");

       transform.Translate(new Vector3(xAxisValue * speed, zAxisValue * speed, 0.0f));

        Vector3 newPos = transform.position;

        if (newPos.x > upperLimit.x)
            newPos.x = upperLimit.x;
        else if (newPos.x < lowerLimit.x)
            newPos.x = lowerLimit.x;

        if (newPos.z > upperLimit.y)
            newPos.z = upperLimit.y;
        else if (newPos.z < lowerLimit.y)
            newPos.z = lowerLimit.y;

        transform.position = newPos;
    }
}
