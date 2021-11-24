using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBottle : MonoBehaviour
{
    public static bool bottle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bottle"))
        {
          bottle = true;

        }
    }
}
