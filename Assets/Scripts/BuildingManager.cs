using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance {get; private set;}

    //Event handler
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs {
        public BuildingTypeSO activeBuildingType;
    }

    //Members
    BuildingTypeListSO buildingTypeList;
    BuildingTypeSO activeBuildingType;
    Dictionary<BuildingTypeSO.BuildingName, BuildingTypeSO> buildingNameDictionary;
  
    private void Awake() {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingNameDictionary = new Dictionary<BuildingTypeSO.BuildingName, BuildingTypeSO>();
        activeBuildingType = buildingTypeList.list[0];
    }
    private void Start() {

        // Add buildingTypes to dictionary
        foreach(BuildingTypeSO bt in buildingTypeList.list) {
            buildingNameDictionary[bt.buildingName] = bt;
        }
    }
    public void SetActiveBuildingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;
        // Fire off event for other classes to use
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType});
    }
    private void CreateBuilding(BuildingTypeSO buildingType) {
        Instantiate(buildingType.prefab, MousePointer.Instance.GetMouseWorldPosition(), Quaternion.identity);
    }

    private void Update(){

        //Manage Selection Using the Number Keys
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("Selecting standard");
            SetActiveBuildingType(buildingNameDictionary[BuildingTypeSO.BuildingName.STANDARD_TURRET]);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("Selecting ninja");
            SetActiveBuildingType(buildingNameDictionary[BuildingTypeSO.BuildingName.NINJA_TURRET]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            Debug.Log("Selecting banana");
            SetActiveBuildingType(buildingNameDictionary[BuildingTypeSO.BuildingName.BANANA_TURRET]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            Debug.Log("Selecting enemy");
            SetActiveBuildingType(buildingNameDictionary[BuildingTypeSO.BuildingName.NONE]);
        }

        if(Input.GetMouseButtonDown(0)){
            CreateBuilding(activeBuildingType);
        }
    }

    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }

}
