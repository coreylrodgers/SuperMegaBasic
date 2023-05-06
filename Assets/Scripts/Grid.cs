using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

[System.Serializable]
public class Grid
{
    [SerializeField] private string name;
    [SerializeField] private int width, height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    private GameObject[,] placeableGhostArray;
    //build constructor
    public Grid(int width, int height, float cellSize, Vector3 originPosition, string name)
    {
        this.name = name;
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;


        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {

                // Add placement ghost 

                Vector3 textPosition = GetGridWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f;

                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, textPosition, 20, Color.white, TextAnchor.MiddleCenter);
                debugTextArray[x, y].characterSize = 0.2f;

                Debug.DrawLine(GetGridWorldPosition(x, y), GetGridWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetGridWorldPosition(x, y), GetGridWorldPosition(x + 1, y), Color.white, 100f);
            }

        }

        Debug.DrawLine(GetGridWorldPosition(0, height), GetGridWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetGridWorldPosition(width, 0), GetGridWorldPosition(width, height), Color.white, 100f);


    }

    public Grid GetGrid() {
        return this;
    }

    public string GetGridName()
    {
        return name;
    }
    public Vector3 GetGridWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void SetGridTileOccupied(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, 1);
    }
    public bool IsGridTileOccupied()
    {
        return GetValue(UtilsClass.GetMouseWorldPosition()) == 1 ? true : false;
    }
    public Vector3 GetWorldPlacementPosition(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return new Vector3(x, y) * cellSize + originPosition + new Vector3(cellSize, cellSize) * 0.5f;
    }

    private Grid GetSelectedGrid(Vector3 worldPosition)
    {
        return this;
    }
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            TextMesh textMesh = debugTextArray[x, y];
            textMesh.text = value.ToString();
            textMesh.fontSize = textMesh.fontSize + 4;
            textMesh.color = value == 1 ? Color.red : Color.blue;

            gridArray[x, y] = value;
        }
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);

    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            // if valid value
            return gridArray[x, y];
        }
        else
        {
            return -1;
        }
    }
    public int GetValue(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetValue(x, y);
    }
}
