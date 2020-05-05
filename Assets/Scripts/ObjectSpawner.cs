using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public PlacementIndicator placementIndicator;
   public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

    }

    // Update is called once per frame
   public void place(GameObject obj)
        {
        GameObject go= Instantiate(obj, placementIndicator.transform.position,obj.transform.rotation) as GameObject;
        go.transform.SetParent(parent);
        }
    public void ClearAll()
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}

