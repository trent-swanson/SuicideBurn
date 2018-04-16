using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Get reference to GameManager
    private GameObject gameManager;
    // Get reference to TouchControls
    private Touch touchControls;
    // Get instance of rigidbody
    private Rigidbody rb;

    private bool ifMoving = false;

    // Player movement speed
    [SerializeField]
    [Tooltip("Player movement speed.")]
    public float moveSpeed;
    public float lanePos;

	// Use this for initialization
	void Start ()
    {
        // Get reference to GameManager
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        // Get reference to TouchControls
        touchControls = gameManager.GetComponent<Touch>();
        // Get instance of rigidbody
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (ifMoving == false && transform.position.x < 2.5f && (touchControls.SwipeRight == true || Input.GetKeyDown(KeyCode.D)))
        {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x + lanePos, transform.position.y, transform.position.z), moveSpeed));
        }
        if (ifMoving == false && transform.position.x > -2.5f && (touchControls.SwipeLeft == true || Input.GetKeyDown(KeyCode.A)))
        {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x - lanePos, transform.position.y, transform.position.z), moveSpeed));
        }
        if (ifMoving == false && Input.GetKeyDown(KeyCode.W) || touchControls.Tap)
        {
            //break
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
