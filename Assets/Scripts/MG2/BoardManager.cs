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
    public int[,] board = new int[5, 5];
    public static bool setMouseImage = false;
    public static BoardManager instance;
    public List<Sprite> imageTier2Elements;
    public List<Sprite> imageTier3Elements;
    public Transform positionToGo;
    public Camera camera;
    public static int oldIndex;
    public GameObject settingsPrefab;
    public Transform parentPopup;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }
        AddElements();
        Hide3Elements();
        SetMatrix();
    }

    private void Update()
    {

        SpawnAtMousePos(); 
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.one, 10f);

        if (Input.GetMouseButton(0))
        {
            if (hit.collider != null)
            {
                SetMatrix();
                DragElement.instance.AnimationCheck(hit.collider);
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            SetMatrix();
            if(DragElement.entered && hit.collider != null)
                DragElement.instance.CheckMatching(hit.collider);
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
            if (element[i].id >= 10)
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
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.one, 10f);
      
        if (!setMouseImage && hit.collider != null && hit.collider.gameObject.GetComponent<Image>().enabled)
        {
        int moveIndex = (int)hit.collider.gameObject.GetComponent<Element>().cord.x * 5 + (int)hit.collider.gameObject.GetComponent<Element>().cord.y;
            if (element[moveIndex].id < 4 && element[moveIndex].active)
            {
                int index = element[moveIndex].id;
                oldIndex = index;
                DragElement.instance.dragElement.transform.position = pos;
                DragElement.instance.dragElement.GetComponent<Image>().sprite = imageElements[index];
                DragElement.instance.dragElement.GetComponent<Element>().id = index;
                DragElement.instance.dragElement.GetComponent<Element>().active = true;
                DragElement.instance.dragElement.GetComponent<Element>().cord = element[moveIndex].cord;
                DragElement.instance.dragElement.SetActive(true);
                element[moveIndex].active = false;
                element[moveIndex].id = 10;
                element[moveIndex].GetComponent<Image>().enabled = false;
            }
            else if (element[moveIndex].id >= 4 && element[moveIndex].id < 10 && element[moveIndex].active)
            {
                int index = element[moveIndex].id;
                oldIndex = index;
                DragElement.instance.dragElement.transform.position = pos;
                DragElement.instance.dragElement.GetComponent<Image>().sprite = imageTier2Elements[index - 4];
                DragElement.instance.dragElement.GetComponent<Element>().id = index;
                DragElement.instance.dragElement.GetComponent<Element>().active = true;
                DragElement.instance.dragElement.GetComponent<Element>().cord = element[moveIndex].cord;
                DragElement.instance.dragElement.SetActive(true);
                element[moveIndex].active = false;
                element[moveIndex].id = 10;
                element[moveIndex].GetComponent<Image>().enabled = false;
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

}