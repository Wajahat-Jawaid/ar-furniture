using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMove : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (transform.position.x + Time.deltaTime, transform.position.y);
		if (transform.position.x > 5)
			transform.position = new Vector2 (-5f, transform.position.y);
	}
}
