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
    public float secondsToChangeGesture = 4f;
    public List<GesturePattern> gesturePatterns;
    public GesturePattern currentGesture;
    private int currentIndex;
    [HideInInspector]
    public bool timeToChange = true;
    public List<GameObject> prefabGesture;
    [SerializeField] public Transform parentImage;
    public static RandomGesture instance;
    private Transform _transform;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        _transform = transform;
        timeToChange = true;
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
            Instantiate(prefabGesture[currentIndex], parentImage.position, Quaternion.identity,parentImage);
        if (currentGesture.id == "w")
            Instantiate(prefabGesture[currentIndex], parentImage.position, Quaternion.identity,parentImage);
        if (currentGesture.id == "Circle")
            Instantiate(prefabGesture[currentIndex], parentImage.position, Quaternion.identity,parentImage);
        if (currentGesture.id == "Square")
            Instantiate(prefabGesture[currentIndex], parentImage.position, Quaternion.identity,parentImage);

        yield return new WaitForSeconds(secondsToChangeGesture);
        timeToChange = true;
    }

    public GesturePattern randomPattern()
    {
        currentIndex = Random.Range(0, gesturePatterns.Count);
        return gesturePatterns[currentIndex];
    }
}
