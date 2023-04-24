using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    BuildingTypeListSO buildingTypeList;
    BuildingTypeSO activeBuildingType;

    private void Awake() {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }
    private void Start() {
        activeBuildingType = buildingTypeList.list[0];
    }
    private void CreateBuilding(BuildingTypeSO buildingType) {
        Instantiate(buildingType.prefab, MousePointer.Instance.GetMouseWorldPosition(), Quaternion.identity);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.C)) {
            Debug.Log("Selecting circle");
            activeBuildingType = buildingTypeList.list[0];

        }
        if(Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("Selecting square");
            activeBuildingType = buildingTypeList.list[1];
        }
        if(Input.GetMouseButtonDown(0)){
            CreateBuilding(activeBuildingType);
        }
    }

}
