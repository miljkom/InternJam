using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementMerge : MonoBehaviour
{
    public Vector2 positionToGo;
    private float _currentScale = InitScale;
    private const float TargetScale = 1.5f;
    private const float InitScale = 1f;
    private const float AnimationTimeSeconds = 0.01f;
    private bool _upScale = true;
    
    private void Start()
    {
        StartCoroutine(Fly());
    }

    IEnumerator Fly()
    {
        while (_upScale)
        {
            _currentScale += 0.05f;
            if (_currentScale > TargetScale)
            {
                _upScale = false;
                _currentScale = TargetScale;
            }

            transform.localScale = Vector3.one * _currentScale;
            yield return new WaitForSeconds(AnimationTimeSeconds);
        }

        float timeElapsed = 0f;
        while (timeElapsed < 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, positionToGo,
                timeElapsed / 0.5f);
            timeElapsed += Time.deltaTime;
            if((Vector2)transform.position == positionToGo)
                gameObject.SetActive(false);
            Debug.Log(timeElapsed);
            yield return null;
        }
    }
}
