using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class TestingScript : MonoBehaviour
{
    private Grid topPlacement;
    private Grid bottomPlacement;
    private void Start(){
        topPlacement = new Grid(16, 2, 1f, new Vector3(-8.5f, 2.5f), "top");
        bottomPlacement = new Grid(16,2, 1f, new Vector3(-8.5f, -4) ,"bottom");
    }
    private void Update(){
        if(Input.GetMouseButtonDown(0)) {
            topPlacement.SetValue(UtilsClass.GetMouseWorldPosition(),56 );
            bottomPlacement.SetValue(UtilsClass.GetMouseWorldPosition(),56 );
        }
        if(Input.GetMouseButtonDown(1)) {
            topPlacement.GetValue(UtilsClass.GetMouseWorldPosition());
            bottomPlacement.GetValue(UtilsClass.GetMouseWorldPosition());

        }
    }
}
