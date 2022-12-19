using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<Image>().sprite == instance.dragElement.GetComponent<Image>().sprite)
            Debug.Log("RADI");
    }
}
