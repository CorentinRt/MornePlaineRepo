using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CELL_TYPE
{
    PLAIN = 0,
    FOREST = 1,
    WATER = 2
}
public class Cell : MonoBehaviour, IInteractible
{
    #region Fields
    private int _xPos;
    private int _yPos;

    private CELL_TYPE _cellType;

    private bool _isWalkable;

    [Header("Mesh")]
    [SerializeField] private MeshRenderer _meshRenderer;

    [Header("Materials")]
    [SerializeField] private Material _plainMat;
    [SerializeField] private Material _forestMat;
    [SerializeField] private Material _waterMat;

    #endregion

    #region Properties
    public int XPos { get => _xPos; set => _xPos = value; }
    public int YPos { get => _yPos; set => _yPos = value; }
    public bool IsWalkable { get => _isWalkable; set => _isWalkable = value; }


    #endregion

    public void SetCoordinate(int x, int y)
    {
        _xPos = x;
        _yPos = y;
    }

    public void SetCellType(CELL_TYPE cellType)
    {
        _cellType = cellType;

        switch (_cellType)
        {
            case CELL_TYPE.PLAIN:
                _meshRenderer.material = _plainMat;
                _isWalkable = true;
                break;
            case CELL_TYPE.FOREST:
                _meshRenderer.material = _forestMat;
                _isWalkable = true;
                break;
            case CELL_TYPE.WATER:
                _meshRenderer.material = _waterMat;
                _isWalkable = false;
                break;

            default:
                break;
        }
    }

    #region IInteractible Interface
    public bool IsInteractible()
    {
        return true;
    }

    public void Interact()
    {
        Debug.Log("Interact");
    }

    public bool IsGettable()
    {
        return true;
    }

    public IInteractible InteractGet()
    {
        Debug.Log("InteractGet");
        return this;
    }
    #endregion
}
