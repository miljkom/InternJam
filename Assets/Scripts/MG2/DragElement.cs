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
    public static bool entered = false;
    public static bool matched = false;

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
        if (col.GetComponent<Image>().sprite == instance.dragElement.GetComponent<Image>().sprite &&
            col.gameObject.GetComponent<Element>().active)
        {
            Debug.Log("USAP");
            SetBoolMatrix();
            entered = true;
        }
        else
            matched = true;
    }
    public void CheckMatching(Collider2D col)
    {
        int ROW = boolBoard.GetLength(0);
        int COL = boolBoard.GetLength(1);
        DFS(boolBoard, (int)col.gameObject.GetComponent<Element>().cord.x,
            (int)col.gameObject.GetComponent<Element>().cord.y, ROW, COL);
        int dragId = dragElement.GetComponent<Element>().id;
        Debug.Log("USAO U CHECKING MATCHING");
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
            col.gameObject.transform.GetComponent<Element>().id = dragId + 4;
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
            col.gameObject.transform.GetComponent<Element>().id = dragId + 4;
            col.gameObject.transform.GetComponent<Element>().active = true;
            col.gameObject.transform.GetComponent<Image>().sprite = boardManager.spriteElements[dragId];
        }
        else if (sol < 3)
            Swap(col);
        sol = 1;
        positionBoard = new List<Vector2>();
        entered = false;
        matched = false;
    }

    public void Swap(Collider2D col)
    {
        int colIndex = (int)col.gameObject.GetComponent<Element>().cord.x * 5 +
                       (int)col.gameObject.GetComponent<Element>().cord.y;
        int dragIndex = (int)dragElement.GetComponent<Element>().cord.x * 5 +
                       (int)dragElement.GetComponent<Element>().cord.y;
        
        Sprite tmpSprite = col.gameObject.GetComponent<Image>().sprite;
        int tmpIndex = col.gameObject.GetComponent<Element>().id;
        
        boardManager.element[dragIndex].GetComponent<Image>().enabled = true;
        boardManager.element[dragIndex].GetComponent<Element>().active = true;
        boardManager.element[colIndex].GetComponent<Element>().active = true;
        
        
        boardManager.element[colIndex].GetComponent<Image>().sprite =
            boardManager.element[dragIndex].GetComponent<Image>().sprite;
        boardManager.element[dragIndex].GetComponent<Image>().sprite = tmpSprite;

        boardManager.element[colIndex].GetComponent<Element>().id = boardManager.element[dragIndex].id;
        boardManager.element[dragIndex].GetComponent<Element>().id = tmpIndex;

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