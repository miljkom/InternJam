using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragElement : MonoBehaviour
{

    public GameObject dragElement;
    public static DragElement instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
