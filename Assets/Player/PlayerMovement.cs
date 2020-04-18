using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private Vector2 InputMovement = Vector2.zero;
    private Vector3 Velocity = Vector3.zero;

    public float MoveSpeed;
    public float Acceleration;

    void Update() {
        var target = new Vector3(InputMovement.x, 0, InputMovement.y).normalized * MoveSpeed;
        var lerp = 1f - Mathf.Pow(1f - Acceleration, Time.deltaTime*60f);
        Velocity = Vector3.Lerp(Velocity, target, lerp);
        transform.position += Velocity * Time.deltaTime;
    }

    void OnMove(InputValue value) {
        InputMovement = value.Get<Vector2>();
    }
}
