using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSpawner : MonoBehaviour
{
    public PlacementIndicator placementIndicator;
    public GameObject note;
    public InputField noteif;
    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

    }

    // Update is called once per frame
    public void placeNote()
    {
       GameObject noteObj= Instantiate(note, placementIndicator.transform.position, Quaternion.identity) as GameObject;
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

   
