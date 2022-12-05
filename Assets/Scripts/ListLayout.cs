using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLayout : MonoBehaviour
{
    public List<GameObject> listLayout;
    public static ListLayout instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        for (int i = 0; i < listLayout.Count; i++)
            listLayout[i] = gameObject.transform.GetChild(i).gameObject;
    }
}
