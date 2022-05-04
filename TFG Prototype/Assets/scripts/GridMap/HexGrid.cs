using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;


public class HexGrid : MonoBehaviour
{
	public int width = 6;
	public int height = 6;

	public HexCell cellPrefab;
    public Text cellLabelPrefab;
    [HideInInspector]
    public bool editMode = false;
	HexCell[] cells;

    Canvas gridCanvas;
    HexMesh hexMesh;

    public Color touchedColor = Color.magenta;

    HexCellPriorityQueue searchFrontier;

    int searchFrontierPhase;

    public Color[] colors;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        HexMetrics.colors = colors;

        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }

    }

    private void OnEnable()
    {
        HexMetrics.colors = colors;

    }
    void Start()
    {
        hexMesh.Triangulate(cells);
    }

    void CreateCell( int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * HexMetrics.innerRadius * 2f;
        position.y = 0f;
        position.z = z * HexMetrics.outerRadius * 1.5f;

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        if (x > 0)
        {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - width]);
                if (x > 0)
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
                }
            }
            else
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - width]);
                if (x < width - 1)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
                }
            }
        }

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        cell.uiRect = label.rectTransform;

    }

    public void ShowUI(bool visible)
    {
        gridCanvas.gameObject.SetActive(visible);
    }


    public void Refresh()
    {
        hexMesh.Triangulate(cells);
    }

    public HexCell GetCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        return cells[index];
    }

    public void FindDistancesTo(HexCell cell, int speed)
    {
        Debug.Log("select");
        Search(cell, speed);
    }

    void Search(HexCell cell, int speed)
    {


        disableAllHighlights();

        Queue<HexCell> frontier = new Queue<HexCell>();
        cell.Distance = 0;
        frontier.Enqueue(cell);
        while (frontier.Count > 0)
        {

            HexCell current = frontier.Dequeue();
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                HexCell neighbor = current.GetNeighbor(d);
                if (neighbor == null || neighbor.Distance != int.MaxValue)
                {
                    continue;
                }
                /*  if (neighbor.IsUnderwater)
                  {
                      continue;
                  }*/
                int currentTurn = current.Distance / speed;

                int moveCost = 5; //editar luego
                int distance = current.Distance + moveCost;
                int turn = distance / speed;
                if (turn > currentTurn)
                {
                    break;
                }
                neighbor.Distance = distance;
                if (editMode)
                    neighbor.UpdateDistanceLabel();
                neighbor.EnableHighlight(Color.yellow);
                neighbor.PathFrom = current;
                frontier.Enqueue(neighbor);
            }
        }
    }

    public List<HexCell> FindPath(HexCell fromCell, HexCell toCell, int speed)
    {
        if (fromCell && toCell)
            return Search(fromCell, toCell, speed);
        return null;
    }

    public void disablePathHighLights(List<HexCell> toDisable)
    {
        for (int i= 0; i < toDisable.Count; i++)
        {
            toDisable[i].Distance = int.MaxValue;
            toDisable[i].UpdateDistanceLabel();
            toDisable[i].EnableHighlight(Color.yellow);
        }
    }
    public void disableAllHighlights()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Distance = int.MaxValue;
            cells[i].UpdateDistanceLabel();
            cells[i].DisableHighlight();

        }
    }
    List<HexCell> Search(HexCell fromCell, HexCell toCell, int speed)
    {
        searchFrontierPhase += 2;

        if (searchFrontier == null)
        {
            searchFrontier = new HexCellPriorityQueue();
        }
        else
        {
            searchFrontier.Clear();
        }

       // disableAllHighlights();

        fromCell.SearchPhase = searchFrontierPhase;
        fromCell.Distance = 0;
        fromCell.EnableHighlight(Color.blue);
        if (editMode)
            fromCell.UpdateDistanceLabel();

        searchFrontier.Enqueue(fromCell);

        while (searchFrontier.Count > 0)
        {
            HexCell current = searchFrontier.Dequeue();
            current.SearchPhase += 1;

            if (current == toCell)
            {
                List<HexCell> ret = new List<HexCell>();
                ret.Add(current);
                current = current.PathFrom;
                while (current != fromCell)
                {
                    ret.Add(current);
                    if(editMode)
                    current.UpdateDistanceLabel();
                    current.EnableHighlight(Color.grey);
                    current = current.PathFrom;
                }
                ret.Add(fromCell);
                toCell.EnableHighlight(Color.green);
                if (editMode)
                    toCell.UpdateDistanceLabel();
                return ret;
            }

            int currentTurn = current.Distance / speed;

            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                HexCell neighbor = current.GetNeighbor(d);
                if (neighbor == null || neighbor.SearchPhase > searchFrontierPhase )
                {
                    continue;
                }
                if (neighbor.Unit && neighbor.Unit.faction != fromCell.Unit.faction)
                {
                    continue;
                }

                int moveCost = 5; //editar luego
                int distance = current.Distance + moveCost;
                int turn = distance / speed;
                if (turn > currentTurn)
                {
                    return null;
                }
                if (neighbor.SearchPhase < searchFrontierPhase)
                {
                    neighbor.SearchPhase = searchFrontierPhase;
                    neighbor.Distance = distance;
                    neighbor.PathFrom = current;
                    neighbor.SearchHeuristic = neighbor.coordinates.DistanceTo(toCell.coordinates) * 5;
                    searchFrontier.Enqueue(neighbor);
                }
                else if (distance < neighbor.Distance)
                {
                    int oldPriority = neighbor.SearchPriority;
                    neighbor.Distance = distance;
                    neighbor.PathFrom = current;
                    searchFrontier.Change(neighbor, oldPriority);

                }
                
            }
        }
        return null;
    }

    public HexCell GetCell(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return GetCell(hit.point);
        }
        return null;
    }

    public void Save(BinaryWriter writer)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Save(writer);
        }
    }

    public void Load(BinaryReader reader)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Load(reader);
        }
        Refresh();
    }
}
