using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{

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
  [Space]
  [SerializeField] private Transform palm;
  [SerializeField] private float reachDistance = 0.1f, jointDistance = 0.03f;
  [SerializeField] private LayerMask grabbableLayer;

  private Transform _followTarget;
  private Rigidbody _body;

  private bool _isGrabbing;
  private GameObject _holding;
  private Transform _grabPoint;
  private FixedJoint _joint1, _joint2;

  // Start is called before the first frame update
  void Start()
  {
    // Animation
    _animator = GetComponent<Animator>();

    // Physics
    _followTarget = controller.gameObject.transform;
    _body = GetComponent<Rigidbody>();
    _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
    _body.interpolation = RigidbodyInterpolation.Interpolate;
    _body.mass = 20f;
    _body.maxAngularVelocity = 20f;

    // Input setup
    controller.selectAction.action.started += Grab;
    controller.selectAction.action.canceled += Release;

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

  void Grab(InputAction.CallbackContext context)
  {
    if (_isGrabbing || _holding) return;

    Collider[] grabColliders = Physics.OverlapSphere(palm.position, reachDistance, grabbableLayer);

    if (grabColliders.Length < 1) return;

    var objectToGrab = grabColliders[0].transform.gameObject;
    var objectBody = objectToGrab.GetComponent<Rigidbody>();

    if (objectBody != null)
    {
      _holding = objectBody.gameObject;
    }
    else
    {
      objectBody = objectToGrab.GetComponentInParent<Rigidbody>();
      if (objectBody != null)
      {
        _holding = objectBody.gameObject;
      }
      else
      {
        return;
      }
    }

    StartCoroutine(GrabObject(grabColliders[0], objectBody));
  }

  IEnumerator GrabObject(Collider collider, Rigidbody objectBody)
  {
    _isGrabbing = true;

    // Create a grab point
    _grabPoint = new GameObject().transform;
    _grabPoint.position = collider.ClosestPoint(palm.position);
    _grabPoint.parent = _holding.transform;

    // Move hand to grab point
    _followTarget = _grabPoint;

    // Wait for hand to reach grab point
    while (_grabPoint != null && Vector3.Distance(_grabPoint.position, palm.position) > jointDistance && _isGrabbing)
    {
      yield return new WaitForEndOfFrame();
    }

    // Freeze hand and object motion
    _body.velocity = Vector3.zero;
    _body.angularVelocity = Vector3.zero;
    objectBody.velocity = Vector3.zero;
    objectBody.angularVelocity = Vector3.zero;

    objectBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    objectBody.interpolation = RigidbodyInterpolation.Interpolate;

    // Attach joins
    _joint1 = gameObject.AddComponent<FixedJoint>();
    _joint1.connectedBody = objectBody;
    _joint1.breakForce = float.PositiveInfinity;
    _joint1.breakTorque = float.PositiveInfinity;

    _joint1.connectedMassScale = 1;
    _joint1.massScale = 1;
    _joint1.enableCollision = false;
    _joint1.enablePreprocessing = false;

    _joint2 = _holding.AddComponent<FixedJoint>();
    _joint2.connectedBody = _body;
    _joint2.breakForce = float.PositiveInfinity;
    _joint2.breakTorque = float.PositiveInfinity;

    _joint2.connectedMassScale = 1;
    _joint2.massScale = 1;
    _joint2.enableCollision = false;
    _joint2.enablePreprocessing = false;

    // Reset follow target
    _followTarget = controller.gameObject.transform;

  }

  void Release(InputAction.CallbackContext context)
  {
    if (_joint1 != null)
      Destroy(_joint1);
    if (_joint2 != null)
      Destroy(_joint2);
    if (_grabPoint != null)
      Destroy(_grabPoint.gameObject);

    if (_holding != null)
    {
      var objectBody = _holding.GetComponent<Rigidbody>();
      objectBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
      objectBody.interpolation = RigidbodyInterpolation.None;
      _holding = null;
    }

    _isGrabbing = false;
    _followTarget = controller.gameObject.transform;
  }
}
