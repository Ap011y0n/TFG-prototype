using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour
{
	public Color[] colors;
	public HexGrid hexGrid;
	private Color activeColor;
	public bool editMode = false;
	public bool pathfindingMode = false;
	public bool spawnMode = false;
	public bool movementZoneMode = false;
	HexCell previousCell, searchFromCell, currentCell;
	public HexUnit unitPrefab;

	void Awake()
	{
		SelectColor(0);
	} 

	void Update()
	{
		if(editMode)
        {
			editMap();
		}
		else if(pathfindingMode)
        {
			pathFinding();
		}
		else if(spawnMode)
        {

        }
		else if(movementZoneMode)
        {
			movementZone();
		}

			
	}

	void editMap()
    {
		
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(inputRay, out hit))
			{
				currentCell = hexGrid.GetCell(hit.point);
				HandleInput();
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
				HandleInput();

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
				HandleInput();
			}
		}
	}

	void CreateEntity()
    {
		HexCell cell = GetCellUnderCursor();
		if (cell)
		{
			HexUnit unit = Instantiate(unitPrefab);
			unit.transform.SetParent(hexGrid.transform, false);
		}
	}
	void HandleInput()
	{


		if (editMode)
		{
			EditCell(currentCell);
		}
		else if (pathfindingMode)
		{
			if (searchFromCell && searchFromCell != currentCell)
			{

				hexGrid.FindPath(searchFromCell, currentCell, 24);
			}

		}
		else
		{
			Debug.Log("Hola");
			hexGrid.FindDistancesTo(currentCell, 24);

		}



	}


	void EditCell(HexCell cell)
	{
		cell.color = activeColor;
		hexGrid.Refresh();
	}

	public void SelectColor(int index)
	{
		activeColor = colors[index];
	}
	public void SetEditMode(bool toggle)
	{
		editMode = !editMode;
	}
	public void SetPathfindingMode(bool toggle)
	{
		pathfindingMode = !pathfindingMode;
	}

	HexCell GetCellUnderCursor()
	{
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit))
		{
			return hexGrid.GetCell(hit.point);
		}
		return null;
	}

}
