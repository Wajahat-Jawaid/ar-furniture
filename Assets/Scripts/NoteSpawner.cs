using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSpawner : MonoBehaviour
{
    public PlacementIndicator placementIndicator;
    public GameObject note;
    public InputField noteif; public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

    }
    public void ClearAll()
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    // Update is called once per frame
    public void placeNote()
    {
       GameObject noteObj= Instantiate(note, placementIndicator.transform.position, Quaternion.identity) as GameObject;
        noteObj.transform.SetParent(parent);

        noteObj.GetComponent<TakeNote>().setInputField(noteif);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            noteif.gameObject.SetActive(false);
            Debug.Log("input closed");
        }
    }
}

   
