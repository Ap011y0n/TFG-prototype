using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexUnit : MonoBehaviour
{

	public List<HexCell> path;
	public int faction = 0;
	public HexGrid grid;
	[HideInInspector]
	public bool movement = true;
	[HideInInspector]
	public bool action = true;
	public HexCell Location
	{
		get
		{
			return location;
		}
		set
		{
			location = value;
			value.Unit = this;
			transform.localPosition = value.Position;
		}
	}

	HexCell location;

    private void Start()
    {
		path = new List<HexCell>();
	}
    public void Destroy()
    {

		location.Unit = null;
		grid.combatController.units.Remove(this);
		Destroy(this.gameObject);
	
	}
	public void Move()
    {
		if (path != null)
		{
			Location.Unit = null;
			Location = path[0];
			grid.disableAllHighlights();
			path = null;
			movement = false;

		}
		else
			Debug.LogWarning(gameObject.name + " path is null!");
    }
	public bool isInRange(HexUnit enemy)
    {
		bool ret = false;
		for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
			if (location.GetNeighbor(d) == enemy.location)
            {
				Debug.Log("Enemy in range");
				ret = true;
            }


		}


		return ret;
    }
	public bool HasEnemyInRange()
	{
		bool ret = false;
		for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
		{
			if (location.GetNeighbor(d).Unit && location.GetNeighbor(d).Unit.faction != faction)
			{
				Debug.Log("Enemy in range");
				ret = true;
			}


		}


		return ret;
	}
	public void Attack(HexUnit enemy)
    {
		enemy.Destroy();
		action = false;
    }
	public void ResetActions()
    {
		action = true;
		movement = true;
    }
}
