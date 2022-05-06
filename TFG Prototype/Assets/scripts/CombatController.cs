using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CombatController : MonoBehaviour
{
    int turn = 0;
	public HexEnemy enemyPrefab;
	public HexUnit unitPrefab;
	public List<HexUnit> units;
	public List<HexUnit> playerUnits;
	public List<HexEnemy> enemyUnits;
	public HexGrid grid;

	public void ResetTurn()
    {
        turn = 0;
    }
	public int getCurrentTurn()
    {
		return turn;
    }
    public void NextTurn()
    {
        turn++;
        for(int i = 0; i < units.Count; i++)
        {
            units[i].ResetActions();
        }
    }
   
	public void EndPlayerTurn()
    {
		for (int i = 0; i < playerUnits.Count; i++)
		{
			playerUnits[i].EndActions();
		}
		EnemyTurn();
	}

	public void EnemyTurn()
    {
		for (int i = 0; i < enemyUnits.Count; i++)
		{
			HexCell destination, enemy;
			grid.SearchEnemy(enemyUnits[i].Location, 24, out destination, out enemy);
			if(destination != null)
            {
				enemyUnits[i].path = grid.FindPath(enemyUnits[i].Location, destination, 24);
				if(enemyUnits[i].path != null)
                {
					enemyUnits[i].Move();
					enemyUnits[i].Attack(enemy.Unit);
                }
			}
		}
		NextTurn();
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
			grid.SpawnEntities(enemyPrefab);
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
			grid.SpawnEntities(enemyPrefab);
		}
		else
		{
			Debug.LogWarning("File at: " + path + " does not exist");
		}
	}
}
