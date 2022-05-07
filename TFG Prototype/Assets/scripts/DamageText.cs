using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float maxLifeTime = 3;
    float lifeTime = 0;
    public float movementSpeed = 10;
    void Update()
    {
        lifeTime += Time.deltaTime;
        Vector3 position = transform.localPosition;
        position.y += movementSpeed * Time.deltaTime;
        transform.localPosition = position;
        if (lifeTime >= maxLifeTime)
            Destroy(this.gameObject);
    }
}
