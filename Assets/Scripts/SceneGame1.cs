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
    public Transform placeHolder;
    
    public TextMeshProUGUI[] textX;
    
    public GameObject endScreen;
    public GameObject startScreen;
    
    public static bool endGame = false;
    public bool endGameScreen = false;
    public bool startGameScreen = false;
    public static bool firstTimeEntering = true;


    private void Awake()
    {
        Time.timeScale = 0;
    }

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
        if (startGameScreen && firstTimeEntering)
        {
            if (Input.anyKey)
            {
                Destroy(startScreen.gameObject);
                Time.timeScale = 1f;
                firstTimeEntering = false;
            }
        }

        if ((Elements.instance.elements[0] == 0) &&
            (Elements.instance.elements[1] == 0) && (Elements.instance.elements[2] == 0) &&
            (Elements.instance.elements[3] == 0))
        {
            for (int i = 0; i < 4; i++)
            {
                textX[i].color = Color.red;
            }
        }
    }

    private void Start()
    {
        if (firstTimeEntering)
        {
            startScreen =
                Instantiate<GameObject>(this.startScreen, Vector2.zero, Quaternion.identity, placeHolder);
            startScreen.transform.localPosition = Vector2.zero;
        }

        StartCoroutine("CheckScoreCourutine");
        for (int i = 0; i < 4; i++)
        {
            textX[i].color = Color.red;
        }
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
        for (int i = 0; i < 4; i++)
        {
            textX[i].text = Elements.instance.elements[i].ToString() + "/3";
            if (Elements.instance.elements[i] > 2)
                textX[i].color = Color.green;
            else if(Elements.instance.elements[i] > 0)
                textX[i].color = Color.white;
        }
        if (Elements.instance.elements[0] > 2 && Elements.instance.elements[1] > 2 && Elements.instance.elements[2] > 2 && Elements.instance.elements[3] > 2)
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
