using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    #region Fields
    private int _xPos;
    private int _yPos;


    #endregion

    #region Properties
    public int XPos { get => _xPos; set => _xPos = value; }
    public int YPos { get => _yPos; set => _yPos = value; }


    #endregion

    public void SetCoordinate(int x, int y)
    {
        _xPos = x;
        _yPos = y;
    }

}
