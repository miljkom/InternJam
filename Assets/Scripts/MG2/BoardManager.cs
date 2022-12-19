using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public List<Element> element;
    public List<Sprite> imageElements;
    public Transform parent;
    public ObjectPooler objectPooler;
    public static bool setMouseImage = false;
    private void Awake()
    {
        objectPooler = ObjectPooler.Instance;
        AddElements();
        Hide3Elements();
    }

    private void Update()
    {
        SpawnAtMousePos();
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
            element[i].cord = new Vector2(l, t);
            element[i].active = true;
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
            int index = Random.Range(0, parent.childCount);
            parent.GetChild(index).GetComponent<Image>().enabled = false;
            element[index].active = false;
        }
    }

    public void SpawnAtMousePos()
    {

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 90.00f;
            if (!setMouseImage)
            {
                Debug.Log("USAO");
                PositionCheck(pos);
                setMouseImage = true;
            }

            DragElement.instance.dragElement.transform.position = pos;
        }
        else
        {
            DragElement.instance.dragElement.SetActive(false);
            setMouseImage = false;
        }
    }

    public void PositionCheck(Vector3 pos)
    {
        float minX = 0;
        float minY = 0;
        float x, y;
        for (int i = 0; i < element.Count; i++)
        {
            x = Math.Abs(pos.x - element[i].transform.position.x);
            y = Math.Abs(pos.y - element[i].transform.position.y);
            if (x <= 74f && y <= 71.5f && !setMouseImage)
            {
                int index = element[i].id;
                DragElement.instance.dragElement.transform.position = pos;
                DragElement.instance.dragElement.GetComponent<Image>().sprite = imageElements[index];
                DragElement.instance.dragElement.SetActive(true);
            }
        }
    }

    public GameObject ActivateObject(int id)
    {
        for (int i = 0; i < objectPooler.pooledObjects.Count; i++)
        {
            if (id == objectPooler.element[i].id)
            {
                if (!objectPooler.element[i].active)
                {
                    objectPooler.element[i].active = true;
                    return objectPooler.pooledObjects[i].gameObject;
                }
            }
        }

        return null;
    }
}