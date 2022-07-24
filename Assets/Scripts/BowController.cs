using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
  private Vector3 posOffset = new Vector3(0f, 0.9f, -0.2f);
  private float angleOffset = -90;
  private Transform target;
  [HideInInspector]
  public bool isShooting = false;

  // private bool lookAt = true;

  void Start()
  {
    target = GameObject.Find("Player").transform;
  }

  private void LateUpdate()
  {
    if (isShooting)
    {
      posOffset = new Vector3(0.2f, 1.2f, 2.4f);
      transform.position = target.TransformPoint(posOffset);
      Quaternion newRotation = Quaternion.AngleAxis(angleOffset, Vector3.up) * target.rotation;
      transform.rotation = newRotation;
    }
    else
    {
      posOffset = new Vector3(0f, 0.8f, -0.2f);
      transform.position = target.TransformPoint(posOffset);
      transform.rotation = target.rotation;
    }

  }
}