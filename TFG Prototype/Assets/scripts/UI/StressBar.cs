using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StressBar : MonoBehaviour
{
    public Image stressBarImage;
    public TextMeshProUGUI npcName;
    public GameObject panel;
    public TextMeshProUGUI log;
    public string npcHistory;
    public void InitStressBarUi(float value, string name, string history, GameObject panelRef, TextMeshProUGUI logRef)
    {
        if (value > 0)
            stressBarImage.color = Color.red;

        stressBarImage.fillAmount = Mathf.Clamp(Mathf.Abs(value) / 50, 0f, 1f);
        npcName.text = name;
        panel = panelRef;
        log = logRef;
        npcHistory = history;
    }

    public void NameClick()
    {
        if (!panel.activeSelf)
            panel.SetActive(true);
        log.text = npcHistory;
    }
}
