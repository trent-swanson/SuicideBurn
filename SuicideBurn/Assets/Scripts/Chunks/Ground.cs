using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y >= -0.5)
        {
            Destroy(gameObject.GetComponent<SceneMover>());
        }
    }
}
