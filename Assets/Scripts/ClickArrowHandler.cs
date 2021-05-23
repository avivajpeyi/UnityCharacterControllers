using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickArrowHandler : MonoBehaviour
{
    public float maxScale = 2.5f;
    public float flashDuration = 0.15f;
    public float chargeDuration = 3.0f;
    public bool testing = false;
    public Vector3 target;

    public bool reachedMaxScale = false;
    private Tweener colorTween;
    private Coroutine scaleLerp;
    private Camera cam;
    private Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        myRenderer = GetComponentInChildren<Renderer>();
        colorTween = myRenderer.material.DOColor(Color.yellow, flashDuration)
            .SetLoops(-1, LoopType.Yoyo);
        Reset();
    }


    public void UpdateScale(float scaleVal)
    {
        Vector3 scale = transform.localScale;
        scale.Set(scale.x, scale.y, scaleVal);
        transform.localScale = scale;
    }


    IEnumerator LengthLerp(float lerpDuration)
    {
        float timeElapsed = 0;
        float startValue = transform.localScale.z;
        while (timeElapsed < lerpDuration)
        {
            float t = timeElapsed / lerpDuration;
            t = t * t * (3f - 2f * t);

            float valueToLerp = Mathf.Lerp(startValue, maxScale, t);
            UpdateScale(valueToLerp);


            timeElapsed += Time.deltaTime;

            yield return null;
        }

        UpdateScale(maxScale);
    }


    void GraduallyIncreaseScale()
    {
        reachedMaxScale = transform.localScale.z >= maxScale;
        if (!reachedMaxScale)
        {
            if (scaleLerp == null)
                scaleLerp = StartCoroutine(LengthLerp(chargeDuration));
        }
        else
        {
            FlickerColor();
            StopCoroutine(scaleLerp);
        }
    }

    void FlickerColor()
    {
        if (!colorTween.IsPlaying())
            colorTween.Play();
    }


    void UpdateDirection(Vector3 targetLocation)
    {
        Vector3 relativePos = targetLocation - transform.position;
        Quaternion myRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        myRotation.x = 0;
        myRotation.z = 0;
        transform.rotation = myRotation;
        transform.position = transform.position;
    }

    private void Reset()
    {
        UpdateScale(0.0f);
        colorTween.Pause();
        myRenderer.material.color = Color.white;
        if (scaleLerp != null)
            StopCoroutine(scaleLerp);
        scaleLerp = null;
    }


    private Vector3 GetTestingTarget()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1;
        Vector3 mouseWorldLoc = cam.ScreenToWorldPoint(mousePos);
        return mouseWorldLoc;
    }

    // Update is called once per frame
    void Update()
    {
        if (testing)
        {
            target = GetTestingTarget();
        }

        if (Input.GetMouseButton(0))
        {
            GraduallyIncreaseScale();
            if (!target.Equals(Vector3.zero))
                UpdateDirection(target);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
    }
}