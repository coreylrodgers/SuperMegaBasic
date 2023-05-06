using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public static MousePointer Instance {get; private set;}
    private void Awake() {
        Instance = this;
    }

    public Vector3 GetMouseWorldPosition() {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    private void Update() {
        // get position of mouse
        float y = Input.mousePosition.y;
        float x = Input.mousePosition.x;
        this.transform.position = GetMouseWorldPosition();
    }

}
