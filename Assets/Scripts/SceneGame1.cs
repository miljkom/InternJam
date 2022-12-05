using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GestureRecognizer;
using TMPro;
using UnityEngine;

public class SceneGame1 : MonoBehaviour
{
    public TextMeshProUGUI textA, textB, textC, textD;


    private void Start()
    {
        StartCoroutine(CheckScoreCourutine());
    }

    public void CheckScore()
    {
        textA.text = Elements.instance.elements[0].ToString() + "/3";
        textB.text = Elements.instance.elements[1].ToString() + "/3";
        textC.text = Elements.instance.elements[2].ToString() + "/3";
        textD.text = Elements.instance.elements[3].ToString() + "/3";
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
