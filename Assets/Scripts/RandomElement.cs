using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomElement : MonoBehaviour
{
    public float secondsToChangeImage = 4f;
    private int currentIndex1;
    private int currentIndex2;
    private int currentIndex3;
    private bool timeToChange = false;
    public List<GameObject> placeList;
    private Transform _transform;
    public List<GameObject> imageList;


    public static RandomElement instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        _transform = transform;
    }

    private void Start()
    {
        timeToChange = true;
        currentIndex1 = Random.Range(0, imageList.Count);
        placeList[0] = Instantiate(imageList[currentIndex1], _transform.GetChild(0).position, Quaternion.identity,
            _transform.GetChild(0));
        currentIndex2 = Random.Range(0, imageList.Count);
        placeList[1] = Instantiate(imageList[currentIndex2], _transform.GetChild(1).position, Quaternion.identity,
            _transform.GetChild(1));
        currentIndex3 = Random.Range(0, imageList.Count);
        placeList[2] = Instantiate(imageList[currentIndex3], _transform.GetChild(2).position, Quaternion.identity,
            _transform.GetChild(2));
    }

    private void Update()
    {
        if (timeToChange)
        {
            timeToChange = false;
            StartCoroutine("changeImage");
        }
    }

    IEnumerator changeImage()
    {
        if (timeToChange)
        {
            Destroy(_transform.GetChild(0).GetChild(0));
            placeList[0] = Instantiate(imageList[currentIndex2], new Vector3(0, 0, 0), Quaternion.identity,
                _transform.GetChild(0));
            Destroy(_transform.GetChild(0).GetChild(1));
            placeList[0] = Instantiate(imageList[currentIndex3], new Vector3(0, 0, 0), Quaternion.identity,
                _transform.GetChild(1));
            Destroy(_transform.GetChild(0).GetChild(2));
            currentIndex3 = Random.Range(0, imageList.Count);
            placeList[0] = Instantiate(imageList[currentIndex3], new Vector3(0, 0, 0), Quaternion.identity,
                _transform.GetChild(2));
        }

        yield return new WaitForSeconds(secondsToChangeImage);
        timeToChange = true;
    }
}