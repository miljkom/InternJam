using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<GameObject> pooledObjects;
    public List<Element> element;
    public List<Sprite> imageElements;
    public GameObject objectToPool;
    public Transform parent;
    public int amountToPool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        
        int index = 0;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool,parent);
            element[i].active = false;
            element[i].cord = Vector2.zero;
            element[i].id = index;
            tmp.GetComponent<Image>().sprite = imageElements[index];
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
            if (i % 5 == 0 && i >= 5)
                index++;
        }
    }
}
