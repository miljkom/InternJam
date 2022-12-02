using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneStart : MonoBehaviour
{
    public Button startButtonMG1;
    public Button startButtonMG2;
    public Button startButtonMG3;

    private void Awake()
    {
        startButtonMG1.onClick.AddListener(() => SceneManager.LoadScene("SceneMG1"));
    }
    
        
}
