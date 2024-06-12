using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MysteryCoin : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Sprite plane;
    [SerializeField] private Sprite tree;
    [SerializeField] private Sprite sun;
    [SerializeField] private Sprite umbrella;
    public CoinType coinType;
    public bool isMystery;

    public enum CoinType
    {
        plane = 1,
        tree = 2,
        sun = 3,
        umbrella = 4
    }


    private int ReturnRandomCoin()
    {
        int value = Random.Range(1, 4);
        return value;
    }

    public void CreateRandomCoin()
    {
        int coinIndex = ReturnRandomCoin();

        switch (coinIndex)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = plane;
                coinType = CoinType.plane;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = tree;
                coinType = CoinType.tree;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = sun;
                coinType = CoinType.sun;
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().sprite = umbrella;
                coinType = CoinType.umbrella;
                break;
        }
    }

    private void Awake()
    {
        if (isMystery)
            CreateRandomCoin();
            MiniGameManager.Instance.mysteryCoin = this;
        if (isFlyElement)
            MiniGameManager.Instance.flyElement = this;
    }
    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.Entry endEntry = new EventTrigger.Entry();
        endEntry.eventID = EventTriggerType.EndDrag;
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        endEntry.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    #region Dragg
    public void OnDrag(PointerEventData eventData)
    {
        if (!isMystery)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
            transform.position = rayPoint;
        }
    }

    #endregion

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isMystery)
        {
            if (other.gameObject.GetComponent<MysteryCoin>().coinType == coinType)
            {
                MiniGameManager.Instance.StartCoroutine(MiniGameManager.Instance.FillSequence());
                MiniGameManager.Instance.CreatePixelBoard();
                MiniGameManager.Instance.StartCoroutine(MiniGameManager.Instance.Fly());
                MiniGameManager.Instance.StartCoroutine(MiniGameManager.Instance.Slide());

            }
        }
        //vrati linearno unistavanje pixela;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
            MiniGameManager.Instance.StartCoroutine(MiniGameManager.Instance.RefreshDraggable());
    }

    public bool isFlyElement;

       
}
