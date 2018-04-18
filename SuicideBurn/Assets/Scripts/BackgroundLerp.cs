using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLerp : MonoBehaviour {
    public float moveSpeed;
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
	}
}
