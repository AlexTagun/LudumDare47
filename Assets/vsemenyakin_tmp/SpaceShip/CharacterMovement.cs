using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    void Update() {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        Vector3 vel = transform.forward * Input.GetAxis("Vertical") * speed;

        if (_characterController.isGrounded) {
            _vSpeed = 0;
            if (Input.GetKeyDown("space")) {
                _vSpeed = jumpSpeed;
            }
        }

        _vSpeed -= gravity * Time.deltaTime;
        vel.y = _vSpeed;
        _characterController.Move(vel * Time.deltaTime);
    }

    //Fields
    [SerializeField]
    float speed = 5f; // units per second

    [SerializeField]
    float turnSpeed = 90f; // degrees per second

    [SerializeField]
    float jumpSpeed = 8f;

    [SerializeField]
    float gravity = 9.8f;

    [SerializeField]
    private CharacterController _characterController = null;

    float _vSpeed = 0f;
}
