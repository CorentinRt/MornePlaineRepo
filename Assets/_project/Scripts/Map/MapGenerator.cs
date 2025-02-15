using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Fields
    [Header("Map Datas")]

    [SerializeField] private bool _autoGenerate;

    [Header("Grid")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private HexGrid _hexGrid;

    [Header("Cells")]
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private float _cellInnerGap;
    [SerializeField] private float _cellOuterGap;


    #endregion

    private void Start()
    {
        if (_autoGenerate)
        {
            GenerateMap();
        }
    }


    [Button]
    private void GenerateMap()
    {
        for (int i = 0; i < _height; ++i)   // Y coor
        {
            float midGap = i%2 == 1 ? _cellInnerGap / 2f : 0f;

            for (int j = 0; j < _width; ++j)    // X coor
            {
                GameObject go = Instantiate(_cellPrefab, new Vector3(_cellInnerGap * j + midGap, transform.position.y, _cellOuterGap * 0.75f * i), Quaternion.identity);
                go.name = "Cell : X : " + j + " / Y : " + i;
                go.transform.SetParent(_hexGrid.transform);

                Cell cell = go.GetComponent<Cell>();
                cell.SetCoordinate(j, i);

                int rand = Random.Range(0, 3);
                cell.SetCellType((CELL_TYPE)rand);

                _hexGrid.AddToHexGrid(j, i, cell);
            }
        }
    }

}
