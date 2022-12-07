using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GestureRecognizer;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGame1 : MonoBehaviour
{
    public Transform[] textPlaces;
    public TextMeshProUGUI textA, textB, textC, textD;
    public static bool endGame = false;
    public GameObject endScreen;
    public GameObject startScreen;
    public bool endGameScreen = false;
    public bool startGameScreen = false;
    public Transform placeHolder;
    

    private void Update()
    {
        if (endGame)
        {
            StopCoroutine("CheckScoreCourutine");
            EndGame();
            endGame = false;
        }
        if(endGameScreen)
            if (Input.anyKey)
                SceneManager.LoadScene("Scenes/SceneStart",LoadSceneMode.Single);
        if(startGameScreen)
            if(Input.anyKey)
                Destroy(startScreen.gameObject);
        if ((Elements.instance.elements[0] == 0) &&
            (Elements.instance.elements[1] == 0) && (Elements.instance.elements[2] == 0) &&
            (Elements.instance.elements[3] == 0))
        {
        }
    }

    private void Start()
    {
        startScreen =
            Instantiate<GameObject>(this.startScreen, Vector2.zero, Quaternion.identity, placeHolder);
        startScreen.transform.localPosition = Vector2.zero;
        StartCoroutine("CheckScoreCourutine");
        textA.color = Color.red;
        textB.color = Color.red;
        textC.color = Color.red;
        textD.color = Color.red;
        startGameScreen = true;
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
        if(Elements.instance.elements[0] > 2)
            textA.color = Color.green;
        else if(Elements.instance.elements[0] > 0)
            textA.color = Color.white;
        if(Elements.instance.elements[1] > 2)
            textB.color = Color.green;
        else if(Elements.instance.elements[1] > 0)
            textB.color = Color.white;
        if(Elements.instance.elements[2] > 2)
            textC.color = Color.green;
        else if(Elements.instance.elements[2] > 0)
            textC.color = Color.white;
        if(Elements.instance.elements[3] > 2)
            textD.color = Color.green;
        else if(Elements.instance.elements[3] > 0)
            textD.color = Color.white;
        if ((Elements.instance.elements[0] > 2) &&
            (Elements.instance.elements[1] > 2) && (Elements.instance.elements[2] > 2) &&
            (Elements.instance.elements[3] > 2))
        {
            endGame = true;
        }
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
