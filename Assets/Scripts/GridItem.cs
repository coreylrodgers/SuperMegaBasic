using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridItem : MonoBehaviour
{
    public enum EDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    [SerializeField] int height, width = 3;
    private BoxCollider2D gridCollider;
    [SerializeField] int cellSize = 1;
    [SerializeField] Vector3 origin;
    [SerializeField] GameObject highlight;
    [SerializeField] EDirection direction;

    Grid grid;

    public static event Action<GridItem> OnGridHovered;
    void Start()
    {
        gridCollider = GetComponent<BoxCollider2D>();
        SetCollider();
        grid = new Grid(width, height, cellSize, origin, this.gameObject.name);
    }

    public Grid GetGrid()
    {
        return this.grid;
    }

    void SetCollider()
    {
        Vector2 size = new Vector2(width, height);
        gridCollider.size = size;
        gridCollider.offset = size * 0.5f;
        origin = transform.position;
    }
    public Vector3 GetRotation()
    {
        switch (direction)
        {
            case GridItem.EDirection.UP:
                return new Vector3(0, 0, 0);
            case GridItem.EDirection.DOWN:
                return new Vector3(0, 0, 180);
            case GridItem.EDirection.RIGHT:
                return new Vector3(0, 0, 270);
            case GridItem.EDirection.LEFT:
                return new Vector3(0, 0, 90);
            default:
                return Vector3.zero;
        }
    }

    void OnMouseEnter()
    {
        //Send event to gridManager
        OnGridHovered?.Invoke(this);

    }
    void OnMouseExit()
    {
        OnGridHovered?.Invoke(null);
    }


}
