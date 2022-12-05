using System;
using System.Collections;
using System.Collections.Generic;
using GestureRecognizer;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomGesture : MonoBehaviour
{
    public float secondsToChangeGesture = 2f;
    public List<GesturePattern> gesturePatterns;
    public GesturePattern currentGesture;
    private int currentIndex;
    private bool timeToChange = false;
    public List<GameObject> prefabGesture;
    [SerializeField] public Transform parentImage;
    public static RandomGesture instance;
    private Transform _transform;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        _transform = transform;
    }

    private void Start()
    {
        timeToChange = true;
        currentGesture = randomPattern();
    }

    private void Update()
    {
        if(timeToChange)
        {
            timeToChange = false;
            StartCoroutine("ChangeGesture");
        }
    }

    IEnumerator ChangeGesture()
    {
        if(_transform.childCount > 0)
            Destroy(_transform.GetChild(0).gameObject);
        currentGesture = randomPattern();
        Debug.Log(currentGesture.id);
        if (currentGesture.id == "up")
            Instantiate(prefabGesture[currentIndex], new Vector3(550, 1496, 0), Quaternion.identity,parentImage);
        if (currentGesture.id == "w")
            Instantiate(prefabGesture[currentIndex], new Vector3(550, 1496, 0), Quaternion.identity,parentImage);
        if (currentGesture.id == "Circle")
            Instantiate(prefabGesture[currentIndex], new Vector3(550, 1496, 0), Quaternion.identity,parentImage);
        if (currentGesture.id == "Square")
            Instantiate(prefabGesture[currentIndex], new Vector3(550, 1496, 0), Quaternion.identity,parentImage);

        yield return new WaitForSeconds(secondsToChangeGesture);
        timeToChange = true;
        /*Destroy(gameObject.transform);*/
    }

    public GesturePattern randomPattern()
    {
        currentIndex = Random.Range(0, gesturePatterns.Count);
        return gesturePatterns[currentIndex];
    }
}
