using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Family 
{
    public string name;
    public List<NpcData> members;
    public Dictionary<Family, int> relationships;
    public int stress = 0;

    public NpcData returnRebelMember(PoliticProfile cityProfile, out int politic)
    {
        NpcData ret = members[0];
        politic = members[0].returnConflictivePolitics(cityProfile);

        for (int i = 1; i < members.Count; ++i)
        {
            if (members[i].stress > ret.stress)
            {
                int res = members[i].returnConflictivePolitics(cityProfile);
                if (res != -1)
                {
                    ret = members[i];
                    politic = res;
                }
            }
                
        }
        
        return ret;
    }
}
