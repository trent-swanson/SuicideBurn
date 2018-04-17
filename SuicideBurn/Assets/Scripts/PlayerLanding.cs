using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanding : MonoBehaviour
{
    private bool hasFound = false;
    private float currentSpeed;
    private GameObject speedHolder;
    // Use this for initialization
    void Start()
    {
        //speedHolder = GameObject.Find("SpeedHolder(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        //currentSpeed = speedHolder.GetComponent<SpeedHolder>().GetCurrentSpeed();
        currentSpeed = GameObject.Find("SpeedHolder(Clone)").GetComponent<SpeedHolder>().GetCurrentSpeed();
        //Debug.Log(currentSpeed);
    }
    
}
