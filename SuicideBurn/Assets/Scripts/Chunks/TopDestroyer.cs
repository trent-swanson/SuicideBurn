using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDestroyer : MonoBehaviour
{
    public GameObject shuffleBag;

    // This counts how many times a chunk has been destroyed.
    public int chunkDestroyCounter = 0;

    private bool isCollidingwithDestroyer;

	// Use this for initialization
	void Start ()
    {
        isCollidingwithDestroyer = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(isCollidingwithDestroyer);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destroyer")
        {
            ++chunkDestroyCounter;
            shuffleBag.GetComponent<ShuffleBag>().loadedChunks.RemoveAt(1);
            Destroy(gameObject);
            isCollidingwithDestroyer = true;
        }
    }
}
