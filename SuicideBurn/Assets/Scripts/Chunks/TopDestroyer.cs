using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDestroyer : MonoBehaviour
{
    public GameObject shuffleBag;

    private bool isCollidingwithDestroyer;

	// Use this for initialization
	void Start ()
    {
        isCollidingwithDestroyer = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(isCollidingwithDestroyer);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destroyer")
        {
            shuffleBag.GetComponent<ShuffleBag>().loadedChunks.RemoveAt(0);
            Destroy(gameObject);
            isCollidingwithDestroyer = true;
        }
    }
}
