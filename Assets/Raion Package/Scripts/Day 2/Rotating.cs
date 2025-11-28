using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] float zRotation;
  void FixedUpdate()
    {
        transform.Rotate(0, 0, zRotation, Space.World);
    }
}
