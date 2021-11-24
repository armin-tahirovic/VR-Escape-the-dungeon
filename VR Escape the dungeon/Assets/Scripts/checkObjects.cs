using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkObjects : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerExit(Collider other)
    {
        if (CheckBook.book && CheckBottle.bottle && CheckSword.sword && CheckPlate.plate && CheckForks.fork && CheckKnife.knife)
        {
            _animator.Play("DoorOpen",0,0.0f);
        }
    }
}
