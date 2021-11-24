using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSword : MonoBehaviour
{
  public static bool sword;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("sword"))
    {
      sword = true;
    }
  }
}
