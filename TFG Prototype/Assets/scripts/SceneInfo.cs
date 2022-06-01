using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfo
{
    public string sceneName;
    public List<npc> sceneNpcs;
    public List<SceneInfo> SceneInfos;

    public List<Family> families;
    public npc QuestGiver;
    public PoliticProfile profile;
    public List<string> dailyEvents;

    public void MorningEvent()
    {
        dailyEvents = new List<string>();
        for (int i = 0; i < sceneNpcs.Count; ++i)
        {
            npc tempNpc = sceneNpcs[i];
            dailyEvents.Add(ReturnMorningEvents(out  tempNpc.stress).Replace("Name", tempNpc.name));
            sceneNpcs[i] = tempNpc;

            //Convertir npc a clase npc data o algo asi, 
            //para que puedas asignar estres a la copia dentro de la familia
            //es posible que solo upgradeando a clase, ya altere el valor dentro de familia, 
            //puesto a que puedes modificar valores como stress, y no tienes que hacer esta parafernalia
        }
        foreach (string item in dailyEvents)
        {
            Debug.Log(item);

        }
    }

    public void Work()
    {

    }

    public void Entertainment()
    {

    }

    string ReturnMorningEvents(out int stress)
    {
        string ret = "";
        stress = 0;

        switch (Random.Range(0, 10))
        {
            case 0:
                ret = "Name Prayed after waking up and is now feeling contented"; stress = Random.Range(0, 5); break;
            case 1:
                ret = "Name Read a chapter of their favorite book"; stress = Random.Range(1, 5); break;
            case 2:
                ret = "Name Had breakfast with their loved ones"; stress = Random.Range(1, 5); break;
            case 3:
                ret = "Name Received a letter from a relative"; stress = Random.Range(1, 5); break;
            case 4:
                ret = "Name Saw a dragon flying in the horizon"; stress = Random.Range(1, 5); break;
            case 5:
                ret = "Name Encountered some friendly forest elves"; stress = Random.Range(1, 5); break;
            case 6:
                ret = "Name Took some time to take a walk on the city centre"; stress = Random.Range(1, 5); break;
            case 7:
                ret = "Name Finished writting their book"; stress = Random.Range(1, 5); break;
            case 8:
                ret = "Name Listened to someone playing their favourite song"; stress = Random.Range(1, 5); break;
            case 9:
                ret = "Name Bought a beautiful asset in the city centre"; stress = Random.Range(1, 5); break;
        }

        return ret;
    }
}
