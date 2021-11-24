using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPositions : MonoBehaviour
{
    public static bool test;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("flask"))
        {
            test = true;
            
        }
    }
}
