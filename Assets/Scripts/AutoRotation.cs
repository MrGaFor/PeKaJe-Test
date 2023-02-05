using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    [SerializeField] private float Speed = 180f;
    void LateUpdate()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * Speed);
    }
}
