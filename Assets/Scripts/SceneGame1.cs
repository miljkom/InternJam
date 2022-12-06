using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GestureRecognizer;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGame1 : MonoBehaviour
{
    public TextMeshProUGUI textA, textB, textC, textD;
    public static bool endGame = false;

    private void Update()
    {
        if(!endGame)
            StartCoroutine("CheckScoreCourutine");
        else
        {
            StopCoroutine("CheckScoreCourutine");
            SceneManager.LoadScene("Scenes/SceneStart", LoadSceneMode.Single);
        }
    }

    public void CheckScore()
    {
        textA.text = Elements.instance.elements[0].ToString() + "/3";
        textB.text = Elements.instance.elements[1].ToString() + "/3";
        textC.text = Elements.instance.elements[2].ToString() + "/3";
        textD.text = Elements.instance.elements[3].ToString() + "/3";
        if ((Elements.instance.elements[0] > 2) &&
            (Elements.instance.elements[1] > 2) && (Elements.instance.elements[2] > 2) &&
            (Elements.instance.elements[3] > 2))
            endGame = true;
    }

    IEnumerator CheckScoreCourutine()
    {
        while (true)
        {
            CheckScore();
            yield return new WaitForSeconds(.5f);
        }

    }
}
