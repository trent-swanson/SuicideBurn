using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour {

    private GameObject gameManager;
    private Touch touchControls;
    Animator airBreaksAnimator;
    AudioSource podAudioSource;

    private GameObject cameraObject;

    private bool ifMoving = false;
    private bool hasMoved;
    private float currentHoldTime;

    // Player movement speed
    public float moveSpeed;

    [Space]
    public float lanePos;

    [Space]
    [Space]
    [Header("Effects")]
    public GameObject[] breakThrusters;
    public GameObject accelThruster;
    public GameObject reenteryEffects;
    public GameObject reenteryHotEffects;
    public GameObject airExplosion;
    public GameObject groundExplosion;
    public GameObject podMesh;

    [Space]
    [Space]
    [Header("Break Movement")]
    public float fuelCost;
    public float holdTime;
    public float minYShift;
    public float breakTime;
    public float accelerateTime;
    public AnimationCurve breakCurve;
    public AnimationCurve accelerateCurve;

    private float originalYPos;
    private float minYPos;

    bool dead = false;

	void Start () {
        GameManager.fuel = 1000;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        touchControls = gameManager.GetComponent<Touch>();
        airBreaksAnimator = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        podAudioSource = GetComponent<AudioSource>();
        originalYPos = transform.position.y;
        minYPos = transform.position.y + minYShift;
    }
	
	void Update () {
        if (!dead)
            CheckHeld();

        //move left & right
		if (!dead && ifMoving == false && transform.position.x < lanePos && (touchControls.SwipeRight == true || Input.GetKeyDown(KeyCode.D))) {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x + lanePos, transform.position.y, transform.position.z), moveSpeed));
        }
        if (!dead && ifMoving == false && transform.position.x > -lanePos && (touchControls.SwipeLeft == true || Input.GetKeyDown(KeyCode.A))) {
            StartCoroutine(MoveToPosition(new Vector3(transform.position.x - lanePos, transform.position.y, transform.position.z), moveSpeed));
        }

        //break & accelorate
        if (touchControls.Hold && !dead && GameManager.fuel > 0) {
            GameManager.UpdateFuel(-fuelCost * Time.deltaTime);
            airBreaksAnimator.SetBool("airBreaks", true);
            reenteryHotEffects.SetActive(false);
            accelThruster.SetActive(false);
            podAudioSource.volume = 1;
            foreach (GameObject thruster in breakThrusters) {
                thruster.SetActive(true);
            }
        } else if (!dead) {
            airBreaksAnimator.SetBool("airBreaks", false);
            reenteryHotEffects.SetActive(true);
            accelThruster.SetActive(true);
            podAudioSource.volume = 0.45f;
            foreach (GameObject thruster in breakThrusters) {
                thruster.SetActive(false);
            }
        }

        //Debugging
        if(Input.GetKeyDown(KeyCode.Space)) {
            AirExplode();
        }
    }

    public IEnumerator MoveToPosition(Vector3 position, float timeToMove) {
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

    private void CheckHeld() {
        if (touchControls.Hold && GameManager.fuel > 0) {
            currentHoldTime += Time.deltaTime;

            if(currentHoldTime >= holdTime && hasMoved == false) {
                hasMoved = true;
                StopAllCoroutines();
                StartCoroutine(BreakMove(minYPos, breakTime, breakCurve));
                currentHoldTime = 0f;
            }
        } else {
            currentHoldTime = 0f;

            if (hasMoved) {
                hasMoved = false;
                StopAllCoroutines();
                StartCoroutine(BreakMove(originalYPos, accelerateTime, accelerateCurve));
            }
        }
    }

    private IEnumerator BreakMove(float position, float lerpTime, AnimationCurve curve) {
        float t = 0f;
        float startPostion = transform.position.y;
        float currentPosition = 0f;

        while (t <= lerpTime) {
            t += Time.deltaTime;
            currentPosition = Mathf.LerpUnclamped(startPostion, position, curve.Evaluate(t / lerpTime));
            transform.position = new Vector3(transform.position.x, currentPosition, transform.position.z);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Obsticle") {
            AirExplode();
        }
    }

    public void AirExplode() {
        dead = true;
        airExplosion.SetActive(true);
        podMesh.SetActive(false);
        reenteryEffects.SetActive(false);
        StartCoroutine(Die());
    }

    public void GroundExplode() {
        dead = true;
        groundExplosion.SetActive(true);
        podMesh.SetActive(false);
        reenteryEffects.SetActive(false);
        StartCoroutine(Die());
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DisableInput() {
        dead = true;
    }
}
