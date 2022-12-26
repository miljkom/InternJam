using Newtonsoft.Json.Bson;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class MiniGameManager : MonoBehaviour
{
    #region Singleton
    public static MiniGameManager Instance { get; private set; }
    private MiniGameManager() { }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
            Instance = this;
       
    }
    #endregion

    #region Sequence
    [Space]
    [SerializeField] GameObject[] sequence;
    [SerializeField] Sprite fullCircle;
    [SerializeField] GameObject levelWon;
    [SerializeField] GameObject sequenceIndicator;
    [SerializeField] Timer timer;
    [SerializeField] GameObject losePopupPrefab;
    private int progress = 0;

    public void SwapSprite(GameObject go, Sprite sprite)
    {
        go.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public IEnumerator FillSequence()
    {
        progress++;
        if(progress >= sequence.Length) 
        {
            StartCoroutine(ReturnToSceneStart());
            progress = 1;
            SwapSprite(sequence[4], fullCircle);
        }
        yield return new WaitForSeconds(1f);
        SwapSprite(sequence[progress-1], fullCircle);
    }
    #endregion

    #region PixelCreation
    [Space(10)]
    [SerializeField] GameObject pixelBoardPrefab;
    [SerializeField] Canvas canvas;
    private PixelBoard pixelBoard;
    [SerializeField] private GameObject draggablePrefab;
    private GameObject draggable;
    Vector3 originPosFly;

    public void CreatePixelBoard()
    {
        pixelBoard = Instantiate(pixelBoardPrefab, canvas.transform, false).GetComponent<PixelBoard>();
    }
    #endregion

    private void Start()
    {
        CreatePixelBoard();
        draggable = Instantiate(draggablePrefab, canvas.transform, false);
        originPosFly = flyElement.transform.position;
    }

    #region Elements
    [Space(10)]
    public MysteryCoin mysteryCoin;

   

    public IEnumerator RefreshDraggable()
    {
        yield return new WaitForSeconds(0.2f);
        DestroyObject(draggable);
        draggable = Instantiate(draggablePrefab, canvas.transform, false);
    }
    #endregion
    private IEnumerator ReturnToSceneStart()
    {

        //Instantiate(levelWon.transform, canvas.transform, false);   
        levelWon.SetActive(true);
        timer.timerSlider.value += 30;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Scenes/SceneStart", LoadSceneMode.Single);
        
    }

    public MysteryCoin flyElement;

    public IEnumerator Fly()
    {
        if (flyElement.isFlyElement)
        {
            float journey = 0f;
            while (journey <= 1)
            {
                journey += Time.deltaTime;
                float percent = Mathf.Clamp01(journey / 1f);

                flyElement.transform.position = Vector3.Lerp(originPosFly, sequence[progress-1].transform.position, percent);

                yield return null;
            }
        }
    }

    public IEnumerator Slide()
    {
        {
            float journey = 0f;
            Vector3 originPos = sequenceIndicator.transform.position;
            while (journey <= 1)
            {
                journey += Time.deltaTime;
                float percent = Mathf.Clamp01(journey / 1f);

                sequenceIndicator.transform.position = Vector3.Lerp(originPos, sequence[progress].transform.position, percent);
                

                yield return null;
            }
        }
    }

    public void Lose()
    {
        timer.timerSlider.value += 30;
        pixelBoard.gameObject.SetActive(false);
        draggable.gameObject.SetActive(false);
        Instantiate(losePopupPrefab, canvas.transform, false);
    }

}