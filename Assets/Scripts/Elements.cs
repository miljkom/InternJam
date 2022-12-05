using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements : MonoBehaviour
{

    public List<int> elements;

    public static Elements instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    
}
