using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMover : MonoBehaviour {

    public GameObject shuffleBag;
    // speed that it rises.
    [Tooltip("This is the speed that the world rises by.")]
    public float riseSpeed = 2;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.Translate(Vector3.up * Time.deltaTime * riseSpeed, Space.World);
	}
}
