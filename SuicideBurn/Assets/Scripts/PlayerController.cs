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
		if (ifMoving == false && transform.position.x < 2.5 && (touchControls.SwipeRight == true || Input.GetKeyDown(KeyCode.D)))
        {
            Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
            pos.x += 2.5f;
            StartCoroutine(MoveToPosition(pos, moveSpeed));
        }
        if (ifMoving == false && transform.position.x  > -2.5 && (touchControls.SwipeLeft == true || Input.GetKeyDown(KeyCode.A)))
        {
            Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
            pos.x += -2.5f;
            StartCoroutine(MoveToPosition(pos, moveSpeed));
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
