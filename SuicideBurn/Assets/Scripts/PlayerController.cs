using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Get reference to GameManager
    private GameObject gameManager;
    // Get reference to TouchControls
    private Touch touchControls;
    // Get instance of GameController
    private GameController gameController;

    private bool ifMoving = false;

    // Player movement speed
    [SerializeField]
    [Tooltip("Player movement speed.")]
    public float moveSpeed;
    public float lanePos;
    // Player break speed
    [SerializeField]
    [Tooltip("Player break speed.")]
    public float breakSpeed;
    // Acceleration speed
    [SerializeField]
    [Tooltip("Acceleration speed.")]
    public float accelSpeed;

    public GameObject deathEffect;

    // Use this for initialization
    void Start ()
    {
        // Get reference to GameManager
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        // Get reference to TouchControls
        touchControls = gameManager.GetComponent<Touch>();
        // Get instance of GameController
        gameController = gameManager.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update ()
    {
        // If the player swipes right
		if (ifMoving == false && transform.position.x < 2.5f && (touchControls.SwipeRight == true || Input.GetKeyDown(KeyCode.D)))
        {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x + lanePos, transform.position.y, transform.position.z), moveSpeed));
        }
        // If the player swipes left
        if (ifMoving == false && transform.position.x > -2.5f && (touchControls.SwipeLeft == true || Input.GetKeyDown(KeyCode.A)))
        {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x - lanePos, transform.position.y, transform.position.z), moveSpeed));
        }
        // If the player breaks
        if (ifMoving == false && transform.position.y < 2.0f && (Input.GetKeyDown(KeyCode.W) || touchControls.Tap))
        {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x, 2.0f, transform.position.z), breakSpeed));
        }
        // Natural accel
        else
        {
            if (transform.position.y > 0.5f)
            {
                StartCoroutine(AccelToPosition(new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z), accelSpeed));
            }
        }
    }

    // If a collision is made
    void OnCollisionEnter(Collision collision)
    {
        // If collision is made with "Coin"
        if (collision.gameObject.tag == "Coin")
        {
            GameManager.score += 10;
        }

        // If collision is made with "Laser"
        if (collision.gameObject.tag == "Laser")
        {
            // Destroy the player
            Destroy(gameObject);
            // Play an effect in their place
            Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            gameController.isDead = true;
            gameController.GameOver();
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

    public IEnumerator AccelToPosition(Vector3 position, float timeToMove)
    {
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}
