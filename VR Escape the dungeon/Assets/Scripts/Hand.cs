using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{
  private SkinnedMeshRenderer mesh;

  // Animation
  public float animationSpeed;
  private Animator _animator;
  private float _triggerTarget;
  private float _triggerCurrent;
  private string _animatorTrigger = "Trigger";

  // Physics
  [SerializeField] private ActionBasedController controller;
  [SerializeField] private float followSpeed = 30f;
  [SerializeField] private float rotateSpeed = 100f;
  [Space]
  [SerializeField] private Vector3 positionOffset;
  [SerializeField] private Vector3 rotationOffset;

  private Transform _followTarget;
  private Rigidbody _body;

  // Start is called before the first frame update
  void Start()
  {
    mesh = GetComponentInChildren<SkinnedMeshRenderer>();

    // Animation
    _animator = GetComponent<Animator>();

    // Physics
    _followTarget = controller.gameObject.transform;
    _body = GetComponent<Rigidbody>();
    _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
    _body.interpolation = RigidbodyInterpolation.Interpolate;
    _body.mass = 20f;
    _body.maxAngularVelocity = 20f;

    // Teleport hands
    _body.position = _followTarget.position;
    _body.rotation = _followTarget.rotation;
  }

  // Update is called once per frame
  void Update()
  {
    AnimateHand();
    PhysicsMove();
  }

  private void PhysicsMove()
  {
    // Position
    var positionWithOffset = _followTarget.TransformPoint(positionOffset);
    var distance = Vector3.Distance(positionWithOffset, transform.position);
    _body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

    // Rotation
    var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
    var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
    q.ToAngleAxis(out float angle, out Vector3 axis);
    _body.angularVelocity = angle * (axis * Mathf.Deg2Rad * rotateSpeed);
  }

  public void SetTrigger(float x)
  {
    _triggerTarget = x;
  }

  void AnimateHand()
  {
    if (_triggerCurrent != _triggerTarget)
    {
      _triggerCurrent = Mathf.MoveTowards(_triggerCurrent, _triggerTarget, Time.deltaTime * animationSpeed);
      _animator.SetFloat(_animatorTrigger, _triggerCurrent);
    }
  }

  public void ToggleVisibility()
  {
    mesh.enabled = !mesh.enabled;
  }
}
