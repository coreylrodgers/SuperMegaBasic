using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSelectUI : MonoBehaviour
{
    BuildingTypeListSO buildingTypeList;
    Dictionary<BuildingTypeSO, Transform> buildingTypeTransformDictionary;

    Dictionary<BuildingTypeSO, Transform> enemyTypeTransformDictionary;
    private void Awake()
    {
        Transform btnTemplate = transform.Find("btnTemplate");
        buildingTypeTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        enemyTypeTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        btnTemplate.gameObject.SetActive(false);
        buildingTypeList = Resources.Load<BuildingTypeListSO>((typeof(BuildingTypeListSO)).Name);

        //count index
        int index = 0;
        int offsetAmount = 55;

        //Instantiate new button 
        foreach (BuildingTypeSO bt in buildingTypeList.list)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);

            // Add reference to position of each button to the dictionary
            buildingTypeTransformDictionary[bt] = btnTransform;
            // Skip for the enemy
            if (bt.buildingName == BuildingTypeSO.BuildingName.NONE)
            {
                enemyTypeTransformDictionary[bt] = btnTransform;
                continue;
            }
            // Get the rect transform 
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            btnTransform.Find("sprite").GetComponent<Image>().sprite = bt.sprite;
            btnTransform.Find("text").GetComponent<TextMeshProUGUI>().text = bt.nameString;
            btnTransform.gameObject.SetActive(true);
            index++;
        }

        // Add the enemies
        foreach (BuildingTypeSO enemyStructure in enemyTypeTransformDictionary.Keys) 
        {
            Transform enemyButtonTransform = enemyTypeTransformDictionary[enemyStructure];
            enemyButtonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            enemyButtonTransform.Find("sprite").GetComponent<Image>().sprite = enemyStructure.sprite;
            enemyButtonTransform.Find("text").GetComponent<TextMeshProUGUI>().text = enemyStructure.nameString;
            enemyButtonTransform.gameObject.SetActive(true);
        }



    }

    private void Start()
    {
        // Subscribe to event in Building manager 
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;

        // Update the active building type button here on first run
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
        foreach (BuildingTypeSO bt in buildingTypeTransformDictionary.Keys)
        {
            // Get respective btn
            Transform btnTransform = buildingTypeTransformDictionary[bt];
            btnTransform.Find("base").GetComponent<Outline>().enabled = false;
        }

        // Set the selected outline on the UI element to active 
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        buildingTypeTransformDictionary[activeBuildingType].Find("base").GetComponent<Outline>().enabled = true;

    }
}

