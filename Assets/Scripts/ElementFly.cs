using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFly : MonoBehaviour
{
    public Vector2 positionToGo;
    private float _currentScale = InitScale;
    private const float TargetScale = 1.5f;
    private const float InitScale = 1f;
    private const int FramesCount = 60;
    private const float AnimationTimeSeconds = 0.5f;
    private float _deltaTime = AnimationTimeSeconds/FramesCount;
    private float _dx = (TargetScale - InitScale)/FramesCount;
    private bool _upScale = true;
    private void Start()
    {
        StartCoroutine(Fly());
    }

    IEnumerator Fly()
    {
        while (_upScale)
        {
            _currentScale += _dx;
            if (_currentScale > TargetScale)
            {
                _upScale = false;
                _currentScale = TargetScale;
            }
            transform.localScale = Vector3.one * _currentScale;
            yield return new WaitForSeconds(_deltaTime);
        }
        float timeElapsed = 0f;
        while (timeElapsed < 2f)
        {
            transform.position = Vector3.Lerp(transform.position, positionToGo,
                timeElapsed / 2f);
            timeElapsed += Time.deltaTime;
            yield return null;

        }
    }
}