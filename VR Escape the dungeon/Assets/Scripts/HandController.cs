using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
  ActionBasedController _controller;
  public Hand hand;

  // Start is called before the first frame update
  void Start()
  {
    _controller = GetComponent<ActionBasedController>();
  }

  // Update is called once per frame
  void Update()
  {
    hand.SetTrigger(_controller.activateAction.action.ReadValue<float>());
  }
}
