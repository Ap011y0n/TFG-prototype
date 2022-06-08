using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StressBar : MonoBehaviour
{
    public Image stressBarImage;
    public TextMeshProUGUI npcName;
    public void InitStressBarUi(float value, string name)
    {
        if (value > 0)
            stressBarImage.color = Color.red;

        stressBarImage.fillAmount = Mathf.Clamp(Mathf.Abs(value) / 50, 0f, 1f);
        npcName.text = name;
    }
}
