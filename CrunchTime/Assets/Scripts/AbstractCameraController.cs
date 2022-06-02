using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public abstract class AbstractCameraController : MonoBehaviour
{
    [SerializeField]
    protected GameObject target;
}