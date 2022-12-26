using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public Tier3Element poolObject;
    
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
            
            entered = true;
        }
        else
        {
            matched = true;
        }
    }
    public void CheckMatching(Collider2D col)
    {
        SetBoolMatrix();
        int ROW = boolBoard.GetLength(0);
        int COL = boolBoard.GetLength(1);
        DFS(boolBoard, (int)col.gameObject.GetComponent<Element>().cord.x,
            (int)col.gameObject.GetComponent<Element>().cord.y, ROW, COL);
        int dragId = dragElement.GetComponent<Element>().id;
        Debug.Log("USAO U CHECKING MATCHING");
        int k = 0;
        for (int i = 0; i < 25; i++)
        {
            boardManager.element[i].transform.GetChild(0).gameObject.SetActive(false);
        }
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
            if (dragId < 4)
            {
                col.gameObject.transform.GetComponent<Image>().enabled = true;
                col.gameObject.transform.GetComponent<Element>().id = dragId + 4;
                col.gameObject.transform.GetComponent<Element>().active = true;
                col.gameObject.transform.GetComponent<Image>().sprite = boardManager.imageTier2Elements[dragId];
                int secondElement = (int)positionBoard[1].x * 5 + (int)positionBoard[1].y;
                boardManager.transform.GetChild(secondElement).GetComponent<Image>().enabled = true;
                boardManager.transform.GetChild(secondElement).GetComponent<Image>().sprite = boardManager.imageTier2Elements[dragId];
                boardManager.element[secondElement].id = dragId + 4;
                boardManager.element[secondElement].active = true;
            }
            else
            {
                col.gameObject.transform.GetComponent<Image>().enabled = false;
                col.gameObject.transform.GetComponent<Element>().id = dragId + 8;
                col.gameObject.transform.GetComponent<Element>().active = true;
                col.gameObject.transform.GetComponent<Image>().sprite = boardManager.imageTier3Elements[dragId - 4];
                if (dragId - 4 == 0)
                    k = 0;
                else if (dragId - 4 == 1)
                {
                    k = 2;
                }
                else if (dragId - 4 == 2)
                {
                    k = 4;
                }
                else if (dragId - 4 == 3)
                {
                    k = 6;
                }
                int positionOfObject = (int)positionBoard[0].x * 5 + (int)positionBoard[0].y;
                Vector2 position = boardManager.transform.GetChild(positionOfObject).transform.position;
                Tier3Element elementForAnimation = Instantiate(poolObject, position, Quaternion.identity, boardManager.positionToGo);
                elementForAnimation.GetComponent<Image>().enabled = true;
                elementForAnimation.GetComponent<Image>().sprite =
                    boardManager.imageTier3Elements[dragId - 4];
                elementForAnimation.transform.GetComponent<Image>().color = new Color(
                    elementForAnimation.transform.GetComponent<Image>().color.r,
                    elementForAnimation.transform.GetComponent<Image>().color.g,
                    elementForAnimation.transform.GetComponent<Image>().color.b, 1f);
                elementForAnimation.gameObject.SetActive(true);
                elementForAnimation.AddComponent<ElementMerge>();
                elementForAnimation.GetComponent<ElementMerge>().positionToGo =
                    boardManager.positionToGo.GetChild(k).position;
                Debug.Log(boardManager.positionToGo.GetChild(k).position);
                Score.instance.score[dragId - 4]++;
                col.gameObject.transform.GetComponent<Element>().id = 10;
                col.gameObject.transform.GetComponent<Element>().active = false;
                boardManager.positionToGo.GetChild(k).gameObject.SetActive(true);
                Debug.Log(k);
            }
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
            if (dragId < 4)
            {
                col.gameObject.transform.GetComponent<Image>().enabled = true;
                col.gameObject.transform.GetComponent<Element>().id = dragId + 4;
                col.gameObject.transform.GetComponent<Element>().active = true;
                col.gameObject.transform.GetComponent<Image>().sprite = boardManager.imageTier2Elements[dragId];
            }
            else
            {
                col.gameObject.transform.GetComponent<Image>().enabled = false;
                col.gameObject.transform.GetComponent<Element>().id = dragId + 8;
                col.gameObject.transform.GetComponent<Element>().active = true;
                col.gameObject.transform.GetComponent<Image>().sprite = boardManager.imageTier3Elements[dragId - 4];
                if (dragId - 4 == 0)
                    k = 0;
                else if (dragId - 4 == 1)
                {
                    k = 2;
                }
                else if (dragId - 4 == 2)
                {
                    k = 4;
                }
                else if (dragId - 4 == 3)
                {
                    k = 6;
                }
                int positionOfObject = (int)positionBoard[0].x * 5 + (int)positionBoard[0].y;
                Vector2 position = boardManager.transform.GetChild(positionOfObject).transform.position;
                Tier3Element elementForAnimation = Instantiate(poolObject, position, Quaternion.identity, boardManager.positionToGo);
                elementForAnimation.GetComponent<Image>().enabled = true;
                elementForAnimation.GetComponent<Image>().sprite =
                    boardManager.imageTier3Elements[dragId - 4];
                elementForAnimation.transform.GetComponent<Image>().color = new Color(
                    elementForAnimation.transform.GetComponent<Image>().color.r,
                    elementForAnimation.transform.GetComponent<Image>().color.g,
                    elementForAnimation.transform.GetComponent<Image>().color.b, 1f);
                elementForAnimation.gameObject.SetActive(true);
                elementForAnimation.AddComponent<ElementMerge>();
                elementForAnimation.GetComponent<ElementMerge>().positionToGo =
                    boardManager.positionToGo.GetChild(k).position;
                Debug.Log(boardManager.positionToGo.GetChild(k).position);
                Score.instance.score[dragId - 4]++;
                col.gameObject.transform.GetComponent<Element>().id = 10;
                col.gameObject.transform.GetComponent<Element>().active = false;
                boardManager.positionToGo.GetChild(k).gameObject.SetActive(true);
                Debug.Log(k);
            }
        }
        else if (sol < 3)
            Swap(col);
        for (int i = 0; i < positionBoard.Count; i++)
        {
            int positionIndex = (int)positionBoard[i].x * 5 + (int)positionBoard[i].y;
            boardManager.element[positionIndex].transform.GetChild(0).gameObject.SetActive(false);
            boardManager.element[dragId].transform.GetChild(0).gameObject.SetActive(false);
        }
        sol = 1;
        dragElement.GetComponent<Element>().id = 10;
        dragElement.GetComponent<Element>().active = false;
        positionBoard = new List<Vector2>();
        entered = false;
        matched = false;
    }

    public void AnimationCheck(Collider2D col)
    {
        int index = (int)col.gameObject.GetComponent<Element>().cord.x * 5 +
            (int)col.gameObject.GetComponent<Element>().cord.y;
        int dragId = (int)dragElement.GetComponent<Element>().cord.x * 5 +
                     (int)dragElement.GetComponent<Element>().cord.y;
        
        if (dragElement.GetComponent<Image>().sprite == boardManager.element[index].GetComponent<Image>().sprite &&
            dragElement.GetComponent<Image>().enabled && dragElement.GetComponent<Element>().id != 10)
        {
            SetBoolMatrix();
            int ROW = boolBoard.GetLength(0);
            int COL = boolBoard.GetLength(1);
            DFS(boolBoard, (int)col.gameObject.GetComponent<Element>().cord.x,
                (int)col.gameObject.GetComponent<Element>().cord.y, ROW, COL);
            Debug.Log(sol);
            if (sol >= 3)
            {
                for (int i = 0; i < positionBoard.Count; i++)
                {
                    int positionIndex = (int)positionBoard[i].x * 5 + (int)positionBoard[i].y;
                    boardManager.element[positionIndex].transform.GetChild(0).gameObject.SetActive(true);
                    boardManager.element[dragId].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < 25; i++)
            {
                boardManager.element[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        sol = 1;
        positionBoard = new List<Vector2>();
        entered = true;
        matched = false;
    }
    public void SpawnSame()
    {
        int dragIndex = (int)dragElement.GetComponent<Element>().cord.x * 5 +
                        (int)dragElement.GetComponent<Element>().cord.y;
        boardManager.element[dragIndex].GetComponent<Image>().enabled = true;
        boardManager.element[dragIndex].GetComponent<Element>().active = true;
        boardManager.element[dragIndex].GetComponent<Element>().id = BoardManager.oldIndex;
    }
    public void Swap(Collider2D col)
    {
        if (dragElement.GetComponent<Element>().id != 10)
        {
            int colIndex = (int)col.gameObject.GetComponent<Element>().cord.x * 5 +
                           (int)col.gameObject.GetComponent<Element>().cord.y;
            int dragIndex = (int)dragElement.GetComponent<Element>().cord.x * 5 +
                            (int)dragElement.GetComponent<Element>().cord.y;
            if (boardManager.element[colIndex].GetComponent<Image>().enabled)
            {
                Sprite tmpSprite = col.gameObject.GetComponent<Image>().sprite;
                int tmpIndex = col.gameObject.GetComponent<Element>().id;

                boardManager.element[dragIndex].GetComponent<Image>().enabled = true;
                boardManager.element[dragIndex].GetComponent<Element>().active = true;
                boardManager.element[colIndex].GetComponent<Element>().active = true;


                boardManager.element[colIndex].GetComponent<Image>().sprite =
                    boardManager.element[dragIndex].GetComponent<Image>().sprite;
                boardManager.element[dragIndex].GetComponent<Image>().sprite = tmpSprite;

                boardManager.element[colIndex].GetComponent<Element>().id = BoardManager.oldIndex;
                boardManager.element[dragIndex].GetComponent<Element>().id = tmpIndex;
            }
            else if (col.gameObject == boardManager.element[dragIndex].gameObject)
            {
                boardManager.element[dragIndex].GetComponent<Image>().enabled = true;
                boardManager.element[dragIndex].GetComponent<Element>().active = true;
                boardManager.element[colIndex].GetComponent<Element>().id = BoardManager.oldIndex;

            }
            else
            {
                Sprite tmpSprite = col.gameObject.GetComponent<Image>().sprite;
                Debug.Log("OVDE");
                boardManager.element[dragIndex].GetComponent<Image>().enabled = false;
                boardManager.element[colIndex].GetComponent<Image>().enabled = true;

                boardManager.element[dragIndex].GetComponent<Element>().active = false;
                boardManager.element[colIndex].GetComponent<Element>().active = true;

                boardManager.element[colIndex].GetComponent<Image>().sprite =
                    boardManager.element[dragIndex].GetComponent<Image>().sprite;
                boardManager.element[dragIndex].GetComponent<Image>().sprite = tmpSprite;

                boardManager.element[colIndex].GetComponent<Element>().id = BoardManager.oldIndex;
                boardManager.element[dragIndex].GetComponent<Element>().id = 10;

            }
        }
    }
    public void SetBoolMatrix()
    {
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