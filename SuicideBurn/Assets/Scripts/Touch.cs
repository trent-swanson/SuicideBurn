using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

	public float deadzone = 125f;
    public float holdTime = 0.1f;
	bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, hold;
	bool isDragging = false;
	Vector2 startTouch, swipeDelta;

	public Vector2 SwipeDelta { get { return swipeDelta; } }
	public bool SwipeLeft { get { return swipeLeft; } }
	public bool SwipeRight { get { return swipeRight; } }
	public bool SwipeUp { get { return swipeUp; } }
	public bool SwipeDown { get { return swipeDown; } }
    public bool Tap { get { return tap; } }
    public bool Hold { get { return hold; } }

    private float currentHoldTime;

    void Update() {
		tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

		//Standalone Inputs
		if (Input.GetMouseButtonDown(0)) {
			tap = true;
			isDragging = true;
			startTouch = Input.mousePosition;
		} else if (Input.GetMouseButtonUp(0)) {
			isDragging = false;
			Reset();
            hold = false;
            currentHoldTime = 0f;
		} else if (Input.GetMouseButton(0)) {
            currentHoldTime += Time.deltaTime;

            if (currentHoldTime >= holdTime)
                hold = true;
        }

		//Mobile Inputs
		if(Input.touches.Length != 0) {
			if (Input.touches[0].phase == TouchPhase.Began) {
				tap = true;
				isDragging = true;
				startTouch = Input.touches[0].position;
			} else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) {
				isDragging = false;
				Reset();
			}
		}

		//Calculate Distance
		swipeDelta = Vector2.zero;
		if (isDragging) {
			if (Input.touches.Length != 0) {
				swipeDelta = Input.touches[0].position - startTouch;
			} else if (Input.GetMouseButton(0)) {
				swipeDelta = (Vector2)Input.mousePosition - startTouch;
			}
		}

        if (hold) {
            Debug.Log("Holding");
            return;
        }

		//Crossed Deadzone?
		if (swipeDelta.magnitude > deadzone) {
			//Direction?
			float x = swipeDelta.x;
			float y = swipeDelta.y;
			if(Mathf.Abs(x) > Mathf.Abs(y)) {
				//Left or Right
				if (x < 0)
					swipeLeft = true;
				else
					swipeRight = true;
			} else {
				// Up or Down
				if (y < 0)
					swipeDown = true;
				else
					swipeUp = true;
			}

			Reset();
        }
	}

	void Reset() {
		startTouch = swipeDelta = Vector2.zero;
		isDragging = false;
	}
}