using System;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.ScrollRect;

public static class Extensions {
    // UI SetListener extension that removes previous and then adds new listener
    // (this version is for onClick etc.)
    public static void SetListener(this UnityEvent uEvent, UnityAction call)
    {
        uEvent.RemoveAllListeners();
        uEvent.AddListener(call);
    }

    public static void SetListener(this UnityEvent<Vector2> scrollEvent, UnityAction<Vector2> call)
    {
        scrollEvent.RemoveAllListeners();
        scrollEvent.AddListener(call);
    }

    // UI SetListener extension that removes previous and then adds new listener
    // (this version is for onEndEdit, onValueChanged etc.)
    public static void SetListener<T>(this UnityEvent<T> uEvent, UnityAction<T> call)
    {
        uEvent.RemoveAllListeners();
        uEvent.AddListener(call);
    }

    public static float  RoundToDec(this float number, int decimals)
    {
        float result = Mathf.Round(number * Mathf.Pow(10, decimals)) / Mathf.Pow(10, decimals);

        return result;
    }

    public static void ParentAndReset(this Transform trans, Transform parent, bool resetScale = true)
    {
        trans.parent = parent;
        trans.Reset(resetScale);
    }

    public static void Reset(this Transform trans, bool resetScale = true)
    {
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        if (resetScale)
            trans.localScale = Vector3.zero;
    }
    public static bool TryGetComponent<T>(this GameObject go, out T result)
    {
        result = go.GetComponent<T>();
        return result != null;
    }

    public static bool TryGetComponentInChildren<T>(this GameObject go, out T result)
    {
        result = go.GetComponentInChildren<T>(true);
        return result != null;
    }

    public static bool TryGetComponents<T>(this GameObject go, out T[] result)
    {
        result = go.GetComponents<T>();
        return result != null;
    }

    public static bool TryGetComponentsInChildren<T>(this GameObject go, out T[] result)
    {
        result = go.GetComponentsInChildren<T>();
        return result != null;
    }

    public static bool TryGetElementAtIndex<T>(this object[] par, out T obj, int index)
    {
        if (par != null && par.Length > index && par[index] is T temp)
        {
            obj = temp;
            return true;
        }
        else
        {
            obj = default;
            return false;
        }
    }
}