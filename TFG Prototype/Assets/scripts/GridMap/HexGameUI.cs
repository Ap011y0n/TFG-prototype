using UnityEngine;
using UnityEngine.EventSystems;

public class HexGameUI : MonoBehaviour
{
	public HexGrid grid;
	HexCell currentCell;
	HexUnit selectedUnit;


	public void SetPlayMode()
	{
		grid.editMode = false;
		grid.disableAllHighlights();
	}

	void Update()
	{
		if (!grid.editMode && !EventSystem.current.IsPointerOverGameObject())
		{
			if (Input.GetMouseButtonDown(0))
			{
				if(!selectedUnit)
				DoSelection();
				else if(selectedUnit.path != null && selectedUnit.movement)
                {
					selectedUnit.Move();
					selectedUnit = null;
				}
				else if(currentCell.Unit && selectedUnit.isInRange(currentCell.Unit) && selectedUnit.action)
                {
					selectedUnit.Attack(currentCell.Unit);
                }
			}
			else if(Input.GetMouseButtonDown(1))
            {
				selectedUnit = null;
				grid.disableAllHighlights();
			}
			else if (selectedUnit)
			{
				DoPathfinding();
			}
		}
	}
	void DoSelection()
	{

		UpdateCurrentCell();
		if (currentCell)
		{
			selectedUnit = currentCell.Unit;
			if(selectedUnit)
            {
				if (selectedUnit.movement)
					grid.FindDistancesTo(currentCell, 24);
			}
				

		}

	}

	void DoPathfinding()
	{
		if (UpdateCurrentCell() && selectedUnit.movement)
		{
			if(IsValidDestination(currentCell))
            {
				if (selectedUnit.path != null)
					grid.disablePathHighLights(selectedUnit.path);

				selectedUnit.path = grid.FindPath(selectedUnit.Location, currentCell, 24);
			}
			else
			{
				if (selectedUnit.path != null)
					grid.disablePathHighLights(selectedUnit.path);
				selectedUnit.path = null;
			}
		}
		
	}

	public bool IsValidDestination(HexCell cell)
	{
		return (cell && !cell.Unit);
	}
	bool UpdateCurrentCell()
	{
		HexCell cell =
			grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
		if (cell != currentCell)
		{
			currentCell = cell;
			return true;
		}
		return false;
	}
}
