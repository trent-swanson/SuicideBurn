using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerLanding : MonoBehaviour
{
    public float safeSpeed;
    public float currentSpeed;
    //public GameObject airExplosion;
    public GameObject groundExplosion;

    private GameController gameController;
    private bool safelyHitGroundTrigger;
    private GameObject speedHolder;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

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
            gameObject.GetComponent<CharacterController>().enabled = false;
        }
        else if (other.tag == "GroundTrigger" && currentSpeed <= safeSpeed)
        {
            safelyHitGroundTrigger = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (other.tag == "Ground" && !safelyHitGroundTrigger)
        {
            // Explode
            GroundExplode();   
        }

        else if(other.tag == "Ground"  && safelyHitGroundTrigger)
        {
            // land safely
            GameManager.UpdatePersonCount(1);
            gameController.VictoryPanel();
            
        }  
    }

    public void GroundExplode()
    {
        //dead = true;
        groundExplosion.SetActive(true);
        //podMesh.SetActive(false);
        //reenteryEffects.SetActive(false);
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
