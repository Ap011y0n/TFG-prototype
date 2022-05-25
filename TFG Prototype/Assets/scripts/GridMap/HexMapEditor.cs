using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class HexMapEditor : MonoBehaviour
{
	public HexGrid hexGrid;
	public bool mapInfoMode = false;
	public bool pathfindingMode = false;
	public bool spawnMode = false;
	public bool movementZoneMode = false;
	HexCell previousCell, searchFromCell, currentCell;
	public HexUnit unitPrefab;
	public HexEnemy enemyPrefab;
	public GameObject colorPanel;
	public GameObject editPanel;
	int activeTerrainTypeIndex;

	void Awake()
	{
	//	SelectColor(0);
	} 

	void Update()
	{
		if(hexGrid.editMode)
        {
			if (mapInfoMode)
			{
				editMapInfo();
			}
			else if (pathfindingMode)
			{
				pathFinding();
			}
			else if (spawnMode)
			{
				EntityMode();
			}
			else if (movementZoneMode)
			{
				movementZone();
			}
		}
		

			
	}

	void editMapInfo()
    {
		
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(inputRay, out hit))
			{
				currentCell = hexGrid.GetCell(hit.point);
				EditCell(currentCell);
			}
		}
	}

	void pathFinding()
    {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButton(1))
		{
			searchFromCell = null;
			hexGrid.disableAllHighlights();
		}
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			if (Physics.Raycast(inputRay, out hit))
			{
				searchFromCell = hexGrid.GetCell(hit.point);
			}
		}
		if (searchFromCell && Physics.Raycast(inputRay, out hit))
		{
			currentCell = hexGrid.GetCell(hit.point);
			if (!previousCell)
			{
				previousCell = currentCell;
			}
			else if (previousCell != currentCell && !EventSystem.current.IsPointerOverGameObject())
			{

				previousCell = currentCell;

				if (searchFromCell && searchFromCell != currentCell)
				{
					hexGrid.disableAllHighlights();
					hexGrid.FindPath(searchFromCell, currentCell, 19);
				}

			}
		}
	}

	void movementZone()
    {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			if (Physics.Raycast(inputRay, out hit))
			{
				currentCell = hexGrid.GetCell(hit.point);
				hexGrid.FindDistancesTo(currentCell, 19);
			}
		}
	}

	void EntityMode()
    {
		
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			HexCell cell = GetCellUnderCursor();
			if (cell)
				if (!cell.Unit)
				{
					HexUnit unit = Instantiate(unitPrefab);
					unit.Location = cell;
					unit.grid = hexGrid;
					unit.faction = 0;
					unit.GetComponent<Renderer>().material.color = Color.blue;
					hexGrid.combatController.units.Add(unit);
					unit.setStats(HexUnit.unitType.Human, 50);
					hexGrid.combatController.playerUnits.Add(unit);

				}
				else
				{
					cell.Unit.Destroy();
				}

		}
		if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
		{
			HexCell cell = GetCellUnderCursor();
			if (cell)
				if (!cell.Unit)
				{
					HexEnemy unit = Instantiate(enemyPrefab);
					unit.Location = cell;
					unit.grid = hexGrid;
					unit.faction = 1;
					unit.GetComponent<Renderer>().material.color = Color.red;
					hexGrid.combatController.units.Add(unit);
					unit.setStats(HexUnit.unitType.Kelpie, 40);
					hexGrid.combatController.enemyUnits.Add(unit);

				}
				else
				{
					cell.Unit.Destroy();
				}

		}

	}


	public void SetTerrainTypeIndex(int index)
	{
		activeTerrainTypeIndex = index;
	}
	void EditCell(HexCell cell)
	{
		if (cell)
		{
			if (activeTerrainTypeIndex >= 0)
			{
				cell.TerrainTypeIndex = activeTerrainTypeIndex;
			}
			hexGrid.Refresh();
		}
	}


	public void SetEditMode(bool toggle)
	{
		editPanel.SetActive(true);
		colorPanel.SetActive(false);
		hexGrid.editMode = true;
	}
	public void SetPlayMode(bool toggle)
	{
		editPanel.SetActive(false);
		colorPanel.SetActive(false);
	}
	public void SetEditMapInfoMode(bool toggle)
	{
		colorPanel.SetActive(true);
		mapInfoMode = !mapInfoMode;
	}
	public void SetPathfindingMode(bool toggle)
	{
		colorPanel.SetActive(false);
		pathfindingMode = !pathfindingMode;
	}
	public void SetZoneMode(bool toggle)
	{
		colorPanel.SetActive(false);
		movementZoneMode = !movementZoneMode;
	}
	public void SpawnMode(bool toggle)
	{
		colorPanel.SetActive(false);
		spawnMode = !spawnMode;
	}
	HexCell GetCellUnderCursor()
	{
		return
			hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
	}

	public void Save()
	{
		string path = Path.Combine(Application.dataPath + "/maps", editPanel.transform.Find("Map Name").GetComponent<InputField>().text + ".txt");
		using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
		{
			hexGrid.Save(writer);
		}
	}

	public void Load()
	{
		string path = Path.Combine(Application.dataPath + "/maps", editPanel.transform.Find("Map Name").GetComponent<InputField>().text + ".map");

		if (System.IO.File.Exists(path))
		{
			Debug.Log("Loading file at: " + path);
			hexGrid.DeleteEntities();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
			{
				hexGrid.Load(reader);
			}
			hexGrid.SpawnEntities(enemyPrefab);
		}
		else
		{
			Debug.LogWarning("File at: " + path + " does not exist");
		}

		
	}
}
