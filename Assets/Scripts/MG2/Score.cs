using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public List<int> score;
    public List<GameObject> scoreObjects;
    private bool counter = false;
    public static Score instance;
    public static int scoreCounterMG2 = 0;
    public Transform placeHolder2;
    public GameObject endScreen;
    public bool endGameScreen = false;
    public bool endGame = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        CheckScore();
        if (counter && endGame)
        {
            EndGame();
            endGame = false;
        }
        if(endGameScreen)
            if (Input.anyKey)
                SceneManager.LoadScene("Scenes/SceneStart",LoadSceneMode.Single);

    }

    public void CheckScore()
    {
        for (int i = 0; i < score.Count; i++)
        {
            scoreObjects[i].transform.GetChild(0).GetComponent<Text>().text = score[i].ToString() + "/1";
            if (score[0] > 0 && score[1] > 0 && score[2] > 0 && score[3] > 0)
                counter = true;
        }
    }
    private void EndGame()
    {
        GameObject endScreen = Instantiate<GameObject>(this.endScreen,
            Vector2.zero, Quaternion.identity, placeHolder2);
        endScreen.transform.localPosition = Vector2.zero;
        scoreCounterMG2++;
        endGameScreen = true;
    }
}
