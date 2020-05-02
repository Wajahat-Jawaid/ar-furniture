using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    //Initialize Variables
    GameObject getTarget;
    bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 positionOfScreen;
    public GameObject converPanel;

  
    void Update()
    {

        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {

 RaycastHit hitInfo;
            getTarget = ReturnClickedObject(out hitInfo);
            if (getTarget != null)
            {
                isMouseDragging = true;
                //Converting world position to screen position.
                positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);          
                offsetValue = getTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z));
            }
        }

        //Mouse Button Up
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }

        //Is mouse Moving
        if (isMouseDragging)
        {
            //tracking mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z);

            //converting screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offsetValue;
            if (getTarget.tag == "AxisDrag")
            {
                //It will update target gameobject's current postion.
                getTarget.transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);
            }
            else
            {
                //It will update target gameobject's current postion.
                getTarget.transform.position = new Vector3(currentPosition.x, getTarget.transform.position.y, currentPosition.z);
            }
          
        }


    }

    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

}
