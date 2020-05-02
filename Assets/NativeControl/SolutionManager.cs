using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolutionManager : MonoBehaviour
{
    public RawImage preview;
    public Text GPSStatusTxt;
    // Start is called before the first frame update
    void Start()
    {
        NativeControl.Instance();  
    }
    public void StartGPS()
    {
        NativeControl.Instance().StartGPS();
    }
    public void CaptureAndPreview()
    {
        StartCoroutine(PreparePreview());
    }
    IEnumerator PreparePreview()
    {
        yield return new WaitForEndOfFrame();
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        preview.gameObject.SetActive(true);
        preview.texture = screenImage;
     }
    public void SaveToGallery()
    {
        Texture2D image = (Texture2D)preview.texture;
        NativeControl.Instance().SaveImageToGallery(image, SaveImageLocation);
    }
    public void SaveImageLocation(string path)
    {
        Debug.Log("####SaveImageLocation#### " + path);
    }

    /// <summary>
    /// If location service is not enable, location service screen shows.
    /// This method is responsible for location service sceen visibility.
    /// </summary>
    /// <param name="_status">Status.</param>
    public void LocationServiceScreenStatus(string _status)
    {
        switch (_status)
        {
            case "start":
                NativeControl.Instance().ShowToast("GPS Start", ToastLenght.LENGTH_LONG);
            break;

            case "stop":
            NativeControl.Instance().CheckLocationService();
            break;
        }
    }
    /// <summary>
    /// Gets the location service event.
    /// If user select high accuracy option in location service screen, return true in location status
    /// </summary>
    /// <param name="location_status">Location status.</param>
    public void GetLocationServiceEvent(string location_status)
    {
        
        switch(location_status)
        {
            case "true":
                GPSStatusTxt.text = "Mobile location service/GPS is start";
            break;

            case "false":
                GPSStatusTxt.text = "Mobile location service/GPS is not start";
                break;
        }
    }
    public void ShowToast()
    {
        NativeControl.Instance().ShowToast("Hello, Great Work!!!", ToastLenght.LENGTH_LONG);

    }
}    
   
