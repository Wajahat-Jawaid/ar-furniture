using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
public class Measurement : MonoBehaviour
{
    [SerializeField]
    Transform parent;
    [SerializeField]
    private GameObject MeasurePointPrefaab;
    [SerializeField]
    private GameObject distanceTxtPrefab;
    [SerializeField]
    private float measureFactor = 39.37f;
    [SerializeField]
    private Vector3 offsetMeasurement = Vector3.zero;
    [SerializeField]
    private ARCameraManager aRCameraManager;
    [SerializeField]
    private GameObject[] startPoints;
    private GameObject[] endPoints;
    private TextMeshProUGUI[] distanceTxts;
    private Vector2 touchPosition = default;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public PlacementIndicator placementIndicator;
    bool startMeasuring;
    int currentIndex; public float FixedSize = 1;
    Vector3 fixedScale;
    public enum Units
    {
        m_cm,ft_in,inch
    }
     Units unit= Units.m_cm;
    private void Start()
    {
   
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        startPoints = new GameObject[100];
        endPoints = new GameObject[100];
        distanceTxts = new TextMeshProUGUI[100];
    }

    public void setUnit(int _unit)
    {
        if (_unit==0)
        {
            unit = Units.m_cm;
        }
        else if(_unit==1)
        {
            unit = Units.ft_in;
        }
        else
        {
            unit = Units.inch;
        }
    }

    public void placeMeasurePoints()
    {
        if (!startMeasuring)
        {
            startPoints[currentIndex] = Instantiate(MeasurePointPrefaab, placementIndicator.transform.position, Quaternion.identity);
            startPoints[currentIndex].transform.SetParent(parent);
            endPoints[currentIndex] = Instantiate(MeasurePointPrefaab, placementIndicator.transform.position, Quaternion.identity);
            endPoints[currentIndex].transform.SetParent(parent);

            endPoints[currentIndex].transform.GetChild(0).gameObject.SetActive(false);
            GameObject dtxt = Instantiate(distanceTxtPrefab, endPoints[currentIndex].transform.position + .2f * Vector3.Normalize(startPoints[currentIndex].transform.position - endPoints[currentIndex].transform.position)
, Quaternion.identity);
            dtxt.transform.SetParent(parent);

            distanceTxts[currentIndex]= dtxt.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            float camDist = Vector3.Distance(Camera.main.transform.position, dtxt.transform.position);

            dtxt.transform.localScale = dtxt.transform.localScale * camDist;
            fixedScale = dtxt.transform.localScale;
            Color color = UnityEngine.Random.ColorHSV();
          
          
                    startMeasuring = true;
          
            //Input.gyro.enabled = true;
        }
        else
        {
            startMeasuring = false;
            startPoints[currentIndex].GetComponent<LineRenderer>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (startMeasuring)
        {

            LineRenderer hml = startPoints[currentIndex].GetComponent<LineRenderer>();
            LineRenderer wml = endPoints[currentIndex].GetComponent<LineRenderer>();
           
            endPoints[currentIndex].transform.SetPositionAndRotation(placementIndicator.transform.position, Quaternion.identity);

            startPoints[currentIndex].GetComponent<LineRenderer>().enabled = true;
            wml.SetPosition(0, startPoints[currentIndex].transform.position);
            wml.SetPosition(1, endPoints[currentIndex].transform.position);
            hml.startColor = Color.white;
            hml.endColor = Color.white;
            hml.SetPosition(0, startPoints[currentIndex].transform.position);
            hml.SetPosition(1, startPoints[currentIndex].transform.position+new Vector3(0,15,0));     
            float dist = Vector3.Distance(startPoints[currentIndex].transform.position, endPoints[currentIndex].transform.position);
            float camdist = Vector3.Distance(Camera.main.transform.position, distanceTxts[currentIndex].transform.parent.parent.position);
            distanceTxts[currentIndex].transform.parent.parent.position = endPoints[currentIndex].transform.position + .2f *dist* Vector3.Normalize(startPoints[currentIndex].transform.position - endPoints[currentIndex].transform.position);

            if ( camdist%100 >1 )
            {
                distanceTxts[currentIndex].transform.parent.parent.localScale = fixedScale * (camdist % 100);
            }

            if (unit==Units.m_cm)
            {
                if (dist < 1)
                {
                    distanceTxts[currentIndex].text = (dist*100).ToString("0") + " cm";
                }
                else
                {
                    distanceTxts[currentIndex].text = dist.ToString("0.00") + " m";

                }
            }

            else if (unit == Units.ft_in)
            {
                dist *= measureFactor;
                distanceTxts[currentIndex].text =Math.Truncate(dist / 12).ToString("0") + "ft "+ (dist % 12).ToString("0") + "in";
            }
            else
            {
                dist *= measureFactor;
                distanceTxts[currentIndex].text = dist.ToString("0")+"in";
            }

        }
    }
    public void ClearAll()
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
        currentIndex = 0;
    }
    void clearStartPoints(int index)
    {
        for (int i = 0; i <= index; i++)
        {
            GameObject go = startPoints[i];
            startPoints[i] = null;
            Destroy(go);
        }
    }
    void clearEndPoints(int index)
    {
        for (int i = 0; i <= index; i++)
        {
            GameObject go1 = endPoints[i];
            endPoints[i] = null;
            Destroy(go1);
        }
    }
    void cleardistanceTxts(int index)
    {
        for (int i = 0; i <= index; i++)
        {
                       GameObject go2 = distanceTxts[i].gameObject;
            distanceTxts[i] = null;
            Destroy(go2);
        }
    }
    public bool campareColor(Color c1, Color c2)
    {
        if (c1.r == c2.r && c1.g == c2.g&& c1.b == c2.b&& c1.a == c2.a)
        {
            return true;
        }
        return false;
    }
}

