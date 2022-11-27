using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.2f;
    public AnimationCurve animationCurve;
    public static CameraShake Instance;

    void Awake()
    {
        Instance = this;
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = animationCurve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere;
            yield return null;
        }

        transform.position = startPosition;
    }

    public void Shake()
    {
        StartCoroutine(Shaking());
    }
}
