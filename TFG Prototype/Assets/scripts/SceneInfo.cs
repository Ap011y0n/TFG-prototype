using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfo
{
    public string sceneName;
    public List<NpcData> sceneNpcs;
    public List<SceneInfo> SceneInfos;

    public List<Family> families;
    public NpcData QuestGiver;
    public PoliticProfile profile;
    public List<string> dailyEvents;
    public static List<DailyEvent> morningEvents;
    public struct DailyEvent
    {
        public Vector2Int stress;
        public Mood mood;
        public string text;
    }
    public void MorningEvent()
    {
        dailyEvents = new List<string>();
        for (int i = 0; i < sceneNpcs.Count; ++i)
        {
            DailyEvent randomEvent = ReturnMorningEvent();
            sceneNpcs[i].stress += Random.Range(randomEvent.stress.x, randomEvent.stress.y);
            string eventText = randomEvent.text;
            sceneNpcs[i].mood = randomEvent.mood;
            dailyEvents.Add(eventText.Replace("Name", sceneNpcs[i].name));

        }
        //foreach (string item in dailyEvents)
        //{
        //    Debug.Log(item);

        //}
    }
    public void CheckTantrums()
    {
        int politic = 0;
        for(int i = 0; i < families.Count; ++i)
        {
            int totalStress = 0;
            for(int j = 0; j < families[i].members.Count; ++j)
            {
                totalStress += families[i].members[j].stress;
            }
            //  if(Random.Range( 0, totalStress / families[i].members.Count) > 1)
            if ((totalStress / families[i].members.Count) > 1)
            {
                NpcData instigator;
              if( GatherSupport(families[i], out politic, out instigator))
                {
                    launchCoup(politic, instigator);
                }

            }

        }
    }
    void launchCoup(int politic, NpcData instigator)
    {
        string log = "Coup launched by " + instigator.name  + "of the " 
            + instigator.family.name + " family, with enough popular support, " + sceneName + " politic1 is now politic2";
        switch(politic)
        {
            case 1:
                log = log.Replace("politic1", PoliticsGenerator.returnLeadershipView(profile.leadership));
                log = log.Replace("politic2", PoliticsGenerator.returnLeadershipView(instigator.profile.leadership));
                if(profile.leadership == instigator.profile.leadership)
                {
                    Debug.Log("broo");
                }
                profile.leadership = instigator.profile.leadership;

                break;
            case 2:
                log = log.Replace("politic1", PoliticsGenerator.returnLeadershipView(profile.leadership));
                log = log.Replace("politic2", PoliticsGenerator.returnLeadershipView(instigator.profile.leadership));
                profile.foreign = instigator.profile.foreign;
                break;
            case 3:
                log = log.Replace("politic1", PoliticsGenerator.returnReligiousView(profile.religion));
                log = log.Replace("politic2", PoliticsGenerator.returnReligiousView(instigator.profile.religion));
                profile.religion = instigator.profile.religion;
                break;
            case 4:
                log = log.Replace("politic1", PoliticsGenerator.returnJudicialView(profile.justice));
                log = log.Replace("politic2", PoliticsGenerator.returnJudicialView(instigator.profile.justice));
                profile.justice = instigator.profile.justice;
                break;
            case 5:
                log = log.Replace("politic1", PoliticsGenerator.returnMilitaryView(profile.military));
                log = log.Replace("politic2", PoliticsGenerator.returnMilitaryView(instigator.profile.military));
                profile.military = instigator.profile.military;
                break;
            case 6:
                log = log.Replace("politic1", PoliticsGenerator.returnEconomyView(profile.economy));
                log = log.Replace("politic2", PoliticsGenerator.returnEconomyView(instigator.profile.economy));
                profile.economy = instigator.profile.economy;
                break;
            case 7:
                log = log.Replace("politic1", PoliticsGenerator.returnCulturalView(profile.cultural));
                log = log.Replace("politic2", PoliticsGenerator.returnCulturalView(instigator.profile.cultural));
                profile.cultural = instigator.profile.cultural;
                break;
            case 8:
                log = log.Replace("politic1", PoliticsGenerator.returnIntellectualView(profile.intellectual));
                log = log.Replace("politic2", PoliticsGenerator.returnIntellectualView(instigator.profile.intellectual));
                profile.intellectual = instigator.profile.intellectual;
                break;

        }
        Debug.Log(log);
    }
    bool GatherSupport(Family instigators, out int politic, out NpcData instigator)
    {
        instigator = instigators.returnRebelMember(profile, out politic);
        bool succeeded = false;

        if (politic == -1)
        {
            return succeeded;
        }
        List<Family> supporters = new List<Family>();

        for (int i = 0; i < families.Count; ++i)
        {
            int totalStress = 0;
            for (int j = 0; j < families[i].members.Count; ++j)
            {
                totalStress += families[i].members[j].stress;
            }
           
            if((totalStress / families[i].members.Count) > 1)
            {
                supporters.Add(families[i]);
            }

        }
        if(supporters.Count > families.Count/2)
        {
            succeeded = true;
        }
        return succeeded;
    }

    public void Work()
    {

    }

    public void Entertainment()
    {

    }

    DailyEvent ReturnMorningEvent()
    {
        return morningEvents[Random.Range(0, morningEvents.Count)]; 
    }
    public static void fillMorningEvents()
    {
        morningEvents = new List<DailyEvent>();
        DailyEvent newEvent;
        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name prayed after waking up, they were filled with joy";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(3, 6);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name prayed after waking up, but no one answered their prayings, they felt disgust";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.surprise;
        newEvent.text = "Name prayed after waking up, they got a signal which surprised them";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name prayed after waking up, but no one answered their prayings, they felt sad";
        morningEvents.Add(newEvent);
        // ******************************
        newEvent.stress = new Vector2Int(3, 6);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name read a chapter of their favorite book, but their favourite character died, they felt anger against at the autor";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name read a chapter of their favorite book, but after a character died, they saddened";

        morningEvents.Add(newEvent);
        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name read a chapter of their favorite book, they feel blissful";
        morningEvents.Add(newEvent);

        morningEvents.Add(newEvent);
        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.surprise;
        newEvent.text = "Name read a chapter of their favorite book, their expectations were suvberted!";
        morningEvents.Add(newEvent);
        // ******************************
        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name had breakfast with their loved ones, they started the day with a cheerful attitudde";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name had breakfast alone, the quietness made him a bit sad";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(3, 6);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name burned his breakfast, the quietness made him a bit sad";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.surprise;
        newEvent.text = "Name tried new food for breakfast, and they liked it";
        morningEvents.Add(newEvent);
        // ******************************

        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name received a letter from a relative, they remembered happier times";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(3, 5);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name received a letter, remembering of his debt, they immediately got angrier";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name received a letter, a distant relative died, they felt sadness because of the tragic news";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name received a loved letter, its content surprised them";
        morningEvents.Add(newEvent);
        // ******************************

        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name saw a dragon flying in the horizon, this rare sight improved their day";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.surprise;
        newEvent.text = "Name saw a dragon flying in the horizon, even though scared, they felt kinda excited to witness such an event";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(3, 6);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name saw a dragon flying in the horizon, they hates this creatures";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name saw a dragon flying in the horizon, that remembered them of last years incident in a near village";
        morningEvents.Add(newEvent);
        // ******************************

        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name encountered some friendly forest fairies, their funny pranks improved their mood";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.surprise;
        newEvent.text = "Name encountered some friendly forest fairies, they told them some gossips about the townfolks";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(3, 6);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name encountered some forest fairies, which robbed their purse. Nedless to say, they didn't like that prank";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name encountered some forest fairies, they started whispering their mistakes and failures, worsening their mood";
        morningEvents.Add(newEvent);
        // ******************************

        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name took some time to take a walk on the city centre, the peaceful streets calmed their mind";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(3, 6);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name took some time to take a walk on the city centre but suddenly he got mugged by two brigands";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name took some time to take a walk on the city centre, the weather depressed hm a little";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.surprise;
        newEvent.text = "Name took some time to take a walk on the city centre. While in the park, some squirrels approached them";
        morningEvents.Add(newEvent);
        // ******************************

        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name listened to someone playing a song. It was their favourite which brought a smile to their face";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(3, 6);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name listened to someone playing a song which they hate, that worsened their day";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name listened to someone playing a song. It was a sad song which affected their mood";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.surprise;
        newEvent.text = "Name listened to someone playing a song, they were surprised to listen something new";
        morningEvents.Add(newEvent);
        // ******************************

        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name finished writting their book, they are pleased with the final result";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(3, 6);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name finished writting their book, since they had to rush it, they feel very disppleased with the final result";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name finished writting their book, they felt insecure about releasing it";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.surprise;
        newEvent.text = "Name finished writting their book, they didn't expect to finnish it that quickly";
        morningEvents.Add(newEvent);
        // ******************************

        newEvent.stress = new Vector2Int(-5, -2);
        newEvent.mood = Mood.joy;
        newEvent.text = "Name bought some items in the market, their beautiful crafting pleases them";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(3, 6);
        newEvent.mood = Mood.disgust;
        newEvent.text = "Name bought some items in the market, they feel they got scammed";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(1, 4);
        newEvent.mood = Mood.sadness;
        newEvent.text = "Name bought some items in the market, they would've prefered to be sleeping instead of wandering the streets";
        morningEvents.Add(newEvent);

        newEvent.stress = new Vector2Int(-3, 0);
        newEvent.mood = Mood.surprise;
        newEvent.text = "Name bought some items in the market, a specal offert suprised him";
        morningEvents.Add(newEvent);
        // ******************************

    }
}
