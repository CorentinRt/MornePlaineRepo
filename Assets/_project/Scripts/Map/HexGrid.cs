using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    #region Fields
    private static HexGrid _instance;

    [SerializeField] private Dictionary<(int, int), Cell> _hexGridDict;


    #endregion

    #region Properties
    public static HexGrid Instance { get => _instance; set => _instance = value; }


    #endregion

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }

    public void AddToHexGrid(int x, int y, Cell cell)
    {
        if (_hexGridDict == null)
        {
            _hexGridDict = new Dictionary<(int, int), Cell>();
        }

        _hexGridDict[(x, y)] = cell;

        //Debug.Log("Add to hex grid : " + x + ", " + y);
    }

    #region Pathfinding A*
    public Cell[] GetPathTo(Cell startCell, Cell targetCell)
    {
        if (startCell == null || targetCell == null)  return null;

        var openSet = new List<Cell> { startCell };
        var cameFrom = new Dictionary<Cell, Cell>();
        var gScore = new Dictionary<Cell, float> { [startCell] = 0 };
        var fScore = new Dictionary<Cell, float> { [startCell] = Heuristic(startCell, targetCell) };

        while (openSet.Count > 0)
        {
            // Trier par le plus petit F et prendre le premier
            var current = openSet.OrderBy(cell => fScore.ContainsKey(cell) ? fScore[cell] : float.MaxValue).First();

            if (current == targetCell) return ReconstructPath(cameFrom, current);

            openSet.Remove(current);

            foreach (var neighbor in GetNeighbors(current))
            {
                float tentativeGScore = gScore[current] + Distance(current, neighbor);

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + Heuristic(neighbor, targetCell);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }
    #region Macro
    private float Heuristic(Cell a, Cell b)
    {
        return Mathf.Abs(a.XPos - b.XPos) + Mathf.Abs(a.YPos - b.YPos);
    }
    private List<Cell> GetNeighbors(Cell cell)
    {
        List<Cell> neighbors = new List<Cell>();
        (int x, int y)[] directions = new (int, int)[]
        {
            (1, 0), (1, -1), (0, -1),
            (-1, 0), (-1, 1), (0, 1)
        };

        foreach (var (dx, dy) in directions)
        {
            if (_hexGridDict.TryGetValue((cell.XPos + dx, cell.YPos + dy), out Cell neighbor))
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
    private Cell[] ReconstructPath(Dictionary<Cell, Cell> cameFrom, Cell current)
    {
        List<Cell> path = new List<Cell> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path.ToArray();
    }
    private float Distance(Cell a, Cell b)
    {
        return (Mathf.Abs(a.XPos - b.XPos) + Mathf.Abs(a.YPos - b.YPos) + Mathf.Abs((a.XPos - a.YPos) - (b.XPos - b.YPos))) / 2f;
    }
    #endregion
    #endregion
}
