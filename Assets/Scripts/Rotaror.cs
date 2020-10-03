using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotaror : MonoBehaviour {
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _eulerAngleVelocity = new Vector3(0, 0, 100);

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(_eulerAngleVelocity * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }
}