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
	public HexGameUI UI;

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
		if (enemyUnits.Count <= 0)
        {
			UI.ActivateEndUi();
		}
		else
			for (int i = 0; i < enemyUnits.Count; i++)
			{
				HexCell destination, enemy;
				grid.SearchEnemy(enemyUnits[i].Location, 24, out destination, out enemy);
				if (destination != null)
				{
					enemyUnits[i].path = grid.FindPath(enemyUnits[i].Location, destination, 24);
					if (enemyUnits[i].path != null)
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
			int num = 1;
			switch(mapInfo.creature)
            {
				case HexUnit.unitType.BigMonster:
					num = 1;
					break;
				case HexUnit.unitType.SmallMonster:
					num = 40;
					break;
			}
			grid.SpawnEntities(enemyPrefab, mapInfo.creature, num);
		}
		else
		{
			Debug.LogWarning("File at: " + path + " does not exist");
		}
	}

	public void Load()
	{
		Debug.LogError("Warning, this load is only intended for debug purposes");
		string path = Path.Combine(Application.dataPath + "/maps", "SingleEntityMap.map");

		if (System.IO.File.Exists(path))
		{
			Debug.Log("Loading file at: " + path);
			grid.DeleteEntities();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
			{
				grid.Load(reader);
			}
			grid.SpawnEntities(enemyPrefab, HexUnit.unitType.BigMonster, 1);
		}
		else
		{
			Debug.LogWarning("File at: " + path + " does not exist");
		}
	}
}
