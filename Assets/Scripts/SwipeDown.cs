using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.EventSystems;

public class SwipeDown : MonoBehaviour,IEndDragHandler
{

    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        this.gameObject.SetActive(false);
    }
    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }
}
