using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragElement : MonoBehaviour
{

    public GameObject dragElement;
    public static DragElement instance;
    public static int[,] boolBoard = new int[5, 5];
    public static List<Vector2> positionBoard = new List<Vector2>();
    public BoardManager boardManager;
    public static int sol = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        boardManager = BoardManager.instance;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        {
            if (col.GetComponent<Image>().sprite == instance.dragElement.GetComponent<Image>().sprite &&
                col.gameObject.GetComponent<Element>().active)
            {
                SetBoolMatrix();
                int ROW = boolBoard.GetLength(0);
                int COL = boolBoard.GetLength(1);
                DFS(boolBoard, (int)col.gameObject.GetComponent<Element>().cord.x,
                    (int)col.gameObject.GetComponent<Element>().cord.y, ROW, COL);
                Debug.Log(sol);
                int dragId = dragElement.GetComponent<Element>().id;
                if (sol >= 5)
                {
                    Debug.Log("MATCH 5");
                    for (int i = 0; i < positionBoard.Count; i++)
                    {
                        int index = (int)positionBoard[i].x * 5 + (int)positionBoard[i].y;
                        boardManager.element[index].active = false;
                        boardManager.element[index].id = 10;
                        boardManager.transform.GetChild(index).GetComponent<Image>().enabled = false;
                        int drag = (int)dragElement.GetComponent<Element>().cord.x * 5 +
                                   (int)dragElement.GetComponent<Element>().cord.y;
                        boardManager.element[drag].active = false;
                        boardManager.element[drag].id = 10;
                        boardManager.transform.GetChild(drag).GetComponent<Image>().enabled = false;
                        dragElement.GetComponent<Element>().id = 10;
                        dragElement.GetComponent<Element>().active = false;
                        dragElement.SetActive(false);
                    }
                    col.gameObject.transform.GetComponent<Image>().enabled = true;
                    col.gameObject.transform.GetComponent<Element>().id += 4;
                    col.gameObject.transform.GetComponent<Element>().active = true;
                    col.gameObject.transform.GetComponent<Image>().sprite = boardManager.spriteElements[dragId];

                }
                else if (sol >= 3)
                {
                    Debug.Log("MATCH 3");
                    for (int i = 0; i < positionBoard.Count; i++)
                    {
                        int index = (int)positionBoard[i].x * 5 + (int)positionBoard[i].y;
                        boardManager.element[index].active = false;
                        boardManager.element[index].id = 10;
                        boardManager.transform.GetChild(index).GetComponent<Image>().enabled = false;
                        int drag = (int)dragElement.GetComponent<Element>().cord.x * 5 +
                                   (int)dragElement.GetComponent<Element>().cord.y;
                        boardManager.element[drag].active = false;
                        boardManager.element[drag].id = 10;
                        boardManager.transform.GetChild(drag).GetComponent<Image>().enabled = false;
                        dragElement.GetComponent<Element>().id = 10;
                        dragElement.GetComponent<Element>().active = false;
                        dragElement.SetActive(false);
                        
                    }
                    col.gameObject.transform.GetComponent<Image>().enabled = true;
                    col.gameObject.transform.GetComponent<Element>().id += 4;
                    col.gameObject.transform.GetComponent<Element>().active = true;
                    col.gameObject.transform.GetComponent<Image>().sprite = boardManager.spriteElements[dragId];
                }
                else
                {
                    int drag = (int)dragElement.GetComponent<Element>().cord.x * 5 +
                               (int)dragElement.GetComponent<Element>().cord.y;
                    boardManager.element[drag].active = true;
                    Debug.Log(drag);
                }
            }
            sol = 1;
            positionBoard = new List<Vector2>();
        }
    }

    public void SetBoolMatrix()
    {
        int k = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (dragElement.GetComponent<Element>().id == boardManager.board[i, j])
                {
                    boolBoard[i, j] = 1;
                }
                else
                    boolBoard[i, j] = 0;
            }
        }
    }

    private static void DFS(int[,] Matrix, int i, int j, int ROW,
        int COL)
    {
        if (i < 0 || j < 0 || i > (ROW - 1) || j > (COL - 1)
            || Matrix[i, j] != 1)
        {
            return;
        }

        if (Matrix[i, j] == 1)
        {
            Matrix[i, j] = 0;
            positionBoard.Add(new Vector2(i,j));
            sol++;
            Debug.Log(sol);
            DFS(Matrix, i + 1, j, ROW,
                COL); // right side traversal
            DFS(Matrix, i - 1, j, ROW,
                COL); // left side traversal
            DFS(Matrix, i, j + 1, ROW,
                COL); // upward side traversal
            DFS(Matrix, i, j - 1, ROW,
                COL); // downward side traversal
        }

    }

}