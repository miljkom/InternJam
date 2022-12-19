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
        gate = Instantiate(startPrefab, root.position, Quaternion.identity, root);
    }

    private void Start()
    {
        for (int i = 0; i < SceneGame1.scoreCounter*2; i+=2)
        {
            gate.transform.GetChild(2).GetChild(9).GetChild(i).gameObject.SetActive(false);
            gate.transform.GetChild(2).GetChild(9).GetChild(i+1).gameObject.SetActive(true);
        }

        if (SceneGame1.scoreCounter == 3)
        {
            gate.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
            gate.transform.GetChild(2).GetChild(3).gameObject.SetActive(false);
            gate.transform.GetChild(2).GetChild(4).gameObject.SetActive(true);
        }
    }
}
