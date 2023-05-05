using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    [SerializeField] int height, width;
    [SerializeField] int cellSize;
    [SerializeField] Vector3 origin;
    Grid grid;
    void Start()
    {
        grid = new Grid(width, height, cellSize, origin, this.name);
    }

    void Update()
    {
        
    }
}
