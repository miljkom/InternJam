using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool isImmune = false;
    
    public IEnumerator FadeAndDie()
    {
        
        float journey = 0f;
        Vector3 originScale = transform.localScale;
        Color32 finalColor = new(255, 255, 255, 0);

        while (journey <= 1)
        {
            journey += Time.deltaTime;
            float percent = Mathf.Clamp01(journey / 1f);

            transform.localScale = Vector3.Lerp(originScale, originScale*1.5f, percent);
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, finalColor, percent);

            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopCoroutine(FadeAndDie());
    }
}
