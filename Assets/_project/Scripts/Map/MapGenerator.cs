using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Fields
    [Header("Map Datas")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [SerializeField] private float _cellGap;

    [SerializeField] private GameObject _cellPrefab;

    [SerializeField] private GameObject _mapContainer;

    #endregion


    [Button]
    private void GenerateMap()
    {
        for (int i = 0; i < _height; ++i)   // Y coor
        {
            float midGap = i%2 == 1 ? _cellGap / 2f : 0f;

            for (int j = 0; j < _width; ++j)    // X coor
            {
                GameObject go = Instantiate(_cellPrefab, new Vector3(_cellGap * j + midGap, transform.position.y, _cellGap * 0.75f * i), Quaternion.identity);
                go.name = "Cell : X : " + j + " / Y : " + i;
                go.transform.SetParent(_mapContainer.transform);

                Cell cell = go.GetComponent<Cell>();
                cell.SetCoordinate(j, i);
            }
        }
    }

}
