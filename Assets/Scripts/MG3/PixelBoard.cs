using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PixelBoard : MonoBehaviour
{
    private const float originalTime = 1f / 6f;
    private float timeToDepixelateOne = originalTime;
    public Box[] matrix;
    private int i = 0;
    private const int j = 12;
    private bool centralBlockImmunityDisabled = false;


    private int ReturnRandomElement()
    {
        int value = Random.Range(0, 36);
        return value;
    }
    public void DepixelateOnePixel(Box go)
    {
        if (!go.isImmune)
            StartCoroutine(go.FadeAndDie());
    }

    public IEnumerator DepixelateAll()
    {
        while (i <= matrix.Length)
        {
            yield return new WaitForSeconds(timeToDepixelateOne);

            DepixelateOnePixel(matrix[ReturnRandomElement()]);
            i++;
            timeToDepixelateOne *= 0.995f;
            if (i > matrix.Length) i = 0;
            //           Debug.Log("ovo je u korutini "+i);
        }
    }

    private void SetCentralBlockImmunity(bool value)
    {
        // 2x2
        matrix[14].isImmune = value;
        matrix[15].isImmune = value;
        matrix[20].isImmune = value;
        matrix[21].isImmune = value;
        //4x4
        matrix[7].isImmune = value;
        matrix[8].isImmune = value;
        matrix[9].isImmune = value;
        matrix[10].isImmune = value;
        matrix[13].isImmune = value;
        matrix[16].isImmune = value;
        matrix[19].isImmune = value;
        matrix[22].isImmune = value;
        matrix[25].isImmune = value;
        matrix[26].isImmune = value;
        matrix[27].isImmune = value;
        matrix[28].isImmune = value;
        Debug.Log("sad su unistivi");
        if (!value) centralBlockImmunityDisabled = true;
    }

    private void Start()
    {
        StartCoroutine(DepixelateAll());
        StartCoroutine(DestroySelf());
    }

    private void Update()
    {
        if (i == j && !centralBlockImmunityDisabled)
        {
            SetCentralBlockImmunity(false);
        }
    }

    private void Awake()
    {
        SetCentralBlockImmunity(true);
    }

    private IEnumerator DestroySelf() 
    {
        yield return new WaitForSeconds(12f);
        Destroy(gameObject);
    }
}
