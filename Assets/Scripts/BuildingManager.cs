using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    BuildingTypeListSO buildingTypeList;
    BuildingTypeSO activeBuildingType;
    Dictionary<BuildingTypeSO.BuildingName, BuildingTypeSO> buildingNameDictionary;

  

    private void Awake() {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingNameDictionary = new Dictionary<BuildingTypeSO.BuildingName, BuildingTypeSO>();
    }
    private void Start() {
        activeBuildingType = buildingTypeList.list[0];

        // Add buildingTypes to dictionary
        foreach(BuildingTypeSO bt in buildingTypeList.list) {
            buildingNameDictionary[bt.buildingName] = bt;
        }
    }
    private void CreateBuilding(BuildingTypeSO buildingType) {
        Instantiate(buildingType.prefab, MousePointer.Instance.GetMouseWorldPosition(), Quaternion.identity);
    }

    private void Update(){


        //Manage Selection Using the Number Keys
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("Selecting standard");
            activeBuildingType = buildingNameDictionary[BuildingTypeSO.BuildingName.STANDARD_TURRET];

        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("Selecting ninja");
            activeBuildingType = buildingNameDictionary[BuildingTypeSO.BuildingName.NINJA_TURRET];
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            Debug.Log("Selecting banana");
            activeBuildingType = buildingNameDictionary[BuildingTypeSO.BuildingName.BANANA_TURRET];
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            Debug.Log("Selecting banana");
            activeBuildingType = buildingNameDictionary[BuildingTypeSO.BuildingName.NONE];
        }



        if(Input.GetMouseButtonDown(0)){
            CreateBuilding(activeBuildingType);
        }
    }

}
