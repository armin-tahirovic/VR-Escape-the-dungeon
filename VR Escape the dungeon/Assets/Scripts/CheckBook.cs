using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class CheckBook : MonoBehaviour
{
    public static bool book;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("book"))
        {
            book = true;
        }
    }
}
