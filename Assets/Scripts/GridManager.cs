using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public static event Action<GridManager> OnActiveGridItemChanged;
    [SerializeField] private GridItem activeGridItem;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        GridItem.OnGridHovered += SetActiveGridItem;
    }
    private void OnDisable()
    {
        GridItem.OnGridHovered -= SetActiveGridItem;
    }


    public void SetActiveGridItem(GridItem gridItem)
    {

        if (gridItem != null)
        {
            activeGridItem = gridItem;
            OnActiveGridItemChanged?.Invoke(this);
        }
        else
        {
            activeGridItem = null;
        }

    }

    public GridItem GetActiveGridItem()
    {
        return activeGridItem;
    }





}
