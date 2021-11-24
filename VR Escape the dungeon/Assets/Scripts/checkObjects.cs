using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkObjects : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerExit(Collider other)
    {
        if (CheckCollision.kold && CheckPositions.test)
        {
            _animator.Play("DoorOpen",0,0.0f);
        }
    }
}
