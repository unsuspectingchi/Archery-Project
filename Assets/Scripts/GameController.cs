using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public string ARROW_PATH = "Prefabs/Arrow_Prefab";
  private Vector3 DEFAULT_ARROW_POSITION = new Vector3(500, 2, 502);

  void Start()
  {
    GameObject arrow = Instantiate(Resources.Load(ARROW_PATH)) as GameObject;
    arrow.transform.position = DEFAULT_ARROW_POSITION;
  }
}