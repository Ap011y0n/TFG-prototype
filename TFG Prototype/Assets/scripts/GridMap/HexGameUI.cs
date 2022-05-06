using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HexGameUI : MonoBehaviour
{
	public HexGrid grid;
	HexCell currentCell;
	HexUnit selectedUnit;
	public bool play = false;
	private int availableTroops;
	public CombatController combatController;
	public GameObject playbutton;
	public Text deployedTroopCount;
	public GameObject startPanel;

	public GameObject turnButton;
	public Text turnCount;
	public GameObject turnPanel;

	public void SetPlayMode()
	{
		grid.editMode = false;
		grid.disableAllHighlights();
	}
	public void StartGame()
    {
		play = true;
		startPanel.SetActive(false);
		playbutton.SetActive(false);

		turnButton.SetActive(true);
		turnPanel.SetActive(true);
		turnCount.text = combatController.getCurrentTurn().ToString();

	}
	private void Start()
    {
		//availableTroops = PlayerManager.Instance.troopCount;
		availableTroops = 4;
		deployedTroopCount.text = availableTroops.ToString();
		combatController.Load();

	}

	void Update()
	{
		if (!grid.editMode && !EventSystem.current.IsPointerOverGameObject())
		{
			if (Input.GetMouseButtonDown(0))
			{
				if(play)
                {
					if (!selectedUnit)
						DoSelection();
					else if (selectedUnit.path != null && selectedUnit.movement)
					{
						selectedUnit.Move();
						selectedUnit = null;
					}
					else if (currentCell && currentCell.Unit && selectedUnit.isInRange(currentCell.Unit) && selectedUnit.action)
					{
						selectedUnit.Attack(currentCell.Unit);
					}
				}
				else
                {
					DoDeploy();
                }
				
			}
			else if(Input.GetMouseButtonDown(1) && play)
            {
				selectedUnit = null;
				grid.disableAllHighlights();
			}
			else if (selectedUnit && play)
			{
				DoPathfinding();
			}
		}
	}
	void DoSelection()
	{

		UpdateCurrentCell();
		if (currentCell && currentCell.Unit)
		{
			selectedUnit = currentCell.Unit;
			grid.disableAllHighlights();

			if (selectedUnit.movement)
            {
				selectedUnit.Location.EnableHighlight(Color.blue);
				grid.FindDistancesTo(selectedUnit.Location, 24);

			}
			else
            {
				if (selectedUnit.HasEnemyInRange())
					selectedUnit.Location.EnableHighlight(Color.red);
				else
				selectedUnit.Location.EnableHighlight(Color.blue);


			}


		}

	}

	void DoDeploy()
    {
		UpdateCurrentCell();
		if(currentCell.TerrainTypeIndex == 2 && availableTroops > 0 && !currentCell.Unit)
        {
			HexUnit unit = Instantiate(combatController.unitPrefab);
			unit.Location = currentCell;
			unit.grid = grid;
			unit.faction = 0;
			unit.GetComponent<Renderer>().material.color = Color.blue;
			combatController.units.Add(unit);
			combatController.playerUnits.Add(unit);
			availableTroops--;
			deployedTroopCount.text = availableTroops.ToString();
		}
		else if (currentCell.TerrainTypeIndex == 2 && currentCell.Unit)
		{
			currentCell.Unit.Destroy();
			availableTroops++;
			deployedTroopCount.text = availableTroops.ToString();
		}
	}

	void DoPathfinding()
	{
		if (UpdateCurrentCell() && selectedUnit.movement)
		{
			if(IsValidDestination(currentCell))
            {
				grid.FindDistancesTo(selectedUnit.Location, 24);
				selectedUnit.path = grid.FindPath(selectedUnit.Location, currentCell, 24);
				if (selectedUnit.path != null)
					grid.PaintPath(selectedUnit.path);
			
			}
			else
			{
				grid.FindDistancesTo(selectedUnit.Location, 24);
				selectedUnit.Location.EnableHighlight(Color.blue);
				if (selectedUnit.path != null)
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

	public void PassTurn()
    {
		turnCount.text = combatController.getCurrentTurn().ToString();
	}
}
