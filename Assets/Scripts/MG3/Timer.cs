using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;


    private void Start()
    {
        StartCoroutine("Tick");
    }
    private void Update()
    {
        if(timerSlider.value==0) 
        {
            MiniGameManager.Instance.Lose();
        }
    }
    private IEnumerator Tick()
    {
        while (timerSlider.value >= 0)
        {
            yield return new WaitForSeconds(1);
            timerSlider.value -= 1;
        }
    }
}