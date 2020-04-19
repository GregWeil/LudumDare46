using UnityEngine;

public class KeepMovement : MonoBehaviour {
    private Animator Animation;
    private CapsuleCollider Collider;
    private Vector3 Velocity = Vector3.zero;
    private Vector3 TargetVelocity = Vector3.zero;
    private bool NeedsToStop = false;

    public float RotationSpeed;
    public float MoveSpeed;
    public float Acceleration;
    public float Decceleration;
    public float StopThreshold;

    void Start() {
        Animation = GetComponentInChildren<Animator>();
        Collider = GetComponent<CapsuleCollider>();
    }

    void Update() {
        if (NeedsToStop) {
            Velocity = Vector3.MoveTowards(Velocity, Vector3.zero, Decceleration * Time.deltaTime);
            if (Velocity.magnitude <= StopThreshold) NeedsToStop = false;
        } else {
            Velocity = Vector3.MoveTowards(Velocity, TargetVelocity, Acceleration * Time.deltaTime);
        }
        if (Velocity.sqrMagnitude > 0f) {
            var targetRotation = Quaternion.LookRotation(Velocity.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
        var position = transform.position + Velocity * Time.deltaTime;
        RaycastHit hit;
        var direction = (position - transform.position).normalized;
        var distance = (position - transform.position).magnitude + 0.1f;
        var top = transform.position - direction * 0.1f + new Vector3(0, transform.localScale.y, 0);
        var bottom = transform.position - direction * 0.1f - new Vector3(0, transform.localScale.y, 0);
        if (Physics.CapsuleCast(top, bottom, Collider.radius, direction, out hit, distance, LayerMask.GetMask("Default"))) {
            position = transform.position;
            Velocity = Vector3.zero;
            TargetVelocity = Vector3.zero;
            NeedsToStop = true;
        }
        transform.position = position;
        Animation.SetFloat("Speed", Velocity.magnitude);
    }

    public void MarkPosition(Vector3 position) {
        var offset = position - transform.position;
        offset.y = 0;
        TargetVelocity = offset.normalized * MoveSpeed;
        NeedsToStop = true;
    }
}
