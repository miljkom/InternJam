using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneStart : MonoBehaviour
{
    public Button startButtonMG1;

    private void Awake()
    {
        startButtonMG1.onClick.SetListener(() => SceneManager.LoadScene("Scenes/SceneMG1", LoadSceneMode.Single));
    }
    
}
