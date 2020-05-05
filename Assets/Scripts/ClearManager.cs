using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
    public Measurement measurement;
    public ObjectSpawner spawner;
    public NoteSpawner nspawner;
    public GameObject panel;
    public Toggle tog;
    int showPanel;
    private void Start()
    {
        showPanel = PlayerPrefs.GetInt("ShowClearPanel");
        tog.onValueChanged.AddListener(setShow);
    }
    // Start is called before the first frame update
   public void headerClearAll()
    {
        if (showPanel > 0)
        {
            clearAll();
        }
        else
        {
            panel.SetActive(true);
        }
    }
   public void clearAll()
    {
        measurement.ClearAll();
        spawner.ClearAll();
        nspawner.ClearAll();
    }

   public  void setShow(bool v)
    {

        if (v)
        {
            showPanel = 1;
            PlayerPrefs.SetInt("ShowClearPanel", 1);
        }
        else
        {
            showPanel = 0;
            PlayerPrefs.SetInt("ShowClearPanel", 0);

        }
    }
}
