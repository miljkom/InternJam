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
    private int currentIndex;
    public int guessCounter = 0;
    public List<GesturePattern> gesturePatterns;
    public List<GameObject> prefabGesture;
    public GesturePattern currentGesture;

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
        StartCoroutine("ChangeGesture");
    }

    private void Update()
    {
        if(SceneGame1.endGame)
            StopCoroutine("ChangeGesture");
    }

    IEnumerator ChangeGesture()
    {
        while (true)
        {
            if (_transform.childCount > 0)
                Destroy(_transform.GetChild(0).gameObject);
            currentGesture = randomPattern();

            Debug.Log(currentGesture.id);

            if (currentGesture.id == "up")
                Instantiate(prefabGesture[currentIndex], parentImage.position, Quaternion.identity, parentImage);
            if (currentGesture.id == "w")
                Instantiate(prefabGesture[currentIndex], parentImage.position, Quaternion.identity, parentImage);
            if (currentGesture.id == "Circle")
                Instantiate(prefabGesture[currentIndex], parentImage.position, Quaternion.identity, parentImage);
            if (currentGesture.id == "Square")
                Instantiate(prefabGesture[currentIndex], parentImage.position, Quaternion.identity, parentImage);

            yield return new WaitForSeconds(secondsToChangeGesture);
        }
    }

    public GesturePattern randomPattern()
    {
        currentIndex = Random.Range(0, gesturePatterns.Count);
        return gesturePatterns[currentIndex];
    }
}
