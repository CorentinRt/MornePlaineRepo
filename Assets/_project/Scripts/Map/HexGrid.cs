using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    #region Fields
    private Dictionary<(int, int), Cell> _hexGridDict = new Dictionary<(int, int), Cell>();

    #endregion


    public void AddToHexGrid(int x, int y, Cell cell)
    {
        if (_hexGridDict == null)
        {
            _hexGridDict = new Dictionary<(int, int), Cell>();
        }

        _hexGridDict[(x, y)] = cell;

        //Debug.Log("Add to hex grid : " + x + ", " + y);
    }

}
