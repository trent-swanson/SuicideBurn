using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanding : MonoBehaviour
{
    
    public float safeSpeed;

    private float currentSpeed;
    private GameObject speedHolder;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


    private void CalcCurrentSpeed()
    {
        currentSpeed = GameObject.Find("SpeedHolder(Clone)").GetComponent<SpeedHolder>().GetCurrentSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        CalcCurrentSpeed();

        if (currentSpeed >= safeSpeed)
        {
            // explode and die
            // game over
            Destroy(gameObject.GetComponent<MeshRenderer>());
        }
    }
}
