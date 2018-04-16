using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float holdTime;
    public float maxYPosition;
    public float moveTime;
    public AnimationCurve moveCurve;

    private Touch touch;
    private float originalYPosition;
    private float currentHoldTime;

    private bool isMoving;
    private bool hasMoved;

    private void Start() {
        touch = GameObject.FindGameObjectWithTag("GameController").GetComponent<Touch>();
        originalYPosition = transform.position.y;
    }

    private void Update() {
        CheckHeld();
    }

    private void CheckHeld() {
        if (touch.Hold) {
            currentHoldTime += Time.deltaTime;

            if(currentHoldTime >= holdTime && hasMoved == false) {
                hasMoved = true;
                StopAllCoroutines();
                StartCoroutine(MoveCam(new Vector3(transform.position.x, maxYPosition, transform.position.z)));
                currentHoldTime = 0f;
            }
        } else {
            currentHoldTime = 0f;

            if (hasMoved) {
                hasMoved = false;
                StopAllCoroutines();
                StartCoroutine(MoveCam(new Vector3(transform.position.x, originalYPosition, transform.position.z)));
            }
        }
    }

    private IEnumerator MoveCam(Vector3 position) {
        float t = 0f;
        Vector3 startPostion = transform.position;

        while (t <= moveTime) {
            t += Time.deltaTime;

            transform.position = Vector3.LerpUnclamped(startPostion, position, moveCurve.Evaluate(t / moveTime));

            yield return null;
        }
    }
}
