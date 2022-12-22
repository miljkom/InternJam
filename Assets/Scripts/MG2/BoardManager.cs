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
    public int[,] board = new int[5, 5];
    public static bool setMouseImage = false;
    public static BoardManager instance;
    public List<Sprite> spriteElements;
    public Camera camera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        objectPooler = ObjectPooler.Instance;
        AddElements();
        Hide3Elements();
        SetMatrix();
    }

    private void Update()
    {
        SpawnAtMousePos();
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.one, 10f);
            if(DragElement.entered)
                DragElement.instance.CheckMatching(hit.collider);
            if (DragElement.matched)
            {
                DragElement.instance.Swap(hit.collider);
                DragElement.matched = false;
            }
        }
    }
    

    public void AddElements()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            int index = Random.Range(0, imageElements.Count);
            parent.GetChild(i).GetComponent<Image>().sprite = imageElements[index];
            element[i].id = index;
            element[i].active = true;
        }
    }

    public void Add5Elements()
    {
        int counter = 0;
        int sizeOfBoard = 25;
        for (int i = 0; i < sizeOfBoard; i++)
        {
            int index = Random.Range(0, imageElements.Count);
            if (element[i].id == 10)
            {
                parent.GetChild(i).GetComponent<Image>().enabled = true;
                parent.GetChild(i).GetComponent<Image>().sprite = imageElements[index];
                element[i].id = index;
                element[i].active = true;
                counter++;
                if (counter == 5)
                    break;
            }
        }
        SetMatrix();
    }

    public void Hide3Elements()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, parent.childCount);
            parent.GetChild(index).GetComponent<Image>().enabled = false;
            element[index].active = false;
            element[index].id = 10;
        }
    }

    public void SpawnAtMousePos()
    {

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;
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
            float closestX = element[i].GetComponent<BoxCollider2D>().size.x / 2;
            float closestY = element[i].GetComponent<BoxCollider2D>().size.y / 2;
            if (x <= closestX && y <= closestY && !setMouseImage && element[i].active)
            {
                if (element[i].id < 4)
                {
                    int index = element[i].id;
                    DragElement.instance.dragElement.transform.position = pos;
                    DragElement.instance.dragElement.GetComponent<Image>().sprite = imageElements[index];
                    DragElement.instance.dragElement.GetComponent<Element>().id = index;
                    DragElement.instance.dragElement.GetComponent<Element>().active = true;
                    DragElement.instance.dragElement.GetComponent<Element>().cord = element[i].cord;
                    DragElement.instance.dragElement.SetActive(true);
                    element[i].active = false;
                    element[i].GetComponent<Image>().enabled = false;
                }
                else if (element[i].id >= 4)
                {
                    int index = element[i].id;
                    DragElement.instance.dragElement.transform.position = pos;
                    DragElement.instance.dragElement.GetComponent<Image>().sprite = spriteElements[index - 4];
                    DragElement.instance.dragElement.GetComponent<Element>().id = index;
                    DragElement.instance.dragElement.GetComponent<Element>().active = true;
                    DragElement.instance.dragElement.GetComponent<Element>().cord = element[i].cord;
                    DragElement.instance.dragElement.SetActive(true);
                    element[i].active = false;
                    element[i].GetComponent<Image>().enabled = false;
                }
            }
        }
    }
    public void SetMatrix()
    {
        int k = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                board[i, j] = element[k].id;
                element[k].cord = new Vector2(i, j);
                k++;
            }
        }
    }

    /*public GameObject ActivateObject(int id)
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
    }*/
    
}