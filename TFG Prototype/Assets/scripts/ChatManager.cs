using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{

    private static ChatManager _instance;
    public static ChatManager Instance { get { return _instance; } }

    struct job
    {
        public string name;
        public string[] activity;
    }

    enum ChatType
    {
        intro,
        work,
    }
    struct PersonalInfo
    {
        public Mood tag;
        public string text;
        public ChatType type;
    }

    List<PersonalInfo> personalInfoList;
    List<job> jobInfoList;

    [HideInInspector]
    public Npc focusedNPC;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            _instance = this;

        Debug.Log("Awake");
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        jobInfoList = FillJobInfoList();
        personalInfoList = FillPersonalInfoList();
    }

    public string generatePersonalInfo()
    {
        List<PersonalInfo> infoList;
        if (focusedNPC.introducedHimself)
        {
            infoList = ReturnPersonalInfoList(ChatType.work, Mood.maxMoods);
        }
        else
        {
            infoList = ReturnPersonalInfoList(ChatType.intro, focusedNPC.npcMood);

            focusedNPC.introducedHimself = true;

        }


        int randomPos = Random.Range(0, infoList.Count);
        string ret = infoList[randomPos].text;
        ret = ret.Replace("myname", focusedNPC.npcName);

        return ret;
    }
    public string returnCompicityText(int value)
    {
        string ret = "";

        if(value == 0)
        {
            ret = "I'm so happy with this city views on our replace1";
        }
        else if (value > 0 && value < 2)
        {
            ret = "i'm ok with this city views on our replace1, but we could use more replace2";
        }
        else
        {
            ret = "I hate this city views on replace1, we shoud use more replace2";
        }
        return ret;
    }
    public string generateCityInfo()
    {
        string ret = "";
        if (focusedNPC.introducedCity)
        {
            int rand = Random.Range(0, 8);
            switch (rand)
            {
                case 0:
                    {
                        ret = returnCompicityText(PoliticsGenerator.checkLeadership(focusedNPC.profile.leadership, focusedNPC.cityInfo.profile.leadership));
                        ret = ret.Replace("replace1", returnLeadershipView(focusedNPC.cityInfo.profile.leadership));
                        ret = ret.Replace("replace2", returnLeadershipView(focusedNPC.profile.leadership));
                    }
                    break;
                case 1:
                    {
                        ret = returnCompicityText(PoliticsGenerator.checkReligion(focusedNPC.profile.religion, focusedNPC.cityInfo.profile.religion));
                        ret = ret.Replace("replace1", returnReligiousView(focusedNPC.cityInfo.profile.religion));
                        ret = ret.Replace("replace2", returnReligiousView(focusedNPC.profile.religion));
                    }
                    break;
                case 2:
                    {
                        ret = returnCompicityText(PoliticsGenerator.checkForeign(focusedNPC.profile.foreign, focusedNPC.cityInfo.profile.foreign));
                        ret = ret.Replace("replace1", returnForeignView(focusedNPC.cityInfo.profile.foreign));
                        ret = ret.Replace("replace2", returnForeignView(focusedNPC.profile.foreign));
                    }
                    break;
                case 3:
                    {
                        ret = returnCompicityText(PoliticsGenerator.checkEconomy(focusedNPC.profile.economy, focusedNPC.cityInfo.profile.economy));
                        ret = ret.Replace("replace1", returnEconomyView(focusedNPC.cityInfo.profile.economy));
                        ret = ret.Replace("replace2", returnEconomyView(focusedNPC.profile.economy));
                    }
                    break;
                case 4:
                    {
                        ret = returnCompicityText(PoliticsGenerator.checkMilitary(focusedNPC.profile.military, focusedNPC.cityInfo.profile.military));
                        ret = ret.Replace("replace1", returnMilitaryView(focusedNPC.cityInfo.profile.military));
                        ret = ret.Replace("replace2", returnMilitaryView(focusedNPC.profile.military));
                    }
                    break;
                case 5:
                    {
                        ret = returnCompicityText(PoliticsGenerator.checkCulture(focusedNPC.profile.cultural, focusedNPC.cityInfo.profile.cultural));
                        ret = ret.Replace("replace1", returnCulturalView(focusedNPC.cityInfo.profile.cultural));
                        ret = ret.Replace("replace2", returnCulturalView(focusedNPC.profile.cultural));
                    }
                    break;
                case 6:
                    {
                        ret = returnCompicityText(PoliticsGenerator.checkIntellectual(focusedNPC.profile.intellectual, focusedNPC.cityInfo.profile.intellectual));
                        ret = ret.Replace("replace1", returnIntellectualView(focusedNPC.cityInfo.profile.intellectual));
                        ret = ret.Replace("replace2", returnIntellectualView(focusedNPC.profile.intellectual));
                    }
                    break;
                case 7:
                    {
                        ret = returnCompicityText(PoliticsGenerator.checkJustice(focusedNPC.profile.justice, focusedNPC.cityInfo.profile.justice));
                        ret = ret.Replace("replace1", returnJudicialView(focusedNPC.cityInfo.profile.justice));
                        ret = ret.Replace("replace2", returnJudicialView(focusedNPC.profile.justice));

                    }
                    break;
            }
          
        }
        else
        {
            ret = "This is city, I'm glad you visited us";
            ret = ret.Replace("city", focusedNPC.cityInfo.sceneName);

            focusedNPC.introducedCity = true;

        }

        return ret;
    }
    public string generateRumours()
    {
        string ret = "";
        if (focusedNPC.cityInfo.QuestGiver.name == focusedNPC.name && !QuestManager.Instance.activeQuests.ContainsValue(focusedNPC))
        {
            QuestManager.Quest newQuest = QuestManager.Instance.GenerateQuest(focusedNPC.name);
            QuestManager.Instance.AddQuest(newQuest, focusedNPC);

            ret = "Are you willing to help me? Please, go and doquest";
            ret = ret.Replace("doquest", newQuest.questDescription);

        }
        else
        {
            ret = "I heard that npc was in need of help to solve a problem";
            ret = ret.Replace("npc", focusedNPC.cityInfo.QuestGiver.name);
        }

        return ret;
    }

    

    private List<PersonalInfo> ReturnPersonalInfoList(ChatType type, Mood tag)
    {
        List<PersonalInfo> temp = new List<PersonalInfo>();

        for(int i = 0; i < personalInfoList.Count; ++i)
        {
            if (personalInfoList[i].type == type && (personalInfoList[i].tag == tag || personalInfoList[i].tag == Mood.maxMoods))
                temp.Add(personalInfoList[i]);
        }
        if (temp.Count == 0)
            temp.Add(personalInfoList[0]);

        return temp;
    }

    private PersonalInfo createPersonalInfo(string sentence, ChatType type, Mood tag)
    {
        PersonalInfo newInfo;
        newInfo.tag = tag;
        newInfo.text = sentence;
        newInfo.type = type;
        return newInfo;
    }
    private string returnLeadershipView(Leadership leadership)
    {
        string ret = "";
        switch (leadership)
        {
            case Leadership.Representation:
                ret = "democratic representation";
                break;
            case Leadership.Monarchy:
                ret = "monarchical rule";
                break;
            case Leadership.Theocracy:
                ret = "religious representation in the government";
                break;
            case Leadership.Statocracy:
                ret = "military representation in the government";
                break;
        }
        return ret;
    }
    private string returnReligiousView(Religion religion)
    {
        string ret = "";
        switch (religion)
        {
            case Religion.Atheism:
                ret = "state atheism";
                break;
            case Religion.CollectiveFaith:
                ret = "participation from everyone in our religion";
                break;
            case Religion.OrganizedReligion:
                ret = "institutional religion";
                break;
            case Religion.Zealotry:
                ret = "religious fervour";
                break;
        }
        return ret;
    }
    private string returnForeignView(Foreign foreign)
    {
        string ret = "";
        switch (foreign)
        {
            case Foreign.Isolationist:
                ret = "isolationism";
                break;
            case Foreign.Interventionist:
                ret = "attention to foreign affairs";
                break;
            case Foreign.Internationalist:
                ret = "poilitical interference";
                break;
            case Foreign.Imperialist:
                ret = "military expansionism";
                break;
        }
        return ret;
    }
    private string returnEconomyView(Economy economy)
    {
        string ret = "";
        switch (economy)
        {
            case Economy.Protectionism:
                ret = "intervention in the prices";
                break;
            case Economy.Mercantilism:
                ret = "taxes to foreign merchants";
                break;
            case Economy.FreeTrade:
                ret = "market autoregulations";
                break;
            case Economy.Slavery:
                ret = "slavery";
                break;
        }
        return ret;
    }
    private string returnMilitaryView(Military military)
    {
        string ret = "";
        switch (military)
        {
            case Military.StandingArmy:
                ret = "nation's standing army";
                break;
            case Military.Conscription:
                ret = "conscription laws";
                break;
            case Military.Militia:
                ret = "militias";
                break;
            case Military.Pacifism:
                ret = "nation's disarment";
                break;
        }
        return ret;
    }
    private string returnCulturalView(Cultural culture)
    {
        string ret = "";
        switch (culture)
        {
            case Cultural.Aesthetics:
                ret = "aesthetical values";
                break;
            case Cultural.Contemplation:
                ret = "philosophical contemplation";
                break;
            case Cultural.Populism:
                ret = "hero worshipping";
                break;
            case Cultural.Hegemony:
                ret = "cultural pride";
                break;
        }
        return ret;
    }

    private string returnIntellectualView(Intellectual intellectual)
    {
        string ret = "";
        switch (intellectual)
        {
            case Intellectual.Antiquarian:
                ret = "focusing on historical events";
                break;
            case Intellectual.Literary:
                ret = "focusing on literacy";
                break;
            case Intellectual.Scholarly:
                ret = "focusing on natural laws";
                break;
            case Intellectual.Mechanical:
                ret = "focusing on engineering";
                break;
        }
        return ret;
    }
    private string returnJudicialView(Justice justice)
    {
        string ret = "";
        switch (justice)
        {
            case Justice.Frontier:
                ret = "survival of the fittest";
                break;
            case Justice.Vigilantism:
                ret = "focusing on bounty hunters";
                break;
            case Justice.Penitentiary:
                ret = "focusing on our prisons";
                break;
            case Justice.Ordeal:
                ret = "tortures and executions";
                break;
        }
        return ret;
    }
    private List<PersonalInfo> FillPersonalInfoList()
    {
        List<PersonalInfo> temp = new List<PersonalInfo>();
        temp.Add(createPersonalInfo("I'm myname, nice to meet you", ChatType.intro, Mood.joy));
        temp.Add(createPersonalInfo("Welcome adventurer, my name is myname, may I know yours?", ChatType.intro, Mood.joy));
        temp.Add(createPersonalInfo("Greetings traveller, I'm myname", ChatType.intro, Mood.joy));

        temp.Add(createPersonalInfo("Who are you? A traveller, if I should guess by your smell", ChatType.intro, Mood.disgust));
        temp.Add(createPersonalInfo("Look I'm very busy for petty chit-chatting right now, go bother someone else", ChatType.intro, Mood.disgust));
        temp.Add(createPersonalInfo("You must be that adventurer everyone is talking about, I thought you would be more impressive", ChatType.intro, Mood.disgust));
        temp.Add(createPersonalInfo("If you insist... my name is myname, can't say the pleasure is mine...", ChatType.intro, Mood.disgust));

        temp.Add(createPersonalInfo("Oh sorry, I was a little distracted... my name is myname", ChatType.intro, Mood.surprise));
        temp.Add(createPersonalInfo("Mhmmmmm, oh hi! You took me by surprise, I'm myname", ChatType.intro, Mood.surprise));
        temp.Add(createPersonalInfo("Who do we have here? I'm myname, didn't expect any visit right now.", ChatType.intro, Mood.surprise));

        temp.Add(createPersonalInfo("Hi, I'm myname, excuse me if I'm not on the mood, how may I help you?", ChatType.intro, Mood.sadness));
        temp.Add(createPersonalInfo("What now, have you come here to make my day even worse?", ChatType.intro, Mood.sadness));
        temp.Add(createPersonalInfo("Please, I told you that I don't have any more money left... Oh, who are you?", ChatType.intro, Mood.sadness));

        for (int i = 0; i < jobInfoList.Count; ++i)
        {
            

            for (int j = 0; j < jobInfoList[i].activity.Length; ++j)
            {
                string work = "I'm a work, ido";
                work = work.Replace("work", jobInfoList[i].name);
                work = work.Replace("ido", jobInfoList[i].activity[j]);

                temp.Add(createPersonalInfo(work, ChatType.work, Mood.maxMoods));
            }
        }
        

        return temp;
    }

    private List<job> FillJobInfoList()
    {
        List<job> temp = new List<job>();
        job newJob;
        newJob.name = "smith";
        newJob.activity = new string[]
        {
            "I can forge everything, from horseshoes to the finnest swords","in my workshop you'll only find the finnest steel", "if you are interest in weapons and armor, visit me from time to time",
        };
        temp.Add(newJob);

        newJob.name = "book seller";
        newJob.activity = new string[]
        {
            "I have great pieces of knowledge at my shop, come and see them", "I'm normally travelling from town to town like you, gathering every interesting book I can find", "gathering literature is more of an obsession than a job for me",
        };
        temp.Add(newJob);

        newJob.name = "merchant";
        newJob.activity = new string[]
        {
           "I sell my products at the town center, the finnest goods around here If you ask me", "even people of your type must enjoy good wines and robes from time to time, am I right?", "I never sleep on a good offer, a good trader never rests!",
        };
        temp.Add(newJob);

        newJob.name = "guard";
        newJob.activity = new string[]
        {
           "I serve his majesty, no one will break his law on my watch", "do not break the law under any circumstance, or we'll have to intervene", "if you have something to report head on to the town quarters",
        };
        temp.Add(newJob);

        newJob.name = "tailor";
        newJob.activity = new string[]
        {
           "I can make you newer and better clothes than those rags you are wearing ", "I mostly work on demand, but I have some discarded pieces you can look upon in my workshop", "if you need clothes fit for a king, you have come to the right person",
        };
        temp.Add(newJob);

        newJob.name = "inkeeper";
        newJob.activity = new string[]
        {
           "don't waste more time and come to my place to have some fun", "my inn is more than a place to become smashed, it's where the people in this town gather to forget about their lifes", "you should head to my inn, today I received a few barrels of the finnest ale",
        };
        temp.Add(newJob);

        return temp;
    }
}
