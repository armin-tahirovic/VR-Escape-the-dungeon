using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForks : MonoBehaviour
{
  public static bool fork;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("fork"))
    {
      fork = true;
    }
  }
}
