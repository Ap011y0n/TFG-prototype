using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public Light light;
    public Color day;
    public Color night;
    public Color sunset;
    public void SetLight(float time)
    {
        float lerpValue = 0f;
        if(time < 300f)
        {
            lerpValue = 1f;

        }
        if (time >= 300f && time <= 540f)
        {
            lerpValue = 1f - (time-300) / 240f;

        }
        if (time >= 1080f)
        {
            lerpValue = (time-1080f) / 300f;
        }
        light.color = Color.Lerp(day, night, lerpValue);
    }
}
