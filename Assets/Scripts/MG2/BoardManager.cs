using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public List<Element> element;
    public List<Sprite> imageElements;
    public Transform parent;
    private void Awake()
    {
        AddElements();
        Hide3Elements();
    }

    public void AddElements()
    {
        int l = 0;
        int t = 0;
        for (int i = 0; i < parent.childCount; i++)
        {
            int index = Random.Range(0, imageElements.Count);
            parent.GetChild(i).GetComponent<Image>().sprite = imageElements[index];
            element[i].id = index;
            element[i].x = l;
            element[i].y = t;
            t++;
            if (t == 5)
            {
                t = 0;
                l++;
            }
        }
    }

    public void Hide3Elements()
    {
        for (int i = 0; i < 3; i++)
        {
            parent.GetChild(Random.Range(0, parent.childCount)).GetComponent<Image>().enabled = false;
        }
    }
}