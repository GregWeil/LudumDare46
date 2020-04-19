using UnityEngine;
using UnityEngine.InputSystem;

public class CannonController : MonoBehaviour {
    private GameObject Keep;
    private Vector2 Movement = Vector2.zero;
    private Vector2 Velocity = Vector2.zero;

    public GameObject CannonballPrefab;
    public GameObject Target;
    public GameObject Mesh;
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
        Mesh.transform.rotation = Quaternion.LookRotation(Target.transform.localPosition.normalized, Vector3.up);
    }

    void OnMove(InputValue value) {
        Movement = value.Get<Vector2>();
    }

    void OnAttack(InputValue value) {
        if (value.isPressed) {
            var gameObject = Instantiate(CannonballPrefab);
            gameObject.name = CannonballPrefab.name;
            gameObject.transform.position = Mesh.transform.position;
            var cannonball = gameObject.GetComponent<Cannonball>();
            cannonball.TargetPosition = Target.transform.position;
            cannonball.IgnoreObject = Keep;
        }
    }

    void OnTower(InputValue value) {
        if (value.isPressed) {
            SendMessageUpwards("ExitKeep");
        }
    }
}
