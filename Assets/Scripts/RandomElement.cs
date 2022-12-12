using System;
using System.Collections;
using System.Collections.Generic;
using GestureRecognizer;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomElement : MonoBehaviour
{
    public float secondsToChangeImage = 4f;
    private int currentIndex1;
    private int currentIndex2;
    public int currentIndex3;
    
    public static bool dontCreate = false;
    public static bool noCurrentElement = false;
    
    public List<GameObject> placeList;
    public List<GameObject> pList;
    private Transform _transform;
    public List<GameObject> imageList;
    public GameObject drawScreen;

    public static RandomElement instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        _transform = transform;
    }

    public void Reset()
    {
        secondsToChangeImage = 4f;
        dontCreate = false;
        noCurrentElement = false;
        GestureHandler.isGuessed = false;
    }

    private void Start()
    {
        currentIndex1 = Random.Range(0, imageList.Count);
        pList[0] = Instantiate(imageList[currentIndex1], _transform.GetChild(0).position, Quaternion.identity,
            _transform.GetChild(0).GetChild(0));
        currentIndex2 = Random.Range(0, imageList.Count);
        pList[1] = Instantiate(imageList[currentIndex2], _transform.GetChild(1).position, Quaternion.identity,
            _transform.GetChild(1).GetChild(0));
        currentIndex3 = Random.Range(0, imageList.Count);
        pList[2] = Instantiate(imageList[currentIndex3], _transform.GetChild(2).position, Quaternion.identity,
            _transform.GetChild(2).GetChild(0));
        int currentIndex4 = Random.Range(0, imageList.Count);
        pList[3] = Instantiate(imageList[currentIndex4], _transform.GetChild(3).position, Quaternion.identity,
            _transform.GetChild(3).GetChild(0));
        StartCoroutine("ChangeImage");
    }
    private void Update()
    {
        if(SceneGame1.endGame)
            StopCoroutine("ChangeImage");
    }

    IEnumerator ChangeImage()
    {
        while (true)
        {
            float timeElapsed = 0f;
            while (timeElapsed < secondsToChangeImage)
            {
                Vector3 startPoint = new Vector3(placeList[0].transform.position.x,
                    placeList[0].transform.position.y);
                Vector3 endPoint = new Vector3(placeList[1].transform.position.x,
                    placeList[1].transform.position.y);
                
                pList[0].transform.position = Vector3.Lerp(startPoint,endPoint, timeElapsed / secondsToChangeImage);

                startPoint.x = placeList[1].transform.position.x;
                endPoint.x = placeList[2].transform.position.x;
                
                pList[1].transform.position = Vector3.Lerp(startPoint, endPoint, timeElapsed / secondsToChangeImage);
                
                if (!GestureHandler.isGuessed)
                {
                    startPoint.x = placeList[2].transform.position.x;
                    endPoint.x = placeList[3].transform.position.x;
                    
                    pList[2].SetActive(true);
                    pList[2].transform.position = Vector3.Lerp(startPoint,endPoint, timeElapsed / secondsToChangeImage);
                }
                else
                    pList[2].SetActive(false);

                startPoint.x = placeList[3].transform.position.x;
                endPoint.x = placeList[4].transform.position.x;
                
                pList[3].transform.position = Vector3.Lerp(startPoint,endPoint, timeElapsed / secondsToChangeImage);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
           
            int tmp = currentIndex1;
            currentIndex3 = currentIndex2;
            currentIndex2 = tmp;
            currentIndex1 = Random.Range(0, imageList.Count);
            
            while(Elements.instance.elements[currentIndex1] >= 3)
                currentIndex1 = Random.Range(0, imageList.Count);
            
            
            pList[4] = pList[3];
            pList[3].transform.SetParent(placeList[4].transform.GetChild(0));
            pList[3] = pList[2];
            pList[2].transform.SetParent(placeList[3].transform.GetChild(0));
            pList[2] = pList[1];
            pList[1].transform.SetParent(placeList[2].transform.GetChild(0));
            pList[1] = pList[0];
            pList[0].transform.SetParent(placeList[1].transform.GetChild(0));
            
            if(pList[4].transform.parent.childCount > 0)
                Destroy(pList[4].transform.gameObject);
            
            pList[0] = Instantiate(imageList[currentIndex1], _transform.GetChild(0).position, Quaternion.identity,
                _transform.GetChild(0).GetChild(0));
            GestureHandler.isGuessed = false;
            noCurrentElement = false;
            yield return null;
        }
    }

    public void TntBomb()
    {
        if (currentIndex3 == 4)
        {
            for (int i = 0; i < 5; i++)
            {
                Elements.instance.elements[i] = 0;
            }
        }
    }
}