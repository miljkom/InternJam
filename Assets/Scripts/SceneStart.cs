using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneStart : MonoBehaviour
{
    public GameObject startPrefab;
    public GameObject gate;
    public Transform root;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        gate = Instantiate(startPrefab, root.position, Quaternion.identity, root);
    }

    private void Start()
    {
        int indexMG1 = SceneGame1.scoreCounter * 2;
        int indexMG2 = Score.scoreCounterMG2 * 2;
        for (int i = 0; i < indexMG1; i+=2)
        {
            gate.transform.GetChild(0).GetChild(3).GetChild(i).gameObject.SetActive(false);
            gate.transform.GetChild(0).GetChild(3).GetChild(i+1).gameObject.SetActive(true);
        }

        if (SceneGame1.scoreCounter == 3)
        {
            gate.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            gate.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            gate.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        }
        for (int i = 0; i < indexMG2; i+=2)
        {
            gate.transform.GetChild(2).GetChild(6).GetChild(i).gameObject.SetActive(false);
            gate.transform.GetChild(2).GetChild(6).GetChild(i+1).gameObject.SetActive(true);
        }
        if (Score.scoreCounterMG2 == 3)
        {
            gate.transform.GetChild(2).GetChild(6).gameObject.SetActive(false);
            gate.transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
        }
    }
}
