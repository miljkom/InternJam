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
    public int currentIndex2;
    private int currentIndex3;
    private bool dontCreate = false;
    public List<GameObject> placeList;
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

    private void Start()
    {
        currentIndex1 = Random.Range(0, imageList.Count);
        placeList[0] = Instantiate(imageList[currentIndex1], _transform.GetChild(0).position, Quaternion.identity,
            _transform.GetChild(0));
        currentIndex2 = Random.Range(0, imageList.Count);
        placeList[1] = Instantiate(imageList[currentIndex2], _transform.GetChild(1).position, Quaternion.identity,
            _transform.GetChild(1));
        currentIndex3 = Random.Range(0, imageList.Count);
        placeList[2] = Instantiate(imageList[currentIndex3], _transform.GetChild(2).position, Quaternion.identity,
            _transform.GetChild(2));
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
            drawScreen.GetComponent<DrawDetector>().enabled = true;
            int tmp = currentIndex1;
            currentIndex1 = Random.Range(0, imageList.Count);
            placeList[0] = Instantiate(imageList[currentIndex1], _transform.GetChild(0).position, Quaternion.identity,
                _transform.GetChild(0));
            if (_transform.GetChild(1).childCount == 0)
                dontCreate = true;
            else
                dontCreate = false;
           
            currentIndex3 = currentIndex2;
            currentIndex2 = tmp;
            
            placeList[1] = Instantiate(imageList[currentIndex2], _transform.GetChild(1).position, Quaternion.identity, _transform.GetChild(1));
            
            if (dontCreate && _transform.GetChild(2).childCount > 0)
                Destroy(_transform.GetChild(2).GetChild(0).gameObject);
            else if(!dontCreate && _transform.GetChild(2).childCount == 0)
                placeList[2] = Instantiate(imageList[currentIndex3], _transform.GetChild(2).position, Quaternion.identity, _transform.GetChild(2));

            if (_transform.GetChild(0).childCount > 1)
                Destroy(_transform.GetChild(0).GetChild(0).gameObject);
            if (_transform.GetChild(1).childCount > 1)
                Destroy(_transform.GetChild(1).GetChild(0).gameObject);
            if (_transform.GetChild(2).childCount > 1)
                Destroy(_transform.GetChild(2).GetChild(0).gameObject);

            yield return new WaitForSeconds(secondsToChangeImage);
        }
    }

    public void TntBomb()
    {
        if (currentIndex2 == 4)
        {
            for (int i = 0; i < 5; i++)
            {
                Elements.instance.elements[i] = 0;
            }
        }
    }
}