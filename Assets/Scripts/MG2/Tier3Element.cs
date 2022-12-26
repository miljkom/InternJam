using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier3Element : MonoBehaviour
{
    public Element element;
    public static Tier3Element instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        gameObject.AddComponent<Element>();
        element.id = 0;
        element.active = false;
        element.cord = Vector2.zero;
    }
}
