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
        var position = transform.position + Velocity * Time.deltaTime;
        RaycastHit hit;
        var direction = (position - transform.position).normalized;
        var distance = (position - transform.position).magnitude + 0.1f;
        var top = transform.position - direction * 0.1f + new Vector3(0, transform.localScale.y, 0);
        var bottom = transform.position - direction * 0.1f - new Vector3(0, transform.localScale.y, 0);
        var radius = (transform.localScale.x + transform.localScale.z) / 4f;
        if (Physics.CapsuleCast(top, bottom, radius, direction, out hit, distance)) {
            position = transform.position;
            Velocity = Vector3.zero;
            TargetVelocity = Vector3.zero;
            NeedsToStop = true;
        }
        transform.position = position;
    }

    public void MarkPosition(Vector3 position) {
        var offset = position - transform.position;
        offset.y = 0;
        TargetVelocity = offset.normalized * MoveSpeed;
        NeedsToStop = true;
    }
}
