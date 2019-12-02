using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public GameObject mainObject, targetObject;

    public AnimationCurve tweenCurve;

    public float evaluationRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());

    }

    IEnumerator Countdown()
    {
        Debug.Log("Three");

        yield return new WaitForSeconds(1);

        Debug.Log("Two");

        yield return new WaitForSeconds(1);

        Debug.Log("One");

        yield return new WaitForSeconds(1);

        Debug.Log("Zero");

        StartCoroutine(TweenEase());
    }

    IEnumerator TweenEase()
    {
        Debug.Log("Lets do this");

        yield return null;

        float t = 0f; // t = time, like a counter variable

        Vector3 startPos = mainObject.transform.position;
        Vector3 endPos = targetObject.transform.position;

        while (t < 1f) // as long as t < 100% keep repeating this loop
        {
            mainObject.transform.position = Vector3.LerpUnclamped(startPos, endPos, tweenCurve.Evaluate(t));

            t += (Time.deltaTime * evaluationRate); //evaluate by time and control w rate

            yield return null;
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
