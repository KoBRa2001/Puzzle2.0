using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int heigth;

    [SerializeField] private ItemSlot itemSlotPrefab;
    [SerializeField] private DragController _dragController;
    [SerializeField] private ItemController _itemController;

    private Vector2 currentPosition = Vector2.zero;

    private bool[,] cells;
    private ItemSlot[,] itemCells;

    private void Awake()
    {
        cells = new bool[width, heigth];
        itemCells = new ItemSlot[width, heigth];
        currentPosition.x = -width / 2f + 0.5f; ;
        currentPosition.y = -heigth / 2f + 1;
    }

    private void Start()
    {
        InitGrid();
    }

    public void InitGrid()
    {        
        for(int i = 0;  i < width; i++)
        {
            for(int j = 0; j < heigth; j++)
            {
                InitSlot(i, j);               
            }
        }
    }

    public void InitSlot(int i, int j)
    {
        var newSlot = Instantiate(itemSlotPrefab, currentPosition, Quaternion.identity, transform);
        newSlot.Setup(_dragController, this);

        //перенести в медот і створити публічні геттери
        newSlot.x = i;
        newSlot.y = j;

        newSlot.transform.localPosition = currentPosition + new Vector2(i, j);
        itemCells[i, j] = newSlot;
    }

    public void SetCell(int x, int y)
    {
        cells[x, y] = true;


        //if (IsPossibleCollapse(x, y))
        //    VerifyGrid();
    }

    public void CalculateCollapse()
    {
        //if (IsPossibleCollapse())
            VerifyGrid();
    }

    private void VerifyGrid()
    {
        HashSet<Vector2Int> itemIndexes = GetDeletedItems();
        foreach(var currentPosition in itemIndexes)
        {
            itemCells[currentPosition.x, currentPosition.y].ClearSlot();
            cells[currentPosition.x, currentPosition.y] = false;
        }
    }

    public HashSet<Vector2Int> GetDeletedItems()
    {
        HashSet<Vector2Int> items = new HashSet<Vector2Int>();
        List<Vector2Int> tempItems = new List<Vector2Int>();

        for(int x = 0; x < width; x++)
        {
            for (int i = 0; i < heigth; i++)
            {
                if (!cells[x, i])
                {
                    tempItems.Clear();
                    break;
                }
                else
                {
                    tempItems.Add(new Vector2Int(x, i));
                }
            }    
            for(int n = 0; n < tempItems.Count; n++)
            {
                items.Add(tempItems[n]);
            }
            tempItems.Clear();
        }
        for(int y = 0; y < heigth; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!cells[j, y])
                {
                    tempItems.Clear();
                    break;
                }
                else
                {
                    tempItems.Add(new Vector2Int(j, y));
                }
            }
            for (int n = 0; n < tempItems.Count; n++)
            {
                items.Add(tempItems[n]);
            }
        }

        return items;

        //return null;
    }

    public bool IsPossibleCollapse(/*int x, int y*/)
    {
        bool rowsResult = true;
        bool columnsResult = true;

        for (int i = 0; i < width; i++)
        {            
            for (int j = 0; j < heigth; j++)
            {
                rowsResult = rowsResult && cells[i, j];                                
            }
            if (rowsResult)
                break;            
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                columnsResult = columnsResult && cells[i, j];
            }
            if (columnsResult)
                break;
        }

        //for(int i = 0; i < width; i++)
        //{
        //    if (!cells[x, i])
        //    {
        //        columnsResult = false;
        //        break;
        //    }
        //}
        //for(int j = 0; j < heigth; j++)
        //{
        //    if (!cells[j, y])
        //    {
        //        rowsResult = false;
        //        break;
        //    }
        //}
        Debug.Log("Rows " + rowsResult + " Columns " + columnsResult);
        Debug.Log("Return result " + (rowsResult || columnsResult));
        //if (rowsResult && !columnsResult)
        //{
        //    Debug.Log("Row is full");
        //    //Debug.Log("Row " + y + " is full");
        //}
        //if (!rowsResult && columnsResult)
        //{
        //    //Debug.Log("Column " + x + " is full");
        //    Debug.Log("Column is full");
        //}
        //if (rowsResult && columnsResult)
        //{
        //    //Debug.Log("Column " + x + " is full and Row " + y + " is full");
        //    Debug.Log("Column is full and Row is full");
        //}

        return rowsResult || columnsResult;
    }
    
}
