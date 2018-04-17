using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerLanding : MonoBehaviour
{
    
    public float safeSpeed;
    private bool safelyHitGroundTrigger;
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
        if (other.tag == "GroundTrigger" && currentSpeed >= safeSpeed)
        {
            safelyHitGroundTrigger = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (other.tag == "GroundTrigger" && currentSpeed <= safeSpeed)
        {
            safelyHitGroundTrigger = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (other.tag == "Ground" && !safelyHitGroundTrigger)
        {


            // play explode animation
            // game over
            //Destroy(gameObject.GetComponent<MeshRenderer>());
            transform.GetComponent<CharacterController>().GroundExplode();
            
        }

        else if(other.tag == "Ground"  && safelyHitGroundTrigger)
        {
            // land safely
            GameManager.personCount++;
            SceneManager.LoadScene(0);
        }

        //charactercontroller(groundexplosion)   
    }
}
