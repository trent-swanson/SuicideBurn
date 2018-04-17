using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float holdTime;
    public float minYShift;
    public float breakTime;
    public float accelerateTime;
    public AnimationCurve breakCurve;
    public AnimationCurve accelerateCurve;

    private Touch touch;
    private float currentHoldTime;

    private bool isMoving;
    private bool hasMoved;

    private float originalYPos;
    private float minYPos;

    private void Start() {
        touch = GameObject.FindGameObjectWithTag("GameController").GetComponent<Touch>();
        originalYPos = transform.position.y;
        minYPos = transform.position.y + minYShift;
        Debug.Log(originalYPos);
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
                StartCoroutine(MoveCam(new Vector3(transform.position.x, minYPos, transform.position.z), breakTime, breakCurve));
                currentHoldTime = 0f;
            }
        } else {
            currentHoldTime = 0f;

            if (hasMoved) {
                hasMoved = false;
                StopAllCoroutines();
                StartCoroutine(MoveCam(new Vector3(transform.position.x, originalYPos, transform.position.z), accelerateTime, accelerateCurve));
            }
        }
    }

    private IEnumerator MoveCam(Vector3 position, float lerpTIme, AnimationCurve curve) {
        float t = 0f;
        Vector3 startPostion = transform.position;

        while (t <= lerpTIme) {
            t += Time.deltaTime;

            transform.position = Vector3.LerpUnclamped(startPostion, position, curve.Evaluate(t / breakTime));

            yield return null;
        }
    }
}
