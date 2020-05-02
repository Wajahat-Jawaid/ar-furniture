using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
     GameObject MainCamera;

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(this.transform.rotation, MainCamera.transform.rotation, Speed * Time.deltaTime);

    }
}
