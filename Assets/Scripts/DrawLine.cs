using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;
    
    [SerializeField] private LineRenderer lr;
    [SerializeField] private List<Vector2> fingerPositions;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            CreateLine();
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.2f)
                UpdateLine(tempFingerPos);
        }
            
    }

    public void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lr = currentLine.GetComponent<LineRenderer>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lr.SetPosition(0, fingerPositions[0]);
        lr.SetPosition(1, fingerPositions[1]);
    }

    public void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1, newFingerPos);
    }
}
