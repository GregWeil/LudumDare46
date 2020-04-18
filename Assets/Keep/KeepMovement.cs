using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMovement : MonoBehaviour {
    private Vector3 Velocity = Vector3.zero;
    private Vector3 TargetVelocity = Vector3.zero;
    private bool NeedsToStop = false;

    public float MoveSpeed;
    public float Acceleration;
    public float Decceleration;
    public float StopThreshold;

    void Update() {
        if (NeedsToStop) {
            Velocity = Vector3.MoveTowards(Velocity, Vector3.zero, Decceleration * Time.deltaTime);
            if (Velocity.magnitude <= StopThreshold) NeedsToStop = false;
        } else {
            Velocity = Vector3.MoveTowards(Velocity, TargetVelocity, Acceleration * Time.deltaTime);
        }
        transform.position += Velocity * Time.deltaTime;
    }

    public void MarkPosition(Vector3 position) {
        var offset = position - transform.position;
        offset.y = 0;
        TargetVelocity = offset.normalized * MoveSpeed;
        NeedsToStop = true;
    }
}
