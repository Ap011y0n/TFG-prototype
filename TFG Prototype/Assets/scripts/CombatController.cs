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
	public System.Guid guid;
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
				grid.SearchEnemy(enemyUnits[i].Location, 19, out destination, out enemy);
				if (destination != null)
				{
					enemyUnits[i].path = grid.FindPath(enemyUnits[i].Location, destination, 19);
					if (enemyUnits[i].path != null)
					{
						enemyUnits[i].Move();
						enemyUnits[i].Attack(enemy.Unit);
					}
				}
			}
		grid.disableAllHighlights();

		NextTurn();
	}

    public void Load(BattleMapInfo mapInfo)
	{
		guid = mapInfo.guid;
		string path = Path.Combine(Application.dataPath + "/Resources", mapInfo.mapName + ".map");


		//if (System.IO.File.Exists(path))
		//{
			Debug.Log("Loading file at: " + path);
			grid.DeleteEntities();
			TextAsset data = Resources.Load(mapInfo.mapName) as TextAsset;
			Stream s = new MemoryStream(data.bytes);
			BinaryReader reader = new BinaryReader(s);

			//using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
			
				grid.Load(reader);
			
			int num = 1;
		switch (mapInfo.creature)
		{
			default:
				break;
			case HexUnit.unitType.Human:
				{
					num = 40;
				}
				break;
			case HexUnit.unitType.Kelpie:
				{
					num = 40;
				}
				break;
			case HexUnit.unitType.Golem:
				{
					num = 40;
				}
				break;
			case HexUnit.unitType.Wolpertinger:
				{
					num = 40;
				}
				break;
			case HexUnit.unitType.Dragon:
				{
					num = 1;
				}
				break;
			case HexUnit.unitType.Manticore:
				{
					num = 1;
				}
				break;
			case HexUnit.unitType.Ghost:
				{
					num = 40;
				}
				break;
			case HexUnit.unitType.Troll:
				{
					num = 40;
				}
				break;
			case HexUnit.unitType.Giant:
				{
					num = 1;
				}
				break;
		}
		grid.SpawnEntities(enemyPrefab, mapInfo.creature, num, mapInfo.enemiesToSpawn);
		//}
		//else
		//{
		//	Debug.LogWarning("File at: " + path + " does not exist");
		//}
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
			grid.SpawnEntities(enemyPrefab, HexUnit.unitType.Troll, 40);
		}
		else
		{
			Debug.LogWarning("File at: " + path + " does not exist");
		}
	}
}
