using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneMover : MonoBehaviour
{


    public GameObject gameManager;
    // speed that it rises.
    [Tooltip("This is the speed that the world rises by.")]
    public float riseSpeed = 2;
    public float slowSpeed = 1;
    public float slowTime = 1;
    public float currentSpeed;
    public bool isSlowing;

    private bool hasSlowed;

    // Use this for initialization
    void Start()
    {
        riseSpeed = GameObject.Find("ShuffleBag").GetComponent<ShuffleBag>().riseSpeed;
        slowSpeed = GameObject.Find("ShuffleBag").GetComponent<ShuffleBag>().slowSpeed;
        slowTime = GameObject.Find("ShuffleBag").GetComponent<ShuffleBag>().slowTime;
        currentSpeed = riseSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.GetComponent<Touch>() != null && gameManager.GetComponent<Touch>().Hold && isSlowing == false && currentSpeed != slowSpeed)
        {
            hasSlowed = true;
            isSlowing = true;
            StopAllCoroutines();
            StartCoroutine(SpeedScale(slowSpeed));
        }
        else if (gameManager.GetComponent<Touch>() != null && gameManager.GetComponent<Touch>().Hold == false)
        {
            if (hasSlowed)
            {
                isSlowing = false;
                hasSlowed = false;
                StopAllCoroutines();
                StartCoroutine(SpeedScale(riseSpeed));
            }
        }

        gameObject.transform.Translate(Vector3.up * Time.deltaTime * currentSpeed, Space.World);
    }

    private IEnumerator SpeedScale(float finalSpeed)
    {
        float t = 0f;

        while (t < slowTime)
        {
            t += Time.deltaTime;

            currentSpeed = Mathf.Lerp(currentSpeed, finalSpeed, t / slowTime);

            yield return null;
        }
        currentSpeed = finalSpeed;
        isSlowing = false;
    }


}
