using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WOrientationCtrl : MonoBehaviour
{
    public static WOrientationCtrl instance;
    const int frameInterval = 4;
    private float currWidth = 1;
    private float currHeight = 1;
    public enum ScrOrientation
    {
        PORTRAIT = 0,
        LANDSCAPE = 1,
        UNKNOWN = 3
    }
    public ScrOrientation state = ScrOrientation.UNKNOWN;
    public UnityAction<ScrOrientation> OnDeviceRotation;
    public float screenRatio;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        StartCoroutine(CheckDelayedFrame());
    }

    IEnumerator CheckDelayedFrame() //Custom loop na svaki 5ti frame.
    {
        while (true)
        {
            for (int i = 0; i < frameInterval; i++)
            {
                yield return new WaitForEndOfFrame();
            }
            
            FrameDelayedUpdate();
        }
    }

    private void FrameDelayedUpdate()
    {
        if (currHeight / currWidth <=1 && (float)Screen.height / (float)Screen.width > 1)
        {
            currHeight = Screen.height;
            currWidth = Screen.width;
            state = ScrOrientation.PORTRAIT;
            screenRatio = (float)Screen.width / (float)Screen.height;
            if (OnDeviceRotation != null)
                OnDeviceRotation(state);
            //Debug.Log("Screen switch " + state);
        }
        if (currHeight / currWidth >= 1 && (float)Screen.height / (float)Screen.width < 1)
        {
            currHeight = Screen.height;
            currWidth = Screen.width;
            state = ScrOrientation.LANDSCAPE;
            screenRatio = (float)Screen.width / (float)Screen.height;
            if (OnDeviceRotation != null)
                OnDeviceRotation(state);
            //Debug.Log("Screen switch " + state);
        }
        //Debug.Log("FRAME UPDATE " + frameInterval +" "+ state);
    }

    public static ScrOrientation CurrentOrientation()
    {
        if (instance != null)
        {
            return instance.state;
        }
        return ScrOrientation.UNKNOWN;
    }

}
