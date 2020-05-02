using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public PlacementIndicator placementIndicator;

    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

    }

    // Update is called once per frame
   public void place(GameObject obj)
        {         
               Instantiate(obj, placementIndicator.transform.position,obj.transform.rotation);        
        }
    }

