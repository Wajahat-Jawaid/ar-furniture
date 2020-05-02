using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TakeNote : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler,IDragHandler ,IBeginDragHandler
{
    private Vector3 lastMousePosition;

    InputField inputField;
    Text txt;
    bool showIf = false;
    Vector3 originalPositon;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        txt = transform.GetChild(0).GetChild(0).GetChild(0). GetComponent<Text>();
    }
    public void setInputField(InputField inf)
    {
        inputField = inf;
        inputField.onValueChanged.AddListener(ChangeText);
    }
    // Update is called once per frame
    void ChangeText(string _txt)
    {
       txt.text = _txt;
    }
  /*
    private void OnMouseDown()
    {
      
    }
    private void OnMouseDrag()
    {
      
    }
    private void OnMouseUp()
    {
       

    }
    */
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(" OnPointerDown");

        showIf = true;
     
        originalPositon =  cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, cam.nearClipPlane));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(" OnPointerUp");

        if (showIf)
        {
            inputField.gameObject.SetActive(true);
            inputField.text = txt.text;
            inputField.Select();
            inputField.ActivateInputField();
            showIf = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(" Drag");
     
        Vector3 currentMousePosition = cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, cam.nearClipPlane));
        Vector3 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, diff.z);
        Vector3 oldPos = rect.position;
        rect.position = newPosition;
        if (!IsRectTransformInsideSreen(rect))
        {
            rect.position = oldPos;
        }
        lastMousePosition = currentMousePosition;
        if (transform.position != originalPositon)
        {
            showIf = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        lastMousePosition = cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, cam.nearClipPlane));

    }
    /// <summary>
    /// This methods will check is the rect transform is inside the screen or not
    /// </summary>
    /// <param name="rectTransform">Rect Trasform</param>
    /// <returns></returns>
    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners)
        {
            if (rect.Contains(corner))
            {
                visibleCorners++;
            }
        }
        if (visibleCorners == 4)
        {
            isInside = true;
        }
        return isInside;
    }
}
