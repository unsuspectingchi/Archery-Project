using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  private Vector3 posOffset = new Vector3(0f, 1.6f, -2.6f);
  private Vector3 angleOffset = new Vector3(0f, 1.6f, 0f);
  private Transform target;
  [HideInInspector]
  public bool isShooting = false;

  void Start()
  {
    target = GameObject.Find("Player").transform;
  }

  private void LateUpdate()
  {
    if (isShooting)
    {
      posOffset = new Vector3(0f, 1.2f, 2f);
      angleOffset = new Vector3(0f, 1.6f, 20f);
      transform.position = target.TransformPoint(posOffset);
      transform.rotation = target.rotation;
    }
    else
    {
      transform.position = target.TransformPoint(posOffset);
      transform.LookAt(target.position + angleOffset);
      posOffset = new Vector3(0f, 1.6f, -2.6f);
      angleOffset = new Vector3(0f, 1.6f, 0f);
    }

  }
}