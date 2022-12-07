using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFly : MonoBehaviour
{
    public Vector2 positionToGo;
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, positionToGo, Time.deltaTime * 4000f);
    }
}