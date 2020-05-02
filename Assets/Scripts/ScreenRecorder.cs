using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ScreenRecorder : MonoBehaviour
 {
    public GameObject hideGameObject;
    public void CaptureScreenshot()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }


    private IEnumerator TakeScreenshotAndSave()
    {
        hideGameObject.SetActive(false);
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        NativeControl.Instance().ShowToast("Saving...", ToastLenght.LENGTH_SHORT);

        // Save the screenshot to Gallery/Photos
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, "ARFurniture", "screenShot.png"));

        yield return new WaitForEndOfFrame();
        NativeControl.Instance().ShowToast("Saved",ToastLenght.LENGTH_SHORT);

        hideGameObject.SetActive(true);

        // To avoid memory leaks
        Destroy(ss);
    }
  
}
 