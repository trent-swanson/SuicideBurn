using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // Get reference to GameManager
    private GameObject gameManager;
    // Get reference to TouchControls
    private Touch touchControls;
    // Get instance of GameController
    private GameController gameController;
    // Fuel slider
    public Slider fuelSlider;

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
        // Set fuel amount
        GameManager.fuel = 0.0f;
    }

    // Update is called once per frame
    void Update ()
    {
        // Update gauge
        FuelGauge();

        // If the player swipes right
		if (ifMoving == false && transform.position.x < 2f && (touchControls.SwipeRight == true || Input.GetKeyDown(KeyCode.D)))
        {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x + lanePos, transform.position.y, transform.position.z), moveSpeed));
        }
        // If the player swipes left
        if (ifMoving == false && transform.position.x > -2f && (touchControls.SwipeLeft == true || Input.GetKeyDown(KeyCode.A)))
        {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x - lanePos, transform.position.y, transform.position.z), moveSpeed));
        }
        // If the player breaks
        if (ifMoving == false && transform.position.y < 2.0f && (Input.GetKeyDown(KeyCode.W) || touchControls.Tap && !touchControls.Hold))
        {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x, 2.0f, transform.position.z), breakSpeed));
        }
        // Natural accel
        else
        {
            if (transform.position.y > 0.5f)
            {
                transform.position = transform.position += new Vector3(0, accelSpeed, 0);
            }
        }
    }

    // Fuel gauge
    void FuelGauge()
    {
        fuelSlider.value = GameManager.fuel;
    }

    // If a collision is made
    void OnTriggerEnter(Collider other)
    {
        // If collision is made with "Fuel"
        if (other.gameObject.tag == "Fuel")
        {
            GameManager.fuel += 0.1f;
        }

        // If collision is made with "Laser"
        if (other.gameObject.tag == "Laser")
        {
            // Destroy the player
            Destroy(gameObject);
            // Play an effect in their place
            Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            //gameController.isDead = true;
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
}