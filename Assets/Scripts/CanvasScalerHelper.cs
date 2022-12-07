using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasScalerHelper : MonoBehaviour
{
    private CanvasScaler canvasScaler;
    private Vector2 canvasReferenceResolutionLandscape;
    private Vector2 canvasReferenceResolutionPortrait;

    private float referenceAspectLandscape;
    private float referenceAspectPortrait;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        //we can always get canvas scaler component since script is requiring it
        canvasScaler = GetComponent<CanvasScaler>();
        CacheReferenceResolutionAndAspect();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        WOrientationCtrl.instance.OnDeviceRotation += OnRotated;
        ChangePopupCanvasSize(WOrientationCtrl.instance.state);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        WOrientationCtrl.instance.OnDeviceRotation -= OnRotated;
    }

    private void OnRotated(WOrientationCtrl.ScrOrientation orientation)
    {
        ChangePopupCanvasSize(orientation);
    }

    private void ChangePopupCanvasSize(WOrientationCtrl.ScrOrientation state)
    {
        // 0 => match width
        // 1 => match height

        switch (state)
        {
            case WOrientationCtrl.ScrOrientation.PORTRAIT:
                canvasScaler.referenceResolution = canvasReferenceResolutionPortrait;
                //if camera aspect is greater then referent one we want to match height otherwise match width
                //so for example if we are changing from 9:16 to 3:4 only width changes
                canvasScaler.matchWidthOrHeight = (CalculateAspect() >= referenceAspectPortrait) ? 1 : 0;
                break;
            case WOrientationCtrl.ScrOrientation.LANDSCAPE:
                //same logic as for portrait
                canvasScaler.referenceResolution = canvasReferenceResolutionLandscape;
                canvasScaler.matchWidthOrHeight = (CalculateAspect() <= referenceAspectLandscape) ? 0 : 1;
                break;
            default: //Def state is landscape
                canvasScaler.referenceResolution = canvasReferenceResolutionLandscape;
                canvasScaler.matchWidthOrHeight = (CalculateAspect() <= referenceAspectLandscape) ? 0 : 1;
                break;
        }
    }

    /// <summary>
    /// Caches referent resolution and aspect for later user
    /// </summary>
    private void CacheReferenceResolutionAndAspect()
    {
        Vector2 res = canvasScaler.referenceResolution;

        //Checks for default project resolution and sets for reference for portrait and landscape
        //if x < y that means that def project resolution is set in portrait
        if (res.x < res.y)
        {
            canvasReferenceResolutionLandscape = new Vector2(res.y, res.x);
            canvasReferenceResolutionPortrait = new Vector2(res.x, res.y);
        }
        else
        {
            canvasReferenceResolutionLandscape = new Vector2(res.x, res.y);
            canvasReferenceResolutionPortrait = new Vector2(res.y, res.x);
        }

        //Sets project reference apsects for portrait and landscape
        //It should be always some 16:9 resolution since most of the screens are using it
        //Preferably 1920:1080. This should be set in canvas scaler in unity editor
        referenceAspectLandscape = (16f/9f).RoundToDec(2);
        referenceAspectPortrait = (9f/16f).RoundToDec(2);
    }

    private float CalculateAspect()
    {
        return Camera.main.aspect.RoundToDec(2);
    }
}