using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickListener : MonoBehaviour
{

    public GameObject obj;
    public ObjectSpawner objSpawner;
    // Start is called before the first frame update
    void Start()
    {
        objSpawner = FindObjectOfType<ObjectSpawner>();
        this.GetComponent<Button>().onClick.AddListener(()=> objSpawner.place(obj)); 
    }

    // Update is called once per frame
   
}
