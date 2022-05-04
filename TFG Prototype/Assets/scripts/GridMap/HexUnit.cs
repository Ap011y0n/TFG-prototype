using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexUnit : MonoBehaviour
{

	public List<HexCell> path;
	public int faction = 0;
	public HexGrid grid;

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
    public void Die()
    {

		location.Unit = null;
		Destroy(this.gameObject);
	
	}
	public void Move()
    {
		if (path != null)
		{
			Location = path[0];
			grid.disableAllHighlights();
			path = null;

		}
		else
			Debug.LogWarning(gameObject.name + " path is null!");
    }
}
