using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject 
{  public enum BuildingName {
        STANDARD_TURRET,
        NINJA_TURRET,
        BANANA_TURRET,
        NONE
    }
    public string nameString;
    public BuildingName buildingName;
    public GameObject prefab;
    public Projectile projectile;
    public Sprite sprite;
    public float range = 5f;
    public float reloadSpeed = 1f;
    public float fov = 45f;
    public float scanSpeed = 80f;
    
}
