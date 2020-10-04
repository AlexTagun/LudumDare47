using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    void Start() {
        StartCoroutine(moveToCoroutine(_targetTransformA));
    }

    System.Collections.IEnumerator moveToCoroutine(Transform inTargetTransform) {
        while (true) {
            float theSpeedPerFrame = _speed * Time.fixedDeltaTime;
            Vector3 theVectorToTarget = inTargetTransform.position - transform.position;
            float theVectorToTargetMagnitude = theVectorToTarget.magnitude;

            if (theVectorToTargetMagnitude > theSpeedPerFrame) {
                transform.position += theVectorToTarget / theVectorToTargetMagnitude * theSpeedPerFrame;
                yield return null;
            } else {
                break;
            }
        }

        transform.position = inTargetTransform.position;

        Transform theNextTargetTransform = (inTargetTransform == _targetTransformA) ? _targetTransformB : _targetTransformA;
        StartCoroutine(moveToCoroutine(theNextTargetTransform));
    }

    [SerializeField] Transform _targetTransformA = null;
    [SerializeField] Transform _targetTransformB = null;
    [SerializeField] float _speed = 1f;
}
