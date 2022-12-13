using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void ButtonClose()
    {
        SceneManager.LoadScene("Scenes/SceneStart",LoadSceneMode.Single);
    }

    public void ButtonRetry()
    {
        RandomElement.instance.Reset();
        SceneManager.LoadScene("Scenes/SceneMG1", LoadSceneMode.Single);
    }

    public void ButtonMG1()
    {
        SceneManager.LoadScene("Scenes/SceneMG1", LoadSceneMode.Single);
    }
    public void ButtonMG2()
    {
        SceneManager.LoadScene("Scenes/SceneMG3", LoadSceneMode.Single);
    }
    public void ButtonMG3()
    {
        SceneManager.LoadScene("Scenes/SceneMG2", LoadSceneMode.Single);
    }
}
