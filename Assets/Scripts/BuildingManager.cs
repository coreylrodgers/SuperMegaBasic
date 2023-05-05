using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    //Event handler
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }
    private Grid topPlacement;
    private Grid bottomPlacement;

    //Members
    BuildingTypeListSO buildingTypeList;
    BuildingTypeSO activeBuildingType;
    Dictionary<BuildingTypeSO.BuildingName, BuildingTypeSO> buildingNameDictionary;

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingNameDictionary = new Dictionary<BuildingTypeSO.BuildingName, BuildingTypeSO>();
        activeBuildingType = buildingTypeList.list[0];
    }
    private void Start()
    {
        //Create placement grids 
        topPlacement = new Grid(16, 2, 1f, new Vector3(-8.5f, 2.5f), "top");
        bottomPlacement = new Grid(16, 2, 1f, new Vector3(-8.5f, -4), "bottom");

        // Add buildingTypes to dictionary
        foreach (BuildingTypeSO bt in buildingTypeList.list)
        {
            buildingNameDictionary[bt.buildingName] = bt;
        }
    }
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        // Fire off event for other classes to use
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }

    private void SetBuildingGrid(GameObject pf, Grid buildingGrid) {
            pf.GetComponent<GridHolder>().grid = topPlacement;
            Grid grid = pf.GetComponent<GridHolder>().grid;
            grid = buildingGrid;
            grid.SetGridTileOccupied(UtilsClass.GetMouseWorldPosition());
    }
    private void CreateBuilding(BuildingTypeSO buildingType)
    {
        // If on grid get grid name
        int top = topPlacement.GetValue(UtilsClass.GetMouseWorldPosition());
        int btm = bottomPlacement.GetValue(UtilsClass.GetMouseWorldPosition());
        if (top == 0)
        {
            GameObject pf = Instantiate(buildingType.prefab, topPlacement.GetWorldPlacementPosition(UtilsClass.GetMouseWorldPosition()), Quaternion.identity);
            SetBuildingGrid(pf, topPlacement);
            pf.transform.Rotate(new Vector3(0,0, 180));
        }
        else if (btm == 0)
        {
            // Place in bottom
            GameObject pf = Instantiate(buildingType.prefab, bottomPlacement.GetWorldPlacementPosition(UtilsClass.GetMouseWorldPosition()), Quaternion.identity);
            SetBuildingGrid(pf, bottomPlacement);
        }

    }

    private void Update()
    {

        //Manage Selection Using the Number Keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Selecting standard");
            SetActiveBuildingType(buildingNameDictionary[BuildingTypeSO.BuildingName.STANDARD_TURRET]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Selecting ninja");
            SetActiveBuildingType(buildingNameDictionary[BuildingTypeSO.BuildingName.NINJA_TURRET]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Selecting banana");
            SetActiveBuildingType(buildingNameDictionary[BuildingTypeSO.BuildingName.BANANA_TURRET]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Selecting enemy");
            SetActiveBuildingType(buildingNameDictionary[BuildingTypeSO.BuildingName.NONE]);
        }

        if (Input.GetMouseButtonDown(0))
        {
            CreateBuilding(activeBuildingType);
        }
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

}
