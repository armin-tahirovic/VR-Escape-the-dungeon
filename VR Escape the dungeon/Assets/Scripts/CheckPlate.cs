using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlate : MonoBehaviour
{
  public static bool plate;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("plate"))
    {
      plate = true;
    }
  }
}
