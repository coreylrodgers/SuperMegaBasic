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
    //Members
    BuildingTypeListSO buildingTypeList;
    BuildingTypeSO activeBuildingType;
    [SerializeField] GridItem activeGridItem;
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
        // Add buildingTypes to dictionary
        foreach (BuildingTypeSO bt in buildingTypeList.list)
        {
            buildingNameDictionary[bt.buildingName] = bt;
        }
    }
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }


    private void CreateBuilding(BuildingTypeSO buildingType)
    {
        GridItem activeGridItem = GridManager.Instance.GetActiveGridItem();

        if (GridManager.Instance.GetActiveGridItem() != null && !activeGridItem.GetGrid().IsGridTileOccupied()) {
            GameObject building = Instantiate(buildingType.prefab, activeGridItem.GetGrid().GetWorldPlacementPosition(UtilsClass.GetMouseWorldPosition()), Quaternion.identity);
            AssignGridToBuilding(building);
            building.gameObject.transform.Rotate(activeGridItem.GetRotation());
        }

    }

    private void AssignGridToBuilding(GameObject building)
    {
        GridItem gridToAssign = GridManager.Instance.GetActiveGridItem();
        building.GetComponent<GridHolder>().grid = gridToAssign.GetGrid();
        gridToAssign.GetGrid().SetGridTileOccupied(UtilsClass.GetMouseWorldPosition());
    }

    private void Update()
    {

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
            Debug.Log("Click");
            CreateBuilding(activeBuildingType);
        }
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

}
