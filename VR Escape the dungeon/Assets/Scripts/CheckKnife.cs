using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKnife : MonoBehaviour
{
  public static bool knife;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("knife"))
    {
      knife = true;
    }
  }
}
