using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    public Transform camera;
    public GameObject infoTxt;
    public GameObject measureBtn;
    public GameObject furnScrollView;
    public GameObject screenShotBtn;
    public GameObject converterBtn;
    public GameObject converterPanel;
    public GameObject addnote;
    public GameObject noteInputField;
    private ARRaycastManager rayManager;
    private GameObject visual;

    bool up = false;
    // Start is called before the first frame update
    void Start()
    {
        measureBtn.SetActive(false);
        furnScrollView.SetActive(false);
        screenShotBtn.SetActive(false);
        converterBtn.SetActive(false);
        converterPanel.SetActive(false);
        addnote.SetActive(false);
        noteInputField.SetActive(false);
        rayManager =FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
        visual.SetActive(false);
    }
    public void showConverterPanel()
    {
        converterPanel.GetComponent<RectTransform>().DOAnchorPosY(converterPanel.GetComponent<RectTransform>().anchoredPosition.y *-1f, .5f);
    }
    // Update is called once per frame
    void Update()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        // if we hit an AR plane, update the position and rotation
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
                infoTxt.SetActive(false);
                measureBtn.SetActive(true);
                furnScrollView.SetActive(true);
                converterBtn.SetActive(true);
                converterPanel.SetActive(true);
                screenShotBtn.SetActive(true);
                addnote.SetActive(true);
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Elevation")
            {
                transform.position = new Vector3(hit.collider.transform.position.x,hit.point.y, hit.collider.transform.position.z);
            }
        }

    }
}
