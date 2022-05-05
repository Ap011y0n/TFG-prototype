using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CombatController : MonoBehaviour
{
    int turn = 0;
	public HexUnit unitPrefab;
	public List<HexUnit> units;
	public HexGrid grid;

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
   
    public void Load(BattleMapInfo mapInfo)
	{
		string path = Path.Combine(Application.dataPath + "/maps", mapInfo.mapName +".map");

		if (System.IO.File.Exists(path))
		{
			Debug.Log("Loading file at: " + path);
			grid.DeleteEntities();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
			{
				grid.Load(reader);
			}
			grid.SpawnEntities(unitPrefab);
		}
		else
		{
			Debug.LogWarning("File at: " + path + " does not exist");
		}
	}

	public void Load()
	{
		string path = Path.Combine(Application.dataPath + "/maps", "test2.map");

		if (System.IO.File.Exists(path))
		{
			Debug.Log("Loading file at: " + path);
			grid.DeleteEntities();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
			{
				grid.Load(reader);
			}
			grid.SpawnEntities(unitPrefab);
		}
		else
		{
			Debug.LogWarning("File at: " + path + " does not exist");
		}
	}
}
