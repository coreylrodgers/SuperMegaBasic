using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectUI : MonoBehaviour
{
    BuildingTypeListSO buildingTypeList;
    Dictionary<BuildingTypeSO, Transform> buildingTypeTransformDictionary;
    private void Awake()
    {
        Transform btnTemplate = transform.Find("btnTemplate");
        buildingTypeTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        btnTemplate.gameObject.SetActive(false);
        buildingTypeList = Resources.Load<BuildingTypeListSO>((typeof(BuildingTypeListSO)).Name);

        //count index
        int index = 0;
        int offsetAmount = 55;

        //Instantiate new button 
        foreach (BuildingTypeSO bt in buildingTypeList.list)
        {
            if (bt.buildingName == BuildingTypeSO.BuildingName.NONE) continue;
            Transform btnTransform = Instantiate(btnTemplate, transform);

            // Add reference to position of each button to the dictionary
            buildingTypeTransformDictionary[bt] = btnTransform;
            // Get the rect transform 
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            btnTransform.Find("sprite").GetComponent<Image>().sprite = bt.sprite;
            btnTransform.gameObject.SetActive(true);
            index++;
        }

    }

    private void Start()
    {
        // Subscribe to event in Building manager 
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;

        // Update the Sactive building type button here on first run
        UpdateActiveBuildingTypeButton();
    }

    // Events emitted via the Building manager
    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        Debug.Log("Event!");
        UpdateActiveBuildingTypeButton();
    }
    private void UpdateActiveBuildingTypeButton()
    {
        // Loop over Tranforms of the UI buttons
        foreach(BuildingTypeSO bt in buildingTypeTransformDictionary.Keys) {
            // Get respective btn
            Transform btnTransform = buildingTypeTransformDictionary[bt];
            btnTransform.Find("base").GetComponent<Outline>().enabled = false;
        }

        // Set the selected outline on the UI element to active 
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        buildingTypeTransformDictionary[activeBuildingType].Find("base").GetComponent<Outline>().enabled = true;

    }
}

