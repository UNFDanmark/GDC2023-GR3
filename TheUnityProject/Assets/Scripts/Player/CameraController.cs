using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
    }
}
