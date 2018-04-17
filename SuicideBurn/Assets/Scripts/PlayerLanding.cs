using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerLanding : MonoBehaviour
{
    public float safeSpeed;
    private bool safelyHitGroundTrigger;
    public float currentSpeed;
    private GameObject speedHolder;

    private void CalcCurrentSpeed()
    {
        if (FindObjectOfType<SpeedHolder>() == null)
            return;

        currentSpeed = FindObjectOfType<SpeedHolder>().GetCurrentSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        CalcCurrentSpeed();
        if (other.tag == "GroundTrigger" && currentSpeed >= safeSpeed)
        {
            safelyHitGroundTrigger = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject.GetComponent<PlayerController>());
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
            GameManager.UpdatePersonCount(1);
        }
        //charactercontroller(groundexplosion)   
    }
}
