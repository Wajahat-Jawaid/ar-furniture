using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    public LineRenderer line;
    public BoxCollider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = line.GetPosition(0);
        Vector3 endPos = line.GetPosition(1);
        float lineLength = Vector3.Distance(startPos, endPos); // length of line

        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (line.GetPosition(0)+ line.GetPosition(1)) / 2;
        col.transform.position = midPoint; // setting position of collider object   

    }
}
