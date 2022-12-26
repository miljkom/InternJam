using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject settings;

    public void ButtonClose()
    {
        SceneManager.LoadScene("Scenes/SceneStart",LoadSceneMode.Single);
    }
    
    public void ButtonRetry()
    {
        RandomElement.instance.Reset();
        SceneManager.LoadScene("Scenes/SceneMG1", LoadSceneMode.Single);
    }

    public void ButtonContinue()
    {
        Destroy(BoardManager.instance.parentPopup.GetChild(1).gameObject);
    }
    public void ButtonMG1()
    {
        SceneManager.LoadScene("Scenes/SceneMG1", LoadSceneMode.Single);
    }
    public void ButtonMG2()
    {
        SceneManager.LoadScene("Scenes/SceneMG2", LoadSceneMode.Single);
    }
    public void ButtonMG3()
    {
        SceneManager.LoadScene("Scenes/SceneMG3", LoadSceneMode.Single);
    }

    public void ButtonSettings()
    {
        settings = Instantiate(BoardManager.instance.settingsPrefab, Vector3.zero, Quaternion.identity, BoardManager.instance.parentPopup);
        settings.transform.localPosition = Vector2.zero;
    }
}
