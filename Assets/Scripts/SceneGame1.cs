using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GestureRecognizer;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneGame1 : MonoBehaviour
{
    public Transform[] textPlaces;
    public Transform placeHolder;
    public Transform placeHolder2;
    
    public TextMeshProUGUI[] textX;
    
    public GameObject endScreen;
    public GameObject startScreen;
    public GameObject[] checkmark;
    public GameObject glowFrame;
    
    public static bool endGame = false;
    public bool endGameScreen = false;
    public bool startGameScreen = false;
    public static bool firstTimeEntering = true;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (endGame)
        {
            StopCoroutine("CheckScoreCourutine");
            RandomElement.instance.StopCoroutine("ChangeImage");
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

        if (!RandomElement.dontCreate)
        {
            glowFrame.SetActive(false);
        }
        else
        {
            glowFrame.SetActive(true);
        }
        
        if ((Elements.instance.elements[0] == 0) &&
            (Elements.instance.elements[1] == 0) && (Elements.instance.elements[2] == 0) &&
            (Elements.instance.elements[3] == 0))
        {
            for (int i = 0; i < 4; i++)
            {
                textX[i].color = Color.white;
            }
        }
    }

    private void Start()
    {
        if (firstTimeEntering)
        {
            startScreen =
                Instantiate<GameObject>(this.startScreen, Vector2.zero, Quaternion.identity, placeHolder2);
            startScreen.transform.localPosition = Vector2.zero;
        }
        else
        {
            Time.timeScale = 1f;
            RandomElement.instance.Reset();
            for (int i = 0; i < 4; i++)
            {
                Elements.instance.elements[i] = 0;
            }
        }
        StartCoroutine("CheckScoreCourutine");
        startGameScreen = true;

        for (int i = 0; i < 4; i++)
        {
            checkmark[i] = Instantiate(checkmark[i], textPlaces[i].transform.position, Quaternion.identity, textPlaces[i].transform);
            checkmark[i].SetActive(false);
        }
        
    }

    private void EndGame()
    {
        GameObject endScreen = Instantiate<GameObject>(this.endScreen,
            Vector2.zero, Quaternion.identity, placeHolder2);
        endScreen.transform.localPosition = Vector2.zero;
        endGameScreen = true;
    }

    public void CheckScore()
    {
        for (int i = 0; i < 4; i++)
        {
            textX[i].text = Elements.instance.elements[i].ToString() + "/3";
            if (Elements.instance.elements[i] > 2)
            {
                textX[i].alpha = 0f;
                checkmark[i].SetActive(true);
            }
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
