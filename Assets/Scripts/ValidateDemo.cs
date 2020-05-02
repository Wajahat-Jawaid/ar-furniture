using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class ValidateDemo : MonoBehaviour
{
    //public GameObject validationPanel;
    public GameObject[] hide;
    public TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("http://levitete.com/interwood_demo/demo_validation.php"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
                NativeControl.Instance().ShowToast("Error: " + webRequest.error, ToastLenght.LENGTH_LONG);
                Application.Quit();
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
         //       NativeControl.Instance().ShowToast("Received: " + webRequest.downloadHandler.text,ToastLenght.LENGTH_LONG);
                API api = JsonUtility.FromJson<API>(webRequest.downloadHandler.text);
                if (api.status==0)
                {
                    //validationPanel.SetActive(true);
                    txt.text = api.message;
                    for (int i = 0; i < hide.Length; i++)
                    {
                        hide[i].SetActive(false);
                    }
                }
              


            }
        }
    }
}
[Serializable]
public class API
{
    public int status;
   public string message;
}