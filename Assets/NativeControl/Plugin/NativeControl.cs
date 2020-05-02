using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ToastLenght { LENGTH_SHORT, LENGTH_LONG }
public class NativeControl
{
    AndroidJavaClass Androidjavaclass = null;
    private static NativeControl nativeControl = null;
    public ToastLenght eToastLenght = ToastLenght.LENGTH_SHORT;

    public static NativeControl Instance()
    {
        if(nativeControl==null)
        {
            nativeControl = new NativeControl();

        }
        return nativeControl;
    }
    NativeControl()
    {
        Androidjavaclass = new AndroidJavaClass("com.codemaster.android.ActivityControl");
    }
    public void StartGPS()
    {
        Androidjavaclass.CallStatic("EnableLocationPopup");
    }
    public void SaveImageToGallery(Texture2D texture2D, System.Action<string> _callbackImagePath)
    {
        Texture2D screenImage = texture2D;
        byte[] imageBytes = screenImage.EncodeToPNG();
        object[] objects = { imageBytes };
        string pathofimage = Androidjavaclass.CallStatic<string>("saveImageToGallery", objects);
        _callbackImagePath(pathofimage);
        Debug.Log("==Save image path==" + pathofimage);
    }
    public void CheckLocationService()
    {
        Androidjavaclass.CallStatic("CheckLocationService");

    }
    public void ShowToast(string _message,ToastLenght _type)
    {
        int type = 0;
        if(_type == ToastLenght.LENGTH_SHORT)
        {
            type = 0;
        }
        else
        {
            type = 1;
        }
        object[] objects = { _message, type };
        Androidjavaclass.CallStatic("ShowToast", objects);

    }
}
