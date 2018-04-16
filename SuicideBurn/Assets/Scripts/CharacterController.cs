using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private GameObject gameManager;
    private Touch touchControls;

    private GameObject cameraObject;

    private bool ifMoving = false;

    // Player movement speed
    public float moveSpeed;
    public float breakSpeed;
    public float accelSpeed;

    [Space]
    public float lanePos;

	void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        touchControls = gameManager.GetComponent<Touch>();
    }
	
	void Update ()
    {
        //move left & right
		if (ifMoving == false && transform.position.x < 2.5f && (touchControls.SwipeRight == true || Input.GetKeyDown(KeyCode.D))) {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x + lanePos, transform.position.y, transform.position.z), moveSpeed));
        }
        if (ifMoving == false && transform.position.x > -2.5f && (touchControls.SwipeLeft == true || Input.GetKeyDown(KeyCode.A))) {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x - lanePos, transform.position.y, transform.position.z), moveSpeed));
        }

        //break & accelorate
        if (Input.GetKey(KeyCode.W) || touchControls.Tap) {
            //move cameraObject down
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
