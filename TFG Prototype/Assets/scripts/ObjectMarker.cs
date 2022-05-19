using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMarker : MonoBehaviour
{
    public Transform target;
    private GameObject camera;
    public float offset = 1;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(target.position.x,
                                        target.position.y,
                                        target.position.z + 0.5f);
        transform.LookAt(targetPostition);
        Quaternion localRotation = Quaternion.Euler(270, 0f, 0f);
        transform.rotation = transform.rotation * localRotation;

        transform.position = target.position;
        Vector3 posWithinCamera = transform.position;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        if (posWithinCamera.z > camera.transform.position.z + height / 2)
            posWithinCamera.z = camera.transform.position.z + height / 2 - offset;
        else if (posWithinCamera.z < camera.transform.position.z - height / 2)
            posWithinCamera.z = camera.transform.position.z - height / 2 + offset;
        if (posWithinCamera.x > camera.transform.position.x + width / 2)
            posWithinCamera.x = camera.transform.position.x + width / 2 - offset;
        else if (posWithinCamera.x < camera.transform.position.x - width / 2)
            posWithinCamera.x = camera.transform.position.x - width / 2 + offset;

        transform.position = posWithinCamera;
    }
}
