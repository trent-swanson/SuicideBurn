using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private GameObject gameManager;
    private Touch touchControls;
    Animator airBreaksAnimator;

    private GameObject cameraObject;

    private bool ifMoving = false;

    // Player movement speed
    public float moveSpeed;

    [Space]
    public float lanePos;

    [Space]
    [Space]
    public GameObject[] breakThrusters;
    public GameObject accelThruster;

	void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        touchControls = gameManager.GetComponent<Touch>();
        airBreaksAnimator = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
    }
	
	void Update ()
    {
        //move left & right
		if (ifMoving == false && transform.position.x < lanePos && (touchControls.SwipeRight == true || Input.GetKeyDown(KeyCode.D))) {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x + lanePos, transform.position.y, transform.position.z), moveSpeed));
        }
        if (ifMoving == false && transform.position.x > -lanePos && (touchControls.SwipeLeft == true || Input.GetKeyDown(KeyCode.A))) {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x - lanePos, transform.position.y, transform.position.z), moveSpeed));
        }

        //break & accelorate
        if (Input.GetKey(KeyCode.W) || touchControls.Hold) {
            airBreaksAnimator.SetBool("airBreaks", true);
            accelThruster.SetActive(false);
            foreach (GameObject thruster in breakThrusters) {
                thruster.SetActive(true);
            }
        } else {
            airBreaksAnimator.SetBool("airBreaks", false);
            accelThruster.SetActive(true);
            foreach (GameObject thruster in breakThrusters) {
                thruster.SetActive(false);
            }
        }
    }

    public IEnumerator MoveToPosition(Vector3 position, float timeToMove)
    {
        ifMoving = true;
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        ifMoving = false;
    }
}
