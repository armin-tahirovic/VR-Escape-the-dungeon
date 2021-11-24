using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class CheckCollision : MonoBehaviour
{
    public static bool kold;
    public Text text;
   



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("book"))
        {
            kold = true;
        }
    }
}
