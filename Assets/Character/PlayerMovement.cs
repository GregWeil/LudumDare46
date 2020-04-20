using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private Animator Animation;
    private SpriteRenderer Sprite;
    private CapsuleCollider Collider;
    private Vector2 InputMovement = Vector2.zero;
    private Vector3 Velocity = Vector3.zero;

    public float MoveSpeed;
    public float Acceleration;

    void Start() {
        Animation = GetComponentInChildren<Animator>();
        Sprite = GetComponentInChildren<SpriteRenderer>();
        Collider = GetComponent<CapsuleCollider>();
    }

    void Update() {
        var target = new Vector3(InputMovement.x, 0, InputMovement.y).normalized * MoveSpeed;
        var lerp = 1f - Mathf.Pow(1f - Acceleration, Time.deltaTime*60f);
        Velocity = Vector3.Lerp(Velocity, target, lerp);
        transform.position += Velocity * Time.deltaTime;

        for (var i = 0; i < 8; ++i) {
            RaycastHit hit;
            var center = transform.TransformPoint(Collider.center);
            var direction = Quaternion.Euler(0, i * 360f / 8, 0) * Vector3.forward;
            var distance = (transform.localScale.x + transform.localScale.z) / 4f;
            if (Physics.Raycast(transform.position, direction, out hit, distance, LayerMask.GetMask("Default"))) {
                var offset = hit.normal * (distance - hit.distance);
                offset.y = 0;
                transform.position += offset;
            }
        }

        Animation.SetFloat("Speed", Velocity.magnitude);
        if (Mathf.Abs(Velocity.x) > 1f) {
            Sprite.flipX = Velocity.x < 0f;
        }
    }

    void OnMove(InputValue value) {
        InputMovement = value.Get<Vector2>();
    }

    void Dead() {
        MoveSpeed = 0f;
    }
}
