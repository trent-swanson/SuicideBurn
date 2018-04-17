using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedHolder : MonoBehaviour
{
    float speed = 0;

    // Update is called once per frame
    void Update()
    {
        speed = gameObject.GetComponent<SceneMover>().currentSpeed;
        //if(gameObject.transform.position.y >= 10)
        //{
        //    gameObject.transform.SetPositionAndRotation(new Vector3(0, 0, -30), Quaternion.identity);
        //}
    }

    public float GetCurrentSpeed()
    {
        return speed;
    }
}
