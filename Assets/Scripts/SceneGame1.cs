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
    public Transform[] textPlaces;
    public TextMeshProUGUI textA, textB, textC, textD;
    public static bool endGame = false;
    public GameObject endScreen;
    public bool endGameScreen = false;
    public Transform placeHolder;
    

    private void Update()
    {
        if (endGame)
        {
            StopCoroutine("CheckScoreCourutine");
            EndGame();
        }
        if(endGameScreen)
            if (Input.anyKey)
                SceneManager.LoadScene("Scenes/SceneStart",LoadSceneMode.Single);
    }

    private void Start()
    {
        StartCoroutine("CheckScoreCourutine");
    }

    private void EndGame()
    {
        GameObject endScreen = Instantiate<GameObject>(this.endScreen,
            Vector2.zero, Quaternion.identity, placeHolder);
        endScreen.transform.localPosition = Vector2.zero;
        endGameScreen = true;
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
