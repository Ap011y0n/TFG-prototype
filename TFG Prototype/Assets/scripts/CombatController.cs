using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    int turn = 0;
    public List<HexUnit> units;

    public void ResetTurn()
    {
        turn = 0;
    }
    public void EndTurn()
    {
        turn++;
        for(int i = 0; i < units.Count; i++)
        {
            units[i].ResetActions();
        }
    }
}
