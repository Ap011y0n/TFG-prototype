using UnityEngine;
using UnityEngine.EventSystems;

public class HexGameUI : MonoBehaviour
{
	public HexGrid grid;
	bool playMode = true;
	public void SetEditMode()
	{
		grid.ShowUI(true);
		playMode = false;
	}

	public void SetPlayMode()
	{
		grid.ShowUI(false);
		playMode = true;
	}
    private void Update()
    {
		Debug.Log(playMode);
    }

}
