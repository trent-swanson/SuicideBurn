using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightBar : MonoBehaviour {
    public Slider heightSlider;
    public float lerpTime;
    public AnimationCurve lerpCurve;

    private ShuffleBag shuffleBag;

    private void Awake() {
        shuffleBag = FindObjectOfType<ShuffleBag>();
    }

    public void AdjustSlider() {
        StopAllCoroutines();
        StartCoroutine(MoveSlider((float)(shuffleBag.chunksCreated - 4) / (shuffleBag.maxChunksBeforeGround - 4)));
    }

    private IEnumerator MoveSlider(float finalPerc) {
        float t = 0f;

        float currentPerc = heightSlider.value;

        while (t < lerpTime) {
            t += Time.deltaTime;
            heightSlider.value = Mathf.Lerp(currentPerc, finalPerc, lerpCurve.Evaluate(t / lerpTime));
            yield return null;
        }
    }
}
