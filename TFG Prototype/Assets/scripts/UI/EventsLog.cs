using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventsLog : MonoBehaviour
{
    class Log
    {
        public GameObject obj;
        public float time;
    }
    List<Log> logs = new List<Log>();
    public GameObject logPrefab;
    private void Start()
    {
    }
    public void AddLog(string text)
    {
        GameObject newLog = Instantiate(logPrefab, this.transform);
        Log log = new Log();
        log.obj = newLog;
        log.obj.GetComponent<TextMeshProUGUI>().text = text;
        log.time = 0.0f;
        for (int i = 0; i < logs.Count; i++)
        {
            Log temp = logs[i];
            Vector3 position = temp.obj.transform.localPosition;
            position.y += 60f;
            temp.obj.transform.localPosition = position;
        }
        logs.Add(log);

    }
    private void Update()
    {
        for(int i = 0; i < logs.Count; i++)
        {
            logs[i].time += Time.deltaTime;
            if(logs[i].time > 8.0f)
            {
                Destroy(logs[i].obj);
                logs.RemoveAt(i);
                i--;

            }
            else
            {
                if (logs[i].time > 4.0f)
                {
                    Color32 color = logs[i].obj.GetComponent<TextMeshProUGUI>().color;
                    color.a = (byte)((4- (logs[i].time - 4)) / 4 * 255);
                    logs[i].obj.GetComponent<TextMeshProUGUI>().color = color;

                }

                Vector3 position = logs[i].obj.transform.localPosition;
                position.y += 10f * Time.deltaTime;
                logs[i].obj.transform.localPosition = position;
            }
   
            
        }
    }
}
