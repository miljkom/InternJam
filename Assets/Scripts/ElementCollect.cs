using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Element"))
        {
            Destroy(col.gameObject);
            RandomElement.dontCreate = false;
        }
    }
}
