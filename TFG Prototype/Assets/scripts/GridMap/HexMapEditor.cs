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
	HexCell previousCell, searchFromCell;

	void Awake()
	{
		SelectColor(0);
	}

	void Update()
	{
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			HandleInput();
		}
	}

	void HandleInput()
	{
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit))
		{
			HexCell currentCell = hexGrid.GetCell(hit.point);

			if (editMode)
			{
				EditCell(currentCell);
			}
			else if (pathfindingMode)
			{
				if (searchFromCell && searchFromCell != currentCell)
				{

					hexGrid.FindPath(searchFromCell, currentCell);
				}
				searchFromCell = currentCell;

			}
			else
			{
				hexGrid.FindDistancesTo(currentCell);

			}
			previousCell = currentCell;
		}
		else
		{
			previousCell = null;
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
}
