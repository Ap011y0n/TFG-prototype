using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCameraMovement : MonoBehaviour
{
    public float speed = 2.0f;
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
        
    }
}
