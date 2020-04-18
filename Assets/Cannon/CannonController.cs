using UnityEngine;
using UnityEngine.InputSystem;

public class CannonController : MonoBehaviour {
    private GameObject Keep;
    private Vector2 Movement = Vector2.zero;
    private Vector2 Velocity = Vector2.zero;

    public GameObject Target;
    public float MoveSpeed;
    public float Acceleration;
    public float MinRange;
    public float MaxRange;

    void Start() {
        Keep = GameObject.Find("Keep");
    }

    void Update() {
        transform.position = new Vector3(Keep.transform.position.x, 0, Keep.transform.position.z);
        Velocity = Vector2.MoveTowards(Velocity, Movement, Acceleration * Time.deltaTime);
        Target.transform.localPosition += new Vector3(Velocity.x, 0, Velocity.y) * MoveSpeed * Time.deltaTime;
        var distance = Target.transform.localPosition.magnitude;
        if (distance < MinRange) {
            Target.transform.localPosition = Target.transform.localPosition.normalized * MinRange;
        } else if (distance > MaxRange) {
            Target.transform.localPosition = Target.transform.localPosition.normalized * MaxRange;
        }
    }

    void OnMove(InputValue value) {
        Movement = value.Get<Vector2>();
    }

    void OnTower(InputValue value) {
        if (value.isPressed) {
            SendMessageUpwards("ExitKeep");
        }
    }
}
